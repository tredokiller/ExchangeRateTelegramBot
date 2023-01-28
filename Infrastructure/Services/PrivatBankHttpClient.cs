namespace Infrastructure.Services;

public class PrivatBankHttpClient : IPrivatBankHttpClient
{

    private readonly HttpClient _httpClient; 


    public PrivatBankHttpClient(HttpClient client = null)
    {
        _httpClient = client ?? new HttpClient();
    }
    public string DownloadString(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }
        
        return _httpClient.GetStringAsync(text).Result;
    }
}