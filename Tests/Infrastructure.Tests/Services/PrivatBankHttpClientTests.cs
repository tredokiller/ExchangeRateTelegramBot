using Infrastructure.Services;

namespace Infrastructure.Tests.Services;

[TestClass]
public class PrivatBankHttpClientTests
{
    
    
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var service = new PrivatBankHttpClient(null);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void DownloadStringThrowExceptionTest()
    {
        var client = new PrivatBankHttpClient(new HttpClient());

        client.DownloadString(null);
    }


    [TestMethod]
    public void DownloadStringSuccessTest()
    {
        var client = new PrivatBankHttpClient(new HttpClient());

        var testingString = "https://api.privatbank.ua/p24api/exchange_rates?date=01.12.2014";

        var trueResult = new HttpClient().GetStringAsync(testingString).Result;

        var result = client.DownloadString(testingString);

        Assert.AreEqual(trueResult, result);
    }
}