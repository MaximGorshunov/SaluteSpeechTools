using Auth;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcSmartSpeech;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace TextToSpeechService.SpeechSynthesizer;

/// <summary>
/// Represents a streaming speech synthesizer that can synthesize audio.
/// </summary>
[SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates")]
public class StreamSpeechSynthesizer : ISpeechSynthesizer
{
    private const string EndPoint = "https://smartspeech.sber.ru";
    private static readonly SemaphoreSlim s_semaphoreSlim = new(5);
    
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<StreamSynthesisRequest>? _logger;
    private readonly SmartSpeech.SmartSpeechClient _client;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamSpeechSynthesizer"/> class.
    /// </summary>
    /// <param name="tokenProvider">Provides token for authorization</param>
    /// <param name="logger">Logger to log errors.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="tokenProvider"/> is null.</exception>
    public StreamSpeechSynthesizer(ITokenProvider tokenProvider, ILogger<StreamSynthesisRequest>? logger = null)
    {
        _logger = logger;
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        var channel = GrpcChannel.ForAddress(EndPoint);
        _client = new SmartSpeech.SmartSpeechClient(channel);
    }

    /// <summary>
    /// Synthesize audio from synthesis request.
    /// </summary>
    /// <param name="synthesisRequest">Contains synthesis request params.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Stream with synthesized audio bytes</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="synthesisRequest"/> is null or when <paramref name="synthesisRequest.Voice"/> or <paramref name="synthesisRequest.Text"/> is null.
    /// </exception>
    /// <exception cref="RpcException">Thrown when failed to synthesis audio.</exception>
    /// <exception cref="HttpRequestException">Thrown when there is authorization error.</exception>
    public async Task<Stream> SynthesizeAsync(ISynthesisRequest synthesisRequest, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if (synthesisRequest == null)
        {
            throw new ArgumentNullException(nameof(synthesisRequest));
        }
        
        if (synthesisRequest.Voice == null || synthesisRequest.Text == null)
        {
            throw new ArgumentNullException(nameof(synthesisRequest), "Voice and Text are required.");
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
            ex.Data.Add("synthesisRequest", synthesisRequest);
            _logger?.Log(LogLevel.Error, ex, "Failed to synthesis audio. Synthesis request: {@synthesisRequest}.", synthesisRequest);
            throw;
        }
        catch (HttpRequestException ex)
        {
            ex.Data.Add("synthesisRequest", synthesisRequest);
            _logger?.Log(LogLevel.Error, ex, "Failed to synthesis audio. Authorization error. Synthesis request: {@synthesisRequest}.", synthesisRequest);
            throw;
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }
    
    private async Task<Metadata> GetMetadata(CancellationToken cancellationToken) 
        => new() { { "Authorization", $"Bearer {await _tokenProvider.GetTokenAsync(cancellationToken).ConfigureAwait(false)}" } }; 
}
