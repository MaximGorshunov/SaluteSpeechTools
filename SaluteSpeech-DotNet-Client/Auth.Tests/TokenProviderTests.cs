using Xunit;
using Microsoft.Extensions.Configuration;

namespace Auth.Tests;

public class TokenProviderTests
{   
    private static readonly IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    private static readonly string SecretKey = Config["ApiKeys:SecretKey"] ?? throw new ArgumentNullException(nameof(SecretKey));
    
    [Fact]
    public async Task GetToken()
    {
        var tokenProvider = new TokenProvider(SecretKey);
        var cancellationToken = CancellationToken.None;
        var result = await tokenProvider.GetTokenAsync(cancellationToken);
        Assert.NotEmpty(result);
    }
    
    [Fact] 
    public async Task GetToken_WithWrongSecretKey()
    {
        var tokenProvider = new TokenProvider("WrongSecretKey");
        var cancellationToken = CancellationToken.None;
        await Assert.ThrowsAsync<HttpRequestException>(async () => await tokenProvider.GetTokenAsync(cancellationToken));
    }

    [Fact] public async Task GetToken_Multitask()
    {
        var tokenProvider = new TokenProvider(SecretKey);
        var cancellationToken = CancellationToken.None;
        var tasks = new List<Task<string>>
        {
            Task.Run(() => tokenProvider.GetTokenAsync(cancellationToken)),
            Task.Run(() => tokenProvider.GetTokenAsync(cancellationToken)),
            Task.Run(() => tokenProvider.GetTokenAsync(cancellationToken)),
            Task.Run(() => tokenProvider.GetTokenAsync(cancellationToken))
        };
        var tokens = await Task.WhenAll(tasks);
        Assert.All(tokens, t => Assert.Equal(tokens[0], t));
    }
}

