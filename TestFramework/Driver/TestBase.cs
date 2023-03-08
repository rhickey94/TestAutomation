using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using ExtentReportsType = AventStack.ExtentReports.ExtentReports;

namespace TestFramework.Driver;

public class TestBase
{
    private IDriverContext? _driverContext;
    private readonly ThreadLocal<FluentDriver?> _threadSafeDriver = new ThreadLocal<FluentDriver?>();
    protected FluentDriver? Driver => _threadSafeDriver.Value;
    
    private ExtentReportsType _extentReports;
    private ExtentTest _extentTest;

    [OneTimeSetUp]
    public void SuperSetup()
    {
        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent;
        var reportPath = $"{projectDirectory}\\Reports\\index.html";
        var htmlReporter = new ExtentHtmlReporter(reportPath);
        _extentReports = new ExtentReportsType();
        _extentReports.AttachReporter(htmlReporter);
        _extentReports.AddSystemInfo("Host Name", "Sup Nerds");
    }

    [SetUp]
    public void Setup()
    {
        _extentTest = _extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        _driverContext = new DriverContext();
        _threadSafeDriver.Value = new FluentDriver(_driverContext);
    }

    [TearDown]
    public void TearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        DateTime time = DateTime.Now;
        string fileName = $"Screenshot_{time:h_mm_ss}.png";

        if (status == TestStatus.Failed)
        {
            _extentTest.Fail("bruh", CaptureScreenshot(Driver.Driver, fileName));
        }
        _extentReports.Flush();
        Driver!.Driver.Quit();
    }

    private MediaEntityModelProvider CaptureScreenshot(IWebDriver driver, string screenshotName)
    {
        var ts = (ITakesScreenshot)driver;
        var screenshot = ts.GetScreenshot().AsBase64EncodedString;

        return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
    }
}