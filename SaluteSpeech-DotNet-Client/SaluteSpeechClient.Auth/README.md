# SaluteSpeechClient.Auth

## Description

SaluteSpeechClient.Auth is a library that provides authentication for the [SaluteSpeech API](https://developers.sber.ru/docs/ru/salutespeech/category-overview).

## Installation

The SaluteSpeechClient.Auth library can be installed via NuGet for use in your project, but if you planing
to use [SaluteSpeechClient.TextToSpeechService](../SaluteSpeechClient.TextToSpeechService) it already includes the SaluteSpeechClient.Auth library.

`dotnet add package SaluteSpeechClient.Auth --version 1.0.0`

## Usage

Before using it, you need to obtain a secret key for API access.
To obtain authorization data:

* Create a project [SaluteSpeech](https://developers.sber.ru/docs/ru/salutespeech/integration).
* Submit the project for moderation.

After moderation is complete, you will receive access to authorization data:
a field will appear on the page where you can copy the `Client Id`, and a button to generate the `Client Secret`.
The `Client Secret` is displayed only once, so it needs to be saved before use.

Create a new object of type [TokenProvider](./TokenProvider.cs)
in the `SaluteSpeechClient.Auth` namespace and pass the `Client Secret` to its constructor.
You can also pass a typed logger `ILogger<TokenProvider>` to the constructor if necessary.

```csharp
using SaluteSpeechClient.Auth;

ITokenProvider tokenProvider = new TokenProvider("Client Secret");
```

To get the current token, use the [GetTokenAsync](./TokenProvider.cs) method.

```csharp
var token = await tokenProvider.GetTokenAsync();
```
