using TextToSpeechService.SpeechSynthesizer.Enums;

namespace TextToSpeechService.SpeechSynthesizer;

/// <summary>
/// Contains synthesis request settings.
/// </summary>
public interface ISynthesisRequestSettings
{
    /// <summary>
    /// Audio format for the synthesized audio content.
    /// </summary>
    AudioEncoding AudioEncoding { get; set; }
    /// <summary>
    /// The format of the data sent in the request body.
    /// </summary>
    ContentType ContentType { get; set; }
    /// <summary>
    /// Voice code that will be used for synthesis.
    /// </summary>
    string Voice { get; }
    /// <summary>
    /// Set voice code that will be used for synthesis.
    /// </summary>
    /// <param name="voice">Voice code. Audio sampling frequency is 24 kHz</param>
    void SetVoice(Voice24 voice);
    /// <summary>
    /// Set voice code that will be used for synthesis.
    /// </summary>
    /// <param name="voice">Voice code. Audio sampling frequency is 8 kHz</param>
    void SetVoice(Voice8 voice);
}
