using System.ComponentModel;

namespace TextToSpeechService.SpeechSynthesizer.Enums;

/// <summary>
/// The format of the data sent in the request body.
/// </summary>
public enum ContentType
{
    [Description("TEXT")] Text = 0,
    [Description("SSML")] Ssml = 1
}
