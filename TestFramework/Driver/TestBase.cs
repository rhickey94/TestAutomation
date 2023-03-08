using NUnit.Framework;

namespace TestFramework.Driver;

public class TestBase
{
    // ReSharper disable once InconsistentNaming
    protected FluentDriver? driver { get; private set; }
    private IDriverContext? _driverContext;

    [SetUp]
    public void Setup()
    {
        _driverContext = new DriverContext();
        driver = new FluentDriver(_driverContext);
    }

    [TearDown]
    public void TearDown()
    {
        driver?.Driver.Quit();
    }
}