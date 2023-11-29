using TextToSpeechService.SpeechSynthesizer.Enums;

namespace TextToSpeechService.SpeechSynthesizer;

/// <summary>
/// Contains synthesis request params.
/// </summary>
public interface ISynthesisRequest
{
    /// <summary>
    /// Audio format for the synthesized audio content.
    /// </summary>
    AudioEncoding AudioEncoding { get; }
    /// <summary>
    /// The format of the data sent in the request body.
    /// </summary>
    ContentType ContentType { get; }
    /// <summary>
    /// Voice code that will be used for synthesis.
    /// </summary>
    string Voice { get; }
    /// <summary>
    /// Text to synthesize.
    /// </summary>
    string Text { get; }
}
