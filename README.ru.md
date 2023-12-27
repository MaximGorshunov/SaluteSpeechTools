# SaluteSpeechTools

## Описание

SaluteSpeechTools - это комплексный проект, включающий в себя набор .NET библиотек и приложений для распознавания и синтеза речи.
Библиотеки, собранные в подпроекте SaluteSpeech-DotNet-Client, предоставляют доступ к методам [API SaluteSpeech](https://developers.sber.ru/docs/ru/salutespeech/category-overview) 
по распознаванию и синтезу речи.

Библиотека [SaluteSpeechClient.Auth](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth) отвечает за авторизацию,
а [SaluteSpeechClient.TextToSpeechService](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService) предоставляет доступ к методам API для синтеза речи. В текущей версии доступен только потоковый синтез речи, но в планах реализация синхронного и асинхронного синтеза. В будущем планируется создание дополнительной библиотеки для работы с методами API по распознаванию речи.

## Установка

### SaluteSpeech-DotNet-Client

Библиотеки [SaluteSpeechClient.Auth](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth)
и [SaluteSpeechClient.TextToSpeechService](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService) могут быть
установленны через NuGet для использования в вашем проекте.
[SaluteSpeechClient.TextToSpeechService](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService) уже включает в себя библиотеку [SaluteSpeechClient.Auth](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth).

`dotnet add package SaluteSpeechClient.Auth --version 1.0.0`

`dotnet add package SaluteSpeechClient.TextToSpeechService --version 1.0.0`

## Использование

### SaluteSpeech-DotNet-Client

### Авторизация

Для авторизации используйте [SaluteSpeechClient.Auth](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth).
Перед использованием необходимо получить секретный ключ для доступа к API. Чтобы получить авторизационные данные:

* Создайте проект [SaluteSpeech](https://developers.sber.ru/docs/ru/salutespeech/integration).
* Отправьте проект на модерацию.

После завершения модерации вы получите доступ к авторизационным данным:
на странице появится поле, где вы сможете скопировать `Client Id`, и кнопка для генерации `Client Secret`.
`Client Secret` отображается только один раз, поэтому его необходимо сохранить перед использованием.

Создайте новый объект типа [TokenProvider](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth/TokenProvider.cs)
пространства имен `SaluteSpeechClient.Auth` и передайте в его конструктор `Client Secret`.
Также при необходимости можно передать в констурктор типизированный логгер `ILogger<TokenProvider>`.

```csharp
using SaluteSpeechClient.Auth;

ITokenProvider tokenProvider = new TokenProvider("Client Secret");
```

Для получения текущего токена используйте метод [GetTokenAsync](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.Auth/TokenProvider.cs).

```csharp
var token = await tokenProvider.GetTokenAsync();
```

### Синтез речи

Для синтеза речи используйте [SaluteSpeechClient.TextToSpeechService](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService).

#### Потоковый синтез речи

Для потокового синтеза речи используйте [StreamSpeechSynthesizer](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/StreamSpeechSynthesizer.cs).
При инициализации объекта в конструктор передается `ITokenProvider` и типизированный логгер `ILogger<StreamSpeechSynthesizer>` при необходимости:

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISpeechSynthesizer synthesizer = new StreamSpeechSynthesizer(tokenProvider);
```

Для синтеза речи используйте метод [SynthesizeAsync](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/StreamSpeechSynthesizer.cs).

```csharp
Stream result = await synthesizer.SynthesizeAsync(request, cancellationToken);
```

Для создания запроса ипользуйте класс [StreamSynthesisRequest](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/StreamSynthesisRequest.cs):

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISynthesisRequest request = new SynthesisRequest(requestSettings, textToSynthesize);
```

Чтобы задать настройки запроса для потокового синтеза речи используйте класс [StreamSynthesisRequestSettings](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/StreamSynthesisRequestSettings.cs).
Стандартный конструктор без параметров задает стандартные настройки запроса (`AudioEncoding = WAV`, `ContentType = Text`, `Voice = Nec_24000`):

```csharp
using SaluteSpeechClient.TextToSpeechService.SpeechSynthesizer;

ISynthesisRequestSettings requestSettings = new StreamSynthesisRequestSettings();
```

Конструкторы `StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice24 voice)` 
и `StreamSynthesisRequestSettings(AudioEncoding audioEncoding, ContentType contentType, Voice8 voice)` задают конкретные настройки запроса.
В первом случае используются модели голоса с частотой 24 кГц и во втором 8 кГц.

Доступные голоса - [Voice24](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/Enums/Voice24.cs) и [Voice8](./SaluteSpeech-DotNet-Client/SaluteSpeechClient.TextToSpeechService/SpeechSynthesizer/Enums/Voice8.cs).
Примеры голосов доступны по ссылке [здесь](https://developers.sber.ru/docs/ru/salutespeech/synthesis/voices).
