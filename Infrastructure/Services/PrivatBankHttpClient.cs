using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services;

public class PrivatBankHttpClient : IPrivatBankHttpClient
{
    private readonly HttpClient _httpClient;


    public PrivatBankHttpClient(HttpClient client)
    {
        _httpClient = client ?? throw new ArgumentNullException(nameof(client));
    }

    public string DownloadString(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        return _httpClient.GetStringAsync(text).Result;
    }
}