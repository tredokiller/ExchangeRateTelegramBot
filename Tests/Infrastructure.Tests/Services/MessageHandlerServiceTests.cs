using Infrastructure.Models;
using Infrastructure.Services;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;

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
    public void ConstructorSuccessTest()
    {
        var parserService = new JsonParserService(new AppSettings());
        var service = new MessageHandlerService(parserService);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void HandleMessageThrowExceptionTests()
    {
        var parserService = new JsonParserService(new AppSettings());
        var service = new MessageHandlerService(parserService);

        var telegramClient = new Mock<ITelegramBotClient>();
        
        service.HandleMessage(new Message(), null);
        service.HandleMessage(null, telegramClient.Object);
    }
    
    
}