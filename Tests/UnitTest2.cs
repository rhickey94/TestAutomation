using TestFramework.Driver;

namespace Tests;

[Parallelizable(ParallelScope.All)]
public class Tests2 : TestBase
{
    [SetUp]
    public void TestSetup()
    {
    }

    [Test]
    public void Test1()
    {
        Driver.Driver.Navigate().GoToUrl("https://www.twitch.tv");
    }
    
    [Test]
    public void Test2()
    {
        Driver.Driver.Navigate().GoToUrl("https://www.twitch.tv");
    }
}