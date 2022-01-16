using OpenQA.Selenium;

namespace TestSkeleton_Selenium.NET_Screenplay.Drivers
{
    public static class DriverFactory
    {
        /// <summary>
        /// The driver Factory
        /// </summary>
        /// <param name="local"></param>
        /// <param name="desiredBrowser"></param>
        /// <param name="screenFormat"></param>
        /// <returns>A Webdriver instance</returns>
        public static IWebDriver GetDriver(bool local, string desiredBrowser)
        {
            IWebDriver driver;

            switch (local)
            {
                case true:
                    switch (desiredBrowser.ToLowerInvariant())
                    {
                        case "firefox":
                            driver = FirefoxDriverProvider.GetFirefoxDriver();
                            return driver;
                        case "edge":
                            driver = EdgeDriverProvider.GetEdgeDriver();
                            return driver;
                        default:
                            driver = ChromeDriverProvider.GetChromeDriver();
                            return driver;
                    }

                case false:
                    var driverOptions = RemoteDriverProvider.GetRemoteBrowserOptions(desiredBrowser);
                    driver = RemoteDriverProvider.GetRemoteDriver(driverOptions);
                    break;
            }

            return driver;
        }
    }
}
