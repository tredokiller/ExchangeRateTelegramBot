using Infrastructure.Models;
using Infrastructure.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Infrastructure.Tests.Models;


[TestClass]
public class MessageHandlerTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var model = new MessageHandler(null);
    }
    
    
    [TestMethod]
    public void ConstructorSuccessTest()
    {
        var parserService = new JsonParserService(new AppSettings());
        
        var model = new MessageHandler(parserService);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void HandleMessageThrowExceptionTest()
    {
        var parserService = new JsonParserService(new AppSettings());
        var model = new MessageHandler(parserService);
        

        model.HandleMessage(null, null);
    }
    
    
    [TestMethod]
    [DataRow("/start")]
    [DataRow("USD")]
    [DataRow("USD 12.01.2020")]
    public void HandleMessageSuccessTest(string messageText)
    {
        var parserService = new JsonParserService(new AppSettings());
        var model = new MessageHandler(parserService);

        var tokenBot = "5940614812:AAGeYkg9AssR4ivegSl4dV6hBQqP4kSWRPA";

        ITelegramBotClient client = new TelegramBotClient(tokenBot);
        
        

        var message = new Message();
        
        message.Text = messageText;
        message.Chat = new Chat();
        message.Chat.Id = 0;
        
        
        model.HandleMessage(message , client);
    }
}