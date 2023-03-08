using TestFramework.Driver;

namespace Tests;

[Parallelizable]
public class Tests2 : TestBase
{
    [SetUp]
    public void TestSetup()
    {
    }

    [Test]
    public void Test1()
    {
        driver?.Driver.Navigate().GoToUrl("https://www.twitch.tv");
    }
    
    [Test]
    public void Test2()
    {
        driver?.Driver.Navigate().GoToUrl("https://www.twitch.tv");
    }
}