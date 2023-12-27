# SaluteSpeechClient.Auth

## Описание

SaluteSpeechClient.Auth - это библиотека, которая предоставляет аутентификацию для [SaluteSpeech API](https://developers.sber.ru/docs/ru/salutespeech/category-overview).

## Установка

Библиотеку SaluteSpeechClient.Auth можно установить через NuGet для использования в своем проекте, но если вы планируете
использовать [SaluteSpeechClient.TextToSpeechService](../SaluteSpeechClient.TextToSpeechService),
то в нее уже включена библиотека SaluteSpeechClient.Auth.

`dotnet add package SaluteSpeechClient.Auth --version 1.0.0`

## Использование

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