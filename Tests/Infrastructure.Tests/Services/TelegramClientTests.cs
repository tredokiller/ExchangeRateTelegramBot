using Infrastructure.Services;

namespace Infrastructure.Tests.Services;

[TestClass]
public class TelegramClientTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExcpetionTest()
    {
        var client = new TelegramClient(null);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void StartReceivingThrowExceptionTest()
    {
        var token = "5940614812:AAGeYkg9AssR4ivegSl4dV6hBQqP4kSWRPA";
        var client = new TelegramClient(token);

        client.StartReceiving(null, null , null, default);
    }
    
    
    [TestMethod]
    public void SendTextMessageThrowExceptionTest()
    {
        var token = "5940614812:AAGeYkg9AssR4ivegSl4dV6hBQqP4kSWRPA";
        var client = new TelegramClient(token);
        
        try
        {
            client.SendTextMessage(null, null);
        }
        // Assert
        catch (Exception ex)
        {
            Assert.AreEqual(typeof(ArgumentNullException), ex.GetType());
        }
        
    }
    
    
    
}