using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bot.Tests;

[TestClass]
public class BotTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var bot = new Bot(null , null, null, null);
    }
    
    
    
    
    [TestMethod]
    public void ConstructorSuccessTest()
    {
        var communicationservice = new Mock<ICommunication>();

        var messageService = new Mock<IMessageHandlerService>();
        var appSettings = new Mock<AppSettings>();

        var client = new Mock<ITelegramClient>();

        communicationservice.Setup(service => service.ReadLine()).Returns("Something");

        var config = new ConfigurationBuilder();


        config.AddJsonFile("appsettings.json");

        var bot = new Bot(config.Build(), communicationservice.Object , client.Object, messageService.Object );



    }
}