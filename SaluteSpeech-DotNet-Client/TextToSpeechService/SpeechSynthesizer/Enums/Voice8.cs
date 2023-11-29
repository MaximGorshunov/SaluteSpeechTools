using System.ComponentModel;

namespace TextToSpeechService.SpeechSynthesizer.Enums;

/// <summary>
/// Voice code that will be used for synthesis. Audio sampling frequency is 8 kHz. Intended for use in telephony.
/// </summary>
public enum Voice8
{
    [Description("Nec_8000")] Nec = 0,
    [Description("Bys_8000")] Bys = 1,
    [Description("May_8000")] May = 2,
    [Description("Tur_8000")] Tur = 3,
    [Description("Ost_8000")] Ost = 4,
    [Description("Pon_8000")] Pon = 5
}
