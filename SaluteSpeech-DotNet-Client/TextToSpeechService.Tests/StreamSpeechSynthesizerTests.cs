using Auth;
using Xunit;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using TextToSpeechService.SpeechSynthesizer;

namespace TextToSpeechService.Tests;

public class StreamSpeechSynthesizerTests
{
    private static readonly IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    private static readonly string SecretKey = Config["ApiKeys:SecretKey"] ?? throw new ArgumentNullException(nameof(SecretKey));
    
    [Fact]
    public async Task SynthesizeAsync_ThrowsArgumentNullException_WhenSynthesisRequestIsNull()
    {
        var tokenProvider = new TokenProvider("secretKey");
        var cancellationToken = CancellationToken.None;
        var synthesisRequest = (ISynthesisRequest)null;
        var synthesizer = new StreamSpeechSynthesizer(tokenProvider);
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => synthesizer.SynthesizeAsync(synthesisRequest, cancellationToken));
    }
    
    [Fact]
    public async Task SynthesizeAsync_ThrowsRpcException_WhenTextIsEmpty()
    {
        var tokenProvider = new TokenProvider(SecretKey);
        var cancellationToken = CancellationToken.None;
        var synthesisRequest = new StreamSynthesisRequest(string.Empty);
        var synthesizer = new StreamSpeechSynthesizer(tokenProvider);
        
        await Assert.ThrowsAsync<RpcException>(() => synthesizer.SynthesizeAsync(synthesisRequest, cancellationToken));
    }
}