# SaluteSpeechTools

## Description

SaluteSpeechTools is a comprehensive project that includes a set of .NET libraries and applications for speech synthesis and recognition.
The libraries, collected in the SaluteSpeech-DotNet-Client subproject, provide access to the methods of the [SaluteSpeech API](https://developers.sber.ru/docs/ru/salutespeech/category-overview) for speech synthesis and recognition.

The [SaluteSpeechClient.Auth](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth) library is responsible for authentication,
while the [SaluteSpeechClient.TextToSpeechService](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService) library provides access to the API methods for speech synthesis. In the current version, only streaming speech synthesis is available, but synchronous and asynchronous synthesis is planned for the future. There are plans to create an additional library for working with the speech recognition API methods.

## Installation

### SaluteSpeech-DotNet-Client

The [SaluteSpeechClient.Auth](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth)
and [SaluteSpeechClient.TextToSpeechService](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService) libraries can be
installed via NuGet for use in your project.
[SaluteSpeechClient.TextToSpeechService](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService) already includes the [SaluteSpeechClient.Auth](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth) library.

`dotnet add package SaluteSpeechClient.Auth --version 1.0.0`

`dotnet add package SaluteSpeechClient.TextToSpeechService --version 1.0.0`

## Usage

### SaluteSpeech-DotNet-Client

### Authentication

To authenticate, use [SaluteSpeechClient.Auth](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth).
Before using it, you need to obtain a secret key for API access.
To obtain authorization data:

* Create a project [SaluteSpeech](https://developers.sber.ru/docs/ru/salutespeech/integration).
* Submit the project for moderation.

After moderation is complete, you will receive access to authorization data:
a field will appear on the page where you can copy the `Client Id`, and a button to generate the `Client Secret`.
The `Client Secret` is displayed only once, so it needs to be saved before use.

Create a new object of type [TokenProvider](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth/TokenProvider.cs)
in the `SaluteSpeechClient.Auth` namespace and pass the `Client Secret` to its constructor.
You can also pass a typed logger `ILogger<TokenProvider>` to the constructor if necessary.

```csharp
using SaluteSpeechClient.Auth;

ITokenProvider tokenProvider = new TokenProvider("Client Secret");
```

To get the current token, use the [GetTokenAsync](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth/TokenProvider.cs) method.

```csharp
var token = await tokenProvider.GetTokenAsync();
```

### Speech Synthesis

To perform speech synthesis, use [SaluteSpeechClient.TextToSpeechService](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService).

#### Stream Speech Synthesis

For stream speech synthesis, use [StreamSpeechSynthesizer](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/StreamSpeechSynthesizer.cs).
When initializing the object, pass `ITokenProvider` to the constructor and a typed logger `ILogger<StreamSpeechSynthesizer>` if necessary:

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISpeechSynthesizer streamSynthesizer = new StreamSpeechSynthesizer(tokenProvider);
```

To perform speech synthesis, use the [SynthesizeAsync](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/StreamSpeechSynthesizer.cs) method.

```csharp
Stream result = await streamSynthesizer.SynthesizeAsync(request, cancellationToken);
```

To create a request, use the [StreamSynthesisRequest](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/StreamSynthesisRequest.cs) class, which takes the request settings `ISynthesisRequestSettings` and the text to synthesize in its constructor:

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISynthesisRequest request = new SynthesisRequest(requestSettings, textToSynthesize);
```

To set the request settings for stream speech synthesis, use the [StreamSynthesisRequestSettings](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/StreamSynthesisRequestSettings.cs) class.
The default parameterless constructor sets the default request settings (`AudioEncoding = WAV`, `ContentType = Text`, `Voice = Nec_24000`):

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISynthesisRequestSettings requestSettings = new StreamSynthesisRequestSettings();
```

The constructors `StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice24 voice)` and `StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice8 voice)` set specific request settings.
In the first case, voice models with a frequency of 24 kHz are used, and in the second case, 8 kHz.

Available voices - [Voice24](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/Enums/Voice24.cs) and [Voice8](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/Enums/Voice8.cs).
Examples of voices are available [here](https://developers.sber.ru/docs/ru/salutespeech/synthesis/voices).
