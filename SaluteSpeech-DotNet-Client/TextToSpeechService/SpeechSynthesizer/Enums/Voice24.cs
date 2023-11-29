using System.ComponentModel;

namespace TextToSpeechService.SpeechSynthesizer.Enums;

/// <summary>
/// Voice code that will be used for synthesis. Audio sampling frequency is 24 kHz.
/// </summary>
public enum Voice24
{
    [Description("Nec_24000")] Nec = 0,
    [Description("Bys_24000")] Bys = 1,
    [Description("May_24000")] May = 2,
    [Description("Tur_24000")] Tur = 3,
    [Description("Ost_24000")] Ost = 4,
    [Description("Pon_24000")] Pon = 5
}
