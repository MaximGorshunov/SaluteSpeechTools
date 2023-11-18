namespace Auth;

public interface ITokenProvider
{
    Task<string> GetToken(CancellationToken cancellationToken);
}

