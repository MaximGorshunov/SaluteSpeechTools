using System.ComponentModel;

namespace TextToSpeechService.SpeechSynthesizer.Enums;

/// <summary>
/// Audio format for the synthesized audio content.
/// </summary>
public enum AudioEncoding
{
    [Description("AUDIO_ENCODING_UNSPECIFIED")] AudioEncodingUnspecified = 0,
    [Description("PCM_S16LE")] PcmS16Le = 1,
    [Description("OPUS")] Opus = 2,
    [Description("WAV")] Wav = 3
}
