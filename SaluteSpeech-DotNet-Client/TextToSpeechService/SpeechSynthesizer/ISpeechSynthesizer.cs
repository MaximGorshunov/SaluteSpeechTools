namespace TextToSpeechService.SpeechSynthesizer;

public interface ISpeechSynthesizer
{
    /// <summary>
    /// Generates audio stream from synthesis request
    /// </summary>
    public Task<Stream> GenerateAsync(ISynthesisRequest synthesisRequest, CancellationToken cancellationToken);
}
