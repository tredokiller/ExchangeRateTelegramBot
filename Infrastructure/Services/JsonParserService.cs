using Infrastructure.Models;

namespace Infrastructure.Services;

public class JsonParserService : IJsonParserService
{

    private readonly JsonParserModel _parserModel;



    public JsonParserService(JsonParserModel parserModel = null)
    {
        _parserModel = parserModel ?? new JsonParserModel();
    }
    
    public ExchangeRateRoot Parse(string data)
    {
        return _parserModel.Parse(data);
    }
}