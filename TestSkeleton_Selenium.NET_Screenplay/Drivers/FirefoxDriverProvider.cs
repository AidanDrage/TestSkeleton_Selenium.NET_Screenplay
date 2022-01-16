using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TestSkeleton_Selenium.NET_Screenplay.Drivers
{
    static class FirefoxDriverProvider
    {
        public static IWebDriver GetFirefoxDriver()
        {
            var options = new FirefoxOptions();
            var profile = new FirefoxProfile();

            RegisterEncodingFirefox.RegisterEncodingPage437();

            options.Profile = profile;

            IWebDriver driver = new FirefoxDriver(options);
            return driver;
        }
    }
}