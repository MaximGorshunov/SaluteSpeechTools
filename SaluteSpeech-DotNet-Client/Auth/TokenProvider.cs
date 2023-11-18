using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

[assembly:InternalsVisibleTo("AuthTest")]

namespace Auth;

internal class TokenProvider : ITokenProvider
{
    private readonly string _secretKey;
    private string _token = string.Empty;
    private DateTimeOffset _exp;

    public TokenProvider(string secretKey) // add logger
    {
        _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
    }

    public async Task<string> GetToken(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if (string.IsNullOrEmpty(_token) || _exp < DateTimeOffset.UtcNow)
        {
            await GenerateTokenAsync(CancellationToken.None).ConfigureAwait(false);
        }
        
        return _token;
    }

    private async Task GenerateTokenAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        using HttpClient httpClient = new();
        
        const string requestBody = "scope=SALUTE_SPEECH_PERS";
        const string endpoint = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";

        httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + _secretKey);
        httpClient.DefaultRequestHeaders.Add("RqUID", "6f0b1291-c7f3-43c6-bb2e-9f3efb2dc98e"); //TODO: generate uuid
        //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");

        using HttpContent httpContent = new StringContent(requestBody);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        HttpResponseMessage response = await httpClient.PostAsync(new Uri(endpoint), httpContent, cancellationToken).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        JObject json = JObject.Parse(responseBody);
        _token = json.Value<string>("access_token") ?? string.Empty;
        _exp = json.Value<DateTime>("expires_at");
    }
}
