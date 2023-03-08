using OpenQA.Selenium;
using TestFramework.Config;

namespace TestFramework.Driver;

public interface IDriverContext
{
    public IWebDriver GetWebDriver();
    public IWebDriver Driver { get; }
    public TestSettings TestSettings { get; }
}