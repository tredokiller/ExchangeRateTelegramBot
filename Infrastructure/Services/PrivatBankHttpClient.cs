namespace Infrastructure.Services;

public class PrivatBankHttpClient : IPrivatBankHttpClient
{
    public string DownloadString(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }
        
        return new HttpClient().GetStringAsync(text).Result;
    }
}