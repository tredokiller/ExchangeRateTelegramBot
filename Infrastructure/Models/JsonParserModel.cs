using System.Net;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace Infrastructure.Models;

public class JsonParserModel
{
    private const string ApiUrl = "https://api.privatbank.ua/p24api/exchange_rates?date=";
    
    
    public ExchangeRateRoot Parse(string data)
    {
        var json = new WebClient().DownloadString(ApiUrl + data);

        ExchangeRateRoot rate = JsonConvert.DeserializeObject<ExchangeRateRoot>(json);

        return rate;
    }
}