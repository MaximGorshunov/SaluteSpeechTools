using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Auth;

[SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates")]
public class TokenProvider : ITokenProvider
{
    private readonly ILogger<TokenProvider>? _logger;
    private readonly string _secretKey;
    
    private DateTimeOffset _exp;
    private string _token = string.Empty;

    public TokenProvider(string secretKey, ILogger<TokenProvider>? logger = null)
    {
        _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
        _logger = logger;
    }

    /// <summary>
    /// Generates a token, saves and return it if it's empty or expired, else returns current token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> GetToken(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var semaphoreSlim = new SemaphoreSlim(1, 1);
        
        await semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
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
            _logger?.Log(LogLevel.Error, ex, "Failed to generate token. Current token: {@token}. Current exp: {@exp}.", _token, _exp);
            return string.Empty;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task GenerateTokenAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        using HttpClient httpClient = new();
        
        const string requestBody = "scope=SALUTE_SPEECH_PERS";
        const string endpoint = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";

        httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + _secretKey);
        httpClient.DefaultRequestHeaders.Add("RqUID", Guid.NewGuid().ToString());

        using HttpContent httpContent = new StringContent(requestBody);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        HttpResponseMessage response = await httpClient.PostAsync(new Uri(endpoint), httpContent, cancellationToken).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        JObject json = JObject.Parse(responseBody);
        _token = json.Value<string>("access_token") ?? string.Empty;
        _exp = DateTimeOffset.FromUnixTimeMilliseconds(json.Value<long>("expires_at"));
    }
}
