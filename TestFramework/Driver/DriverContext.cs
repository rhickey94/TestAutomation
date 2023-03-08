using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TestFramework.Config;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace TestFramework.Driver;

public class DriverContext : IDriverContext
{
    public IWebDriver Driver { get; }
    public TestSettings TestSettings { get; }
    
    public DriverContext()
    {
        TestSettings = ConfigReader.ReadConfig();
        Driver = GetWebDriver();
        Driver.Navigate().GoToUrl(TestSettings.ApplicationUrl);
    }
    
    public IWebDriver GetWebDriver()
    {
        var driverConfig = GetDriverConfig();

        new DriverManager().SetUpDriver(driverConfig);
        
        return TestSettings.BrowserType switch
        {
            BrowserType.Chrome => new ChromeDriver(),
            BrowserType.Firefox => new FirefoxDriver(),
            _ => new ChromeDriver()
        };
    }

    private IDriverConfig GetDriverConfig()
    {
        return TestSettings.BrowserType switch
        {
            BrowserType.Chrome => new ChromeConfig(),
            BrowserType.Firefox => new FirefoxConfig(),
            _ => new ChromeConfig()
        };
    }
}