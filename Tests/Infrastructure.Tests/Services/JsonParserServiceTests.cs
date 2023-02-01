using FluentAssertions;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace Infrastructure.Tests.Services;

[TestClass]
public class JsonParserServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var service = new JsonParserService(null , null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ParseToExchangeRateThrowExceptionTest()
    {
        var service = new JsonParserService(new AppSettings() , new PrivatBankHttpClient(new HttpClient()));

        service.ParseToExchangeRate(null);
    }


    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ParseToExchangeRateDeserializationExceptionTest()
    {
        var service = new JsonParserService(new AppSettings() , new PrivatBankHttpClient(new HttpClient()));

        var json = service.ParseToExchangeRate("asdasfasdfsd");
    }


    [TestMethod]
    [DataRow("12.02.2020")]
    [DataRow("11.05.2019")]
    [DataRow("07.07.2022")]
    public void ParseToExchangeRateSuccessTest(string data)
    {
        var settings = new AppSettings();
        settings.ApiUrl = "https://api.privatbank.ua/p24api/exchange_rates?date=";

        var service = new JsonParserService(settings , new PrivatBankHttpClient(new HttpClient()));

        var trueJson = new HttpClient().GetStringAsync(settings.ApiUrl + data).Result;
        var trueModel = JsonConvert.DeserializeObject<ExchangeRateRootModel>(trueJson);

        var model = service.ParseToExchangeRate(data);


        trueModel.Should().BeEquivalentTo(model);
    }
}