using Infrastructure.Models;
using Infrastructure.Services.Interfaces;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class JsonParserService : IJsonParserService
{
    private readonly AppSettings _settings;
    private readonly IPrivatBankHttpClient _privatBankHttpClient;


    public JsonParserService(AppSettings settings, IPrivatBankHttpClient privatBankHttpClient)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _privatBankHttpClient = privatBankHttpClient ?? throw new ArgumentNullException(nameof(privatBankHttpClient));
    }


    public ExchangeRateRootModel ParseToExchangeRate(string data)
    {
        if (String.IsNullOrEmpty(data))
        {
            throw new ArgumentNullException(nameof(data));
        }

        var json = _privatBankHttpClient.DownloadString(_settings.ApiUrl + data);

        ExchangeRateRootModel rate = JsonConvert.DeserializeObject<ExchangeRateRootModel>(json);

        return rate;
    }
}