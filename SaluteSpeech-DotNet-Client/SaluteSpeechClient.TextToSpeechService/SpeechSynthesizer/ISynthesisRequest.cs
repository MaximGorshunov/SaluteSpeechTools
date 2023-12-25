namespace SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

/// <summary>
/// Contains synthesis request params.
/// </summary>
public interface ISynthesisRequest
{
    /// <summary>
    /// Synthesis request settings.
    /// </summary>
    ISynthesisRequestSettings Settings { get; }
    /// <summary>
    /// Text to synthesize.
    /// </summary>
    string Text { get; }
}
