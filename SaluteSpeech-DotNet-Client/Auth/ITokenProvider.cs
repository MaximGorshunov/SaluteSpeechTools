namespace Auth;

/// <summary>
/// Provides access to an access token.
/// </summary>
public interface ITokenProvider
{
    /// <summary>
    /// Returns an access token.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Access token value.</returns>
    Task<string> GetToken(CancellationToken cancellationToken);
}

