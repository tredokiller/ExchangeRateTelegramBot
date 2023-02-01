using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IJsonParserService
{
    public ExchangeRateRootModel ParseToExchangeRate(string data);
}