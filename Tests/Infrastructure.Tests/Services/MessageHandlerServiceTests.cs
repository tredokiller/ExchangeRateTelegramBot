using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Tests.Services;

[TestClass]
public class MessageHandlerServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var service = new MessageHandlerService(null);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void HandleMessageThrowExceptionTests()
    {
        var parserService = new JsonParserService(new AppSettings() , new PrivatBankHttpClient(new HttpClient()));
        var service = new MessageHandlerService(parserService);

        service.HandleMessage(null, null);
    }
}