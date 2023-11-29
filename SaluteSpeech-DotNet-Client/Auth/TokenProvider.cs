using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Auth;

[SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates")]
public class TokenProvider : ITokenProvider
{
    private static readonly SemaphoreSlim s_semaphoreSlim = new(1);
    private readonly ILogger<TokenProvider>? _logger;
    private readonly string _secretKey;
    
    private DateTimeOffset _exp;
    private string _token = string.Empty;
    
    /// <param name="secretKey">Client secret key.</param>
    /// <param name="logger">Logger to log errors.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="secretKey"/> is null.</exception>
    public TokenProvider(string secretKey, ILogger<TokenProvider>? logger = null)
    {
        _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
        _logger = logger;
    }

    /// <summary>
    /// Generates an access token, saves and returns it if it's empty or expired, else returns current access token.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Access token value.</returns>
    /// <exception cref="HttpRequestException">Thrown when request to generate token failed.</exception>
    public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        await s_semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (string.IsNullOrEmpty(_token) || _exp - DateTimeOffset.UtcNow <= TimeSpan.FromMinutes(1))
            {
                await GenerateTokenAsync(cancellationToken).ConfigureAwait(false);
            }

            return _token;
        }
        catch (HttpRequestException ex)
        {
            _logger?.Log(LogLevel.Error, ex, "Token generate request failed. Secret key: {@secretKey}", _secretKey);
            throw;
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    private async Task GenerateTokenAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        const string requestBody = "scope=SALUTE_SPEECH_PERS";
        const string endpoint = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
        
        string rqUid = Guid.NewGuid().ToString();
        
        try
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + _secretKey);
            httpClient.DefaultRequestHeaders.Add("RqUID", rqUid);

            using HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.PostAsync(new Uri(endpoint), httpContent, cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            JObject json = JObject.Parse(responseBody);
            _token = json.Value<string>("access_token") ?? string.Empty;
            _exp = DateTimeOffset.FromUnixTimeMilliseconds(json.Value<long>("expires_at"));
        }
        catch (HttpRequestException ex)
        {
            _token = string.Empty;
            ex.Data.Add("RqUID", rqUid);
            ex.Data.Add("secret key", _secretKey);
            throw;
        }

    }
}
