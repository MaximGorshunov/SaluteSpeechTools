using Xunit;
using Microsoft.Extensions.Configuration;

namespace Auth.Tests;

public class TokenProviderTests
{   
    private static readonly IConfigurationRoot s_config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    private static readonly string s_secretKey = s_config["ApiKeys:SecretKey"] ?? throw new ArgumentNullException(nameof(s_secretKey));
    
    [Fact]
    public async Task GetToken()
    {
        var tokenProvider = new TokenProvider(s_secretKey);
        var cancellationToken = CancellationToken.None;
        string result = await tokenProvider.GetTokenAsync(cancellationToken).ConfigureAwait(false);
        Assert.NotEmpty(result);
    }
    
    [Fact] 
    public async Task GetTokenWithWrongSecretKey()
    {
        var tokenProvider = new TokenProvider("WrongSecretKey");
        var cancellationToken = CancellationToken.None;
        await Assert.ThrowsAsync<HttpRequestException>(async () => 
            await tokenProvider.GetTokenAsync(cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);
    }

    [Fact] public async Task GetTokenMultitask()
    {
        var tokenProvider = new TokenProvider(s_secretKey);
        var cancellationToken = CancellationToken.None;
        var tasks = new List<Task<string>>
        {
            Task.Run(() => tokenProvider.GetTokenAsync(cancellationToken), cancellationToken),
            Task.Run(() => tokenProvider.GetTokenAsync(cancellationToken), cancellationToken),
            Task.Run(() => tokenProvider.GetTokenAsync(cancellationToken), cancellationToken),
            Task.Run(() => tokenProvider.GetTokenAsync(cancellationToken), cancellationToken)
        };
        string[] tokens = await Task.WhenAll(tasks).ConfigureAwait(false);
        Assert.All(tokens, t => Assert.Equal(tokens[0], t));
    }
}

