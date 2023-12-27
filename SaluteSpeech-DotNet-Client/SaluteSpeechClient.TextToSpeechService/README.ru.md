# SaluteSpeechClient.TextToSpeechService

## Описание

Библиотека SaluteSpeechClient.TextToSpeechService предоставляет доступ к методам [SaluteSpeech API](https://developers.sber.ru/docs/ru/salutespeech/synthesis/overview) для синтеза речи. В текущей версии доступен только потоковый синтез речи, но в планах реализация синхронного и асинхронного синтеза.

## Установка

SaluteSpeechClient.TextToSpeechService может быть
установлен через NuGet для использования в вашем проекте.
SaluteSpeechClient.TextToSpeechService уже включает в себя библиотеку [SaluteSpeechClient.Auth](../SaluteSpeechClient.Auth).

`dotnet add package SaluteSpeechClient.TextToSpeechService --version 1.0.0`

## Использование

### Потоковый синтез речи

Для потокового синтеза речи используйте [StreamSpeechSynthesizer](./SpeechSynthesizer/StreamSpeechSynthesizer.cs).
При инициализации объекта в конструктор передается `ITokenProvider` (см. [SaluteSpeechClient.Auth](../SaluteSpeechClient.Auth)) и типизированный логгер `ILogger<StreamSpeechSynthesizer>` при необходимости:

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISpeechSynthesizer synthesizer = new StreamSpeechSynthesizer(tokenProvider);
```

Для синтеза речи используйте метод [SynthesizeAsync](./SpeechSynthesizer/StreamSpeechSynthesizer.cs).

```csharp
Stream result = await synthesizer.SynthesizeAsync(request, cancellationToken);
```

Для создания запроса ипользуйте класс [StreamSynthesisRequest](./SpeechSynthesizer/StreamSynthesisRequest.cs):

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISynthesisRequest request = new SynthesisRequest(requestSettings, textToSynthesize);
```

Чтобы задать настройки запроса для потокового синтеза речи используйте класс [StreamSynthesisRequestSettings](./SpeechSynthesizer/StreamSynthesisRequestSettings.cs).
Стандартный конструктор без параметров задает стандартные настройки запроса (`AudioEncoding = WAV`, `ContentType = Text`, `Voice = Nec_24000`):

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISynthesisRequestSettings requestSettings = new StreamSynthesisRequestSettings();
```

Конструкторы `StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice24 voice)` 
и `StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice8 voice)` задают конкретные настройки запроса.
В первом случае используются модели голоса с частотой 24 кГц и во втором 8 кГц.

Доступные голоса - [Voice24](./SpeechSynthesizer/Enums/Voice24.cs) и [Voice8](./SpeechSynthesizer/Enums/Voice8.cs).
Примеры голосов доступны по ссылке [здесь](https://developers.sber.ru/docs/ru/salutespeech/synthesis/voices).
