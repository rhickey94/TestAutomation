using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestFramework.Config;

namespace TestFramework.Driver;

public class FluentDriver : IFluentDriver
{
    private readonly TestSettings _testSettings;
    private readonly IDriverContext _driverContext;
    private readonly Lazy<WebDriverWait> _webDriverWait;

    public IWebDriver Driver => _driverContext.Driver;
    
    public FluentDriver(IDriverContext driverContext)
    {
        _testSettings = driverContext.TestSettings;
        _driverContext = driverContext;
        _webDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
    }
    
    public IWebElement FindElement(By elementLocator)
    {
        return _webDriverWait.Value.Until(_ => _driverContext.Driver.FindElement(elementLocator));
    }

    public IEnumerable<IWebElement> FindElements(By elementLocator)
    {
        return _webDriverWait.Value.Until(_ => _driverContext.Driver.FindElements(elementLocator));
    }

    public WebDriverWait GetWaitDriver()
    {
        return new WebDriverWait(_driverContext.Driver, TimeSpan.FromSeconds(_testSettings.TimeoutInterval ?? 30))
        {
            PollingInterval = TimeSpan.FromSeconds(_testSettings.TimeoutInterval ?? 1)
        };
    }
}