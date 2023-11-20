namespace Auth;

/// <summary>
/// Provides access to a token
/// </summary>
public interface ITokenProvider
{
    /// <summary>
    /// Returns a token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetToken(CancellationToken cancellationToken);
}

