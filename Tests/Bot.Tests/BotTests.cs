using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bot.Tests;

[TestClass]
public class BotTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var bot = new Bot(null, null, null, null);
    }
}