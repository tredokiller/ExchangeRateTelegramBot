using Infrastructure.Models;
using Infrastructure.Services;
using Moq;
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
    [ExpectedException(typeof(ArgumentNullException))]
    public void HandleMessageThrowExceptionTest()
    {
        var parserService = new JsonParserService(new AppSettings());
        var model = new MessageHandler(parserService);
        

        model.HandleMessage(null, null);
    }
    
    
    [TestMethod]
    [DataRow("/start" , Messages.HelloMessage)]
    [DataRow("USD" , Messages.InvalidCodeMessage)]
    [DataRow("USD 12.01.2020" , "Курс на купівлю USD на 12.01.2020 становить: 23.75\nКурс на продаж становить: 24.25")]
    [DataRow("USD erttregdf" , Messages.InvalidDataMessage)]
    public void HandleMessageTest(string messageText , string trueAnswer)
    {
        //var tokenBot = "5940614812:AAGeYkg9AssR4ivegSl4dV6hBQqP4kSWRPA";
        var mockedClient = new Mock<ITelegramClient>();

        var message = new Message();
        
        message.Text = messageText;
        message.Chat = new Chat();
        message.Chat.Id = 0;

        mockedClient.Setup(client => client.SendTextMessage(new ChatId(0), messageText));


        var appSettings = new AppSettings();

        appSettings.ApiUrl = "https://api.privatbank.ua/p24api/exchange_rates?date=";
        
        var parserService = new JsonParserService(appSettings);
        var model = new MessageHandler(parserService);

        model.HandleMessage(message , mockedClient.Object);

        mockedClient.Verify(client => client.SendTextMessage(new ChatId(0), trueAnswer) , Times.Once);

    }
}