namespace Infrastructure.Services;

public interface IPrivatBankHttpClient
{
    public string DownloadString(string text);
}