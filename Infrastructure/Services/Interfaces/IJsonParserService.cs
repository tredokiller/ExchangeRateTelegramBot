using Infrastructure.Models;

namespace Infrastructure.Services;

public interface IJsonParserService
{
    public ExchangeRateRootModel ParseToExchangeRate(string data);
}