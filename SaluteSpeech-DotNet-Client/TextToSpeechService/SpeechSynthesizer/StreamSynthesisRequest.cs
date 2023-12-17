using GrpcSmartSpeech;

namespace TextToSpeechService.SpeechSynthesizer;

/// <summary>
/// Contains params of stream synthesis request.
/// </summary>
public class StreamSynthesisRequest : ISynthesisRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamSynthesisRequest"/> class. Audio encoding set to WAV, ContentType set to TEXT, Voice set to Nec_24000.
    /// </summary>
    /// <param name="settings">Synthesis request settings.</param>
    /// <param name="text">Text to synthesize.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
    public StreamSynthesisRequest(ISynthesisRequestSettings settings, string text)
    {   
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        Text = text ?? throw new ArgumentNullException(nameof(text));    
    }
    
    /// <summary>
    /// Synthesis request settings.
    /// </summary>
    public ISynthesisRequestSettings Settings { get; }
    
    /// <summary>
    /// Text to synthesize.
    /// </summary>
    public string Text { get; }
}
