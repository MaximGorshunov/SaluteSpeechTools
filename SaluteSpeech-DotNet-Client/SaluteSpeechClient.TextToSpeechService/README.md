# SaluteSpeechClient.TextToSpeechService

## Description

SaluteSpeechClient.TextToSpeechService library provides access to the [SaluteSpeech API](https://developers.sber.ru/docs/ru/salutespeech/synthesis/overview) methods for speech synthesis. In the current version, only streaming speech synthesis is available, but synchronous and asynchronous synthesis is planned for the future.

## Installation

The SaluteSpeechClient.TextToSpeechService library can be
installed via NuGet for use in your project, it already includes the [SaluteSpeechClient.Auth](../SaluteSpeechClient.Auth) library.

`dotnet add package SaluteSpeechClient.TextToSpeechService --version 1.0.0`

## Usage

### Stream Speech Synthesis

For stream speech synthesis, use [StreamSpeechSynthesizer](./SpeechSynthesizer/StreamSpeechSynthesizer.cs).
When initializing the object, pass `ITokenProvider` (check [SaluteSpeechClient.Auth](../SaluteSpeechClient.Auth)) to the constructor and a typed logger `ILogger<StreamSpeechSynthesizer>` if necessary:

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISpeechSynthesizer streamSynthesizer = new StreamSpeechSynthesizer(tokenProvider);
```

To perform speech synthesis, use the [SynthesizeAsync](./SpeechSynthesizer/StreamSpeechSynthesizer.cs) method.

```csharp
Stream result = await streamSynthesizer.SynthesizeAsync(request, cancellationToken);
```

To create a request, use the [StreamSynthesisRequest](./SpeechSynthesizer/StreamSynthesisRequest.cs) class, which takes the request settings `ISynthesisRequestSettings` and the text to synthesize in its constructor:

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISynthesisRequest request = new SynthesisRequest(requestSettings, textToSynthesize);
```

To set the request settings for stream speech synthesis, use the [StreamSynthesisRequestSettings](./SpeechSynthesizer/StreamSynthesisRequestSettings.cs) class.
The default parameterless constructor sets the default request settings (`AudioEncoding = WAV`, `ContentType = Text`, `Voice = Nec_24000`):

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISynthesisRequestSettings requestSettings = new StreamSynthesisRequestSettings();
```

The constructors `StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice24 voice)` and `StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice8 voice)` set specific request settings.
In the first case, voice models with a frequency of 24 kHz are used, and in the second case, 8 kHz.

Available voices - [Voice24](./SpeechSynthesizer/Enums/Voice24.cs) and [Voice8](./SpeechSynthesizer/Enums/Voice8.cs).
Examples of voices are available [here](https://developers.sber.ru/docs/ru/salutespeech/synthesis/voices).
