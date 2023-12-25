using System.ComponentModel;

namespace TextToSpeechService.SpeechSynthesizer.Enums;

/// <summary>
/// Voice code that will be used for synthesis. Audio sampling frequency is 8 kHz. Intended for use in telephony.
/// </summary>
public enum Voice8
{
    /// <summary>
    /// Woman's russian voice Natalia (Nec_8000).
    /// </summary>
    [Description("Nec_8000")] Nec = 0,
    /// <summary>
    /// Man's russian voice Boris (Bys_8000).
    /// </summary>
    [Description("Bys_8000")] Bys = 1,
    /// <summary>
    /// Woman's russian voice Marfa (May_8000).
    /// </summary>
    [Description("May_8000")] May = 2,
    /// <summary>
    /// Man's russian voice Taras (Tur_8000).
    /// </summary>
    [Description("Tur_8000")] Tur = 3,
    /// <summary>
    /// Woman's russian voice Alexandra (Ost_8000).
    /// </summary>
    [Description("Ost_8000")] Ost = 4,
    /// <summary>
    /// Man's russian voice Pon (Pon_8000).
    /// </summary>
    [Description("Pon_8000")] Pon = 5
}
