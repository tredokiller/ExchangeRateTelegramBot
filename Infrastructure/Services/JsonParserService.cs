using Infrastructure.Models;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class JsonParserService : IJsonParserService
{
    private AppSettings _settings;


    public JsonParserService(AppSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    
    public ExchangeRateRootModel ParseToExchangeRate(string data)
    {
        if (String.IsNullOrEmpty(data))
        {
            throw new ArgumentNullException(nameof(data));
        }
        
        var json = new HttpClient().GetStringAsync(_settings.ApiUrl+ data).Result;

        ExchangeRateRootModel rate = JsonConvert.DeserializeObject<ExchangeRateRootModel>(json);

        return rate;
    }
}