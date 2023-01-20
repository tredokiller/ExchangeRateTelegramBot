using Infrastructure.Models;

namespace Infrastructure.Services;

public interface IJsonParserService
{
    public ExchangeRateRoot Parse(string data);
}