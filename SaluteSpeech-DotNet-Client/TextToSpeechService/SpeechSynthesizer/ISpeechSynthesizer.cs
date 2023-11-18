namespace TextToSpeechService.SpeechSynthesizer;

public interface ISpeechSynthesizer
{
    public Task<Stream> GenerateAsync(string text);
}
