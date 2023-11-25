using TextToSpeechService.SpeechSynthesizer.Enums;

namespace TextToSpeechService.SpeechSynthesizer;

public interface ISynthesisRequest
{
    AudioEncoding AudioEncoding { get; }
    ContentType ContentType { get; }
    string Voice { get; }
    string Text { get; }
}
