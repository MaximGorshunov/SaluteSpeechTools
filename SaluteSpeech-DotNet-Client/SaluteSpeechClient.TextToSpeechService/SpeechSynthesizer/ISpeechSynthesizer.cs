namespace SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

/// <summary>
/// Represents a speech synthesizer that can synthesize audio.
/// </summary>
public interface ISpeechSynthesizer
{
    /// <summary>
    /// Synthesize audio from synthesis request.
    /// </summary>
    /// <param name="synthesisRequest">Contains synthesis request params.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Stream with synthesized audio bytes</returns>
    Task<Stream> SynthesizeAsync(ISynthesisRequest synthesisRequest, CancellationToken cancellationToken);
}
