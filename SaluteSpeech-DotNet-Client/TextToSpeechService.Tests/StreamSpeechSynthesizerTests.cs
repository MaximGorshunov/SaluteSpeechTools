using Auth;
using Xunit;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using TextToSpeechService.SpeechSynthesizer;

namespace TextToSpeechService.Tests;

public class StreamSpeechSynthesizerTests
{
    private static readonly IConfigurationRoot s_config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    private static readonly string s_secretKey = s_config["ApiKeys:SecretKey"] ?? throw new ArgumentNullException(nameof(s_secretKey));
    
    [Fact]
    public async Task SynthesizeAsyncThrowsArgumentNullExceptionWhenSynthesisRequestIsNull()
    {
        var tokenProvider = new TokenProvider("secretKey");
        var cancellationToken = CancellationToken.None;
        ISynthesisRequest? synthesisRequest = null;
        var synthesizer = new StreamSpeechSynthesizer(tokenProvider);
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => synthesizer.SynthesizeAsync(synthesisRequest!, cancellationToken)).ConfigureAwait(false);
    }
    
    [Fact]
    public async Task SynthesizeAsyncThrowsRpcExceptionWhenTextIsEmpty()
    {
        var tokenProvider = new TokenProvider(s_secretKey);
        var cancellationToken = CancellationToken.None;
        var settings = new StreamSynthesisRequestSettings();
        var synthesisRequest = new StreamSynthesisRequest(settings, string.Empty);
        var synthesizer = new StreamSpeechSynthesizer(tokenProvider);
        
        await Assert.ThrowsAsync<RpcException>(() => synthesizer.SynthesizeAsync(synthesisRequest, cancellationToken)).ConfigureAwait(false);
    }
}
