using Infrastructure.Models;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class JsonParserService : IJsonParserService
{
    private readonly AppSettings _settings;
    private readonly IPrivatBankHttpClient _privatBankHttpClient;


    public JsonParserService(AppSettings settings , IPrivatBankHttpClient privatBankHttpClient = null)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _privatBankHttpClient = privatBankHttpClient ?? new PrivatBankHttpClient();
    }
    
    
    public ExchangeRateRootModel ParseToExchangeRate(string data)
    {
        if (String.IsNullOrEmpty(data))
        {
            throw new ArgumentNullException(nameof(data));
        }
        
        var json = _privatBankHttpClient.DownloadString(_settings.ApiUrl+ data);
        
        ExchangeRateRootModel rate = JsonConvert.DeserializeObject<ExchangeRateRootModel>(json);

        return rate;
    }
}