using System.ComponentModel;

namespace TextToSpeechService.SpeechSynthesizer.Enums;

/// <summary>
/// Audio format for the synthesized audio content.
/// </summary>
public enum AudioEncoding
{
    /// <summary>
    /// AUDIO_ENCODING_UNSPECIFIED encoding format.
    /// </summary>
    [Description("AUDIO_ENCODING_UNSPECIFIED")] AudioEncodingUnspecified = 0,
    /// <summary>
    /// PCM_S16LE encoding format.
    /// </summary>
    [Description("PCM_S16LE")] PcmS16Le = 1,
    /// <summary>
    /// OPUS encoding format.
    /// </summary>
    [Description("OPUS")] Opus = 2,
    /// <summary>
    /// WAV encoding format.
    /// </summary>
    [Description("WAV")] Wav = 3
}
