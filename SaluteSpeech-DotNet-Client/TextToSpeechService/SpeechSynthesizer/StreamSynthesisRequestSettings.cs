using TextToSpeechService.Helpers;
using TextToSpeechService.SpeechSynthesizer.Enums;

namespace TextToSpeechService.SpeechSynthesizer;

/// <summary>
/// Contains synthesis request settings.
/// </summary>
public class StreamSynthesisRequestSettings : ISynthesisRequestSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamSynthesisRequestSettings"/> class with default values.
    /// <see cref="AudioEncoding"/> set to WAV, <see cref="ContentType"/> set to TEXT, <see cref="Voice"/> set to Nec_24000.
    /// </summary>
    public StreamSynthesisRequestSettings()
    {
        AudioEncoding = AudioEncoding.Wav;
        ContentType = ContentType.Text;
        Voice = EnumHelper.GetEnumDescription(Voice24.Nec);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StreamSynthesisRequestSettings"/> class.
    /// </summary>
    /// <param name="audioEncoding">Audio format for the synthesized audio content.</param>
    /// <param name="contentType">The format of the data sent in the request body.</param>
    /// <param name="voice">Voice code that will be used for synthesis. Audio sampling frequency equal 24 kHz.</param>
    public StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice24 voice)
    {
        AudioEncoding = audioEncoding;
        ContentType = contentType;
        Voice = EnumHelper.GetEnumDescription(voice);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamSynthesisRequestSettings"/> class.
    /// </summary>
    /// <param name="audioEncoding">Audio format for the synthesized audio content.</param>
    /// <param name="contentType">The format of the data sent in the request body.</param>
    /// <param name="voice">Voice code that will be used for synthesis. Audio sampling frequency equal 8 kHz.</param>
    public StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice8 voice)
    {
        AudioEncoding = audioEncoding;
        ContentType = contentType;
        Voice = EnumHelper.GetEnumDescription(voice);
    }
    
    public AudioEncoding AudioEncoding { get; set; }
    public ContentType ContentType { get; set; }
    public string Voice { get; private set; }
    
    public void SetVoice(Voice24 voice) => Voice = EnumHelper.GetEnumDescription(voice);
    public void SetVoice(Voice8 voice) => Voice = EnumHelper.GetEnumDescription(voice);
}
