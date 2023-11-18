using Auth;

namespace AuthTest;

public class TokenProviderTests
{
    private const string SecretKey = "NTEwZGNkODAtZTgyMS00ZjZkLTljZGQtNGRjNWY4ZDI0ZTBkOmJmMzYyMTVhLTZjNDQtNDhjOS1hZDdhLWY5MWJhNDdmNzhhOQ==";
    private readonly TokenProvider _tokenProvider = new TokenProvider(SecretKey);
    
    [Fact]
    public async Task GetToken()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
    
        // Act
        var result = await _tokenProvider.GetToken(cancellationToken);
    
        // Assert
        Assert.NotEmpty(result);
    }
}

