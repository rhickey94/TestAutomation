using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.Driver;

public interface IFluentDriver
{
    IWebElement FindElement(By elementLocator);
    IEnumerable<IWebElement> FindElements(By elementLocator);
    WebDriverWait GetWaitDriver();
}