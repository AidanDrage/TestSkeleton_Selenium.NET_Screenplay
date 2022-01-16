using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestSkeleton_Selenium.NET_Screenplay.Drivers
{
    static class ChromeDriverProvider
    {
        public static IWebDriver GetChromeDriver()
        {
            var options = new ChromeOptions();

            IWebDriver driver = new ChromeDriver(options);
            return driver;
        }
    }
}