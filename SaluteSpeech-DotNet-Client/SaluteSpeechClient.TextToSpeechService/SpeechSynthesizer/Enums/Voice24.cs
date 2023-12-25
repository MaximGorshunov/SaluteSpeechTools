using System.ComponentModel;

namespace SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer.Enums;

/// <summary>
/// Voice code that will be used for synthesis. Audio sampling frequency is 24 kHz.
/// </summary>
public enum Voice24
{
    /// <summary>
    /// Woman's russian voice Natalia (Nec_24000).
    /// </summary>
    [Description("Nec_24000")] Nec = 0,
    /// <summary>
    /// Man's russian voice Boris (Bys_24000).
    /// </summary>
    [Description("Bys_24000")] Bys = 1,
    /// <summary>
    /// Woman's russian voice Marfa (May_24000).
    /// </summary>
    [Description("May_24000")] May = 2,
    /// <summary>
    /// Man's russian voice Taras (Tur_24000).
    /// </summary>
    [Description("Tur_24000")] Tur = 3,
    /// <summary>
    /// Woman's russian voice Alexandra (Ost_24000).
    /// </summary>
    [Description("Ost_24000")] Ost = 4,
    /// <summary>
    /// Man's russian voice Sergey (Pon_24000).
    /// </summary>
    [Description("Pon_24000")] Pon = 5
}
