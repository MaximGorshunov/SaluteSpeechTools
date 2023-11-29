using TextToSpeechService.Helpers;
using TextToSpeechService.SpeechSynthesizer.Enums;

namespace TextToSpeechService.SpeechSynthesizer;

/// <summary>
/// Contains params of stream synthesis request.
/// </summary>
public class StreamSynthesisRequest : ISynthesisRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamSynthesisRequest"/> class. Audio encoding set to WAV, ContentType set to TEXT, Voice set to Nec_24000.
    /// </summary>
    /// <param name="text">Text to synthesize.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
    public StreamSynthesisRequest(string text)
    {   
        AudioEncoding = AudioEncoding.Wav;
        ContentType = ContentType.Text;
        Voice = EnumHelper.GetEnumDescription(Voice24.Nec);
        Text = text ?? throw new ArgumentNullException(nameof(text));    
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamSynthesisRequest"/> class with audio sampling frequency equal 8 kHz.
    /// </summary>
    /// <param name="audioEncoding">Audio format for the synthesized audio content.</param>
    /// <param name="contentType">The format of the data sent in the request body.</param>
    /// <param name="voice">Voice code that will be used for synthesis.</param>
    /// <param name="text">Text to synthesize.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
    public StreamSynthesisRequest(AudioEncoding audioEncoding, ContentType contentType, Voice8 voice, string text)
    {
        AudioEncoding = audioEncoding;
        ContentType = contentType;
        Voice = EnumHelper.GetEnumDescription(voice);
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }
     
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamSynthesisRequest"/> class with audio sampling frequency equal 24 kHz.
    /// </summary>
    /// <param name="audioEncoding">Audio format for the synthesized audio content.</param>
    /// <param name="contentType">The format of the data sent in the request body.</param>
    /// <param name="voice">Voice code that will be used for synthesis.</param>
    /// <param name="text">Text to synthesize.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
    public StreamSynthesisRequest(AudioEncoding audioEncoding, ContentType contentType, Voice24 voice, string text)
    {
        AudioEncoding = audioEncoding;
        ContentType = contentType;
        Voice = EnumHelper.GetEnumDescription(voice);
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }
        
    public AudioEncoding AudioEncoding { get; }
    public ContentType ContentType { get; }
    public string Voice { get; }
    public string Text { get; }
}
