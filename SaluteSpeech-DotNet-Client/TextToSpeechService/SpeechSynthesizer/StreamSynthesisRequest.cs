using TextToSpeechService.Helpers;
using TextToSpeechService.SpeechSynthesizer.Enums;

namespace TextToSpeechService.SpeechSynthesizer;

public class StreamSynthesisRequest : ISynthesisRequest
{
    public StreamSynthesisRequest(string text)
    {   
        AudioEncoding = AudioEncoding.Wav;
        ContentType = ContentType.Text;
        Voice = EnumHelper.GetEnumDescription(Voice8.Nec);
        Text = text ?? throw new ArgumentNullException(nameof(text));    
    }
    
    public StreamSynthesisRequest(AudioEncoding audioEncoding, ContentType contentType, Voice8 voice, string text)
    {
        AudioEncoding = audioEncoding;
        ContentType = contentType;
        Voice = EnumHelper.GetEnumDescription(voice);
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }
        
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
