using Auth;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcSmartSpeech;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace TextToSpeechService.SpeechSynthesizer;

[SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates")]
public class StreamSpeechSynthesizer : ISpeechSynthesizer
{
    private const string EndPoint = "https://smartspeech.sber.ru";
    private static readonly SemaphoreSlim s_semaphoreSlim = new(5);
    
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<StreamSynthesisRequest>? _logger;
    private readonly SmartSpeech.SmartSpeechClient _client;
    
    public StreamSpeechSynthesizer(ITokenProvider tokenProvider, ILogger<StreamSynthesisRequest>? logger = null)
    {
        _logger = logger;
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        var channel = GrpcChannel.ForAddress(EndPoint);
        _client = new SmartSpeech.SmartSpeechClient(channel);
    }

    public async Task<Stream> GenerateAsync(ISynthesisRequest synthesisRequest, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if (synthesisRequest == null)
        {
            throw new ArgumentNullException(nameof(synthesisRequest));
        }
        
        await s_semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var request = new SynthesisRequest
            {
                AudioEncoding = (SynthesisRequest.Types.AudioEncoding)synthesisRequest.AudioEncoding,
                ContentType = (SynthesisRequest.Types.ContentType)synthesisRequest.ContentType,
                Voice = synthesisRequest.Voice,
                Text = synthesisRequest.Text
            };
            var metadata = await GetMetadata(cancellationToken).ConfigureAwait(false);
            var response = _client.Synthesize(request, metadata, cancellationToken: cancellationToken);

            var audioBuffer = new List<byte[]>();
            while (await response.ResponseStream.MoveNext().ConfigureAwait(false))
            {
                audioBuffer.Add(response.ResponseStream.Current.Data.ToByteArray());
            }
            byte[] audioBytes = audioBuffer.SelectMany(byteArr => byteArr).ToArray();
            return new MemoryStream(audioBytes);
        }
        catch (RpcException ex)
        {
            _logger?.Log(LogLevel.Error, ex, "Failed to synthesis audio. Synthesis request: {@synthesisRequest}.", synthesisRequest);
            return new MemoryStream();
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }
    
    private async Task<Metadata> GetMetadata(CancellationToken cancellationToken) 
        => new() { { "Authorization", $"Bearer {await _tokenProvider.GetToken(cancellationToken).ConfigureAwait(false)}" } }; 
}
