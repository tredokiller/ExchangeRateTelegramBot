namespace Infrastructure.Services.Interfaces
{
    public interface IPrivatBankHttpClient
    {
        public string DownloadString(string text);
    }
}