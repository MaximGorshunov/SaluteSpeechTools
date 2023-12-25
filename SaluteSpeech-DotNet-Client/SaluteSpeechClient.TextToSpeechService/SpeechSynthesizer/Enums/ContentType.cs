using System.ComponentModel;

namespace SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer.Enums;

/// <summary>
/// The format of the data sent in the request body.
/// </summary>
public enum ContentType
{
    /// <summary>
    /// Text format without SSML tags.
    /// </summary>
    [Description("TEXT")] Text = 0,
    /// <summary>
    /// Text format with SSML tags.
    /// </summary>
    [Description("SSML")] Ssml = 1
}
