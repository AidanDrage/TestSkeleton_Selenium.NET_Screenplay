using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace TestSkeleton_Selenium.NET_Screenplay.Drivers
{
    static class RemoteDriverProvider
    {
        public static DriverOptions GetRemoteBrowserOptions(string desiredBrowser, string screenFormat = "desktop")
        {
            switch (desiredBrowser.ToLower())
            {
                case "firefox":

                    var ffOptions = new FirefoxOptions();
                    var profile = new FirefoxProfile();

                    RegisterEncodingFirefox.RegisterEncodingPage437();

                    switch (screenFormat.ToLowerInvariant())
                    {
                        case "tablet":
                            profile.SetPreference("general.useragent.override",
                                "Mozilla/5.0 (iPad; CPU OS 11_0 like Mac OS X) AppleWebKit/604.1.34 (KHTML, like Gecko) Version/11.0 Mobile/15A5341f Safari/604.1");
                            break;
                        case "mobile":
                            profile.SetPreference("general.useragent.override",
                                "Mozilla/5.0 (Linux; Android 8.0; Pixel 2 Build/OPD3.170816.012) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Mobile Safari/537.36");
                            break;
                    }

                    ffOptions.Profile = profile;
                    return ffOptions;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    switch (screenFormat.ToLowerInvariant())
                    {
                        case "tablet":
                            edgeOptions.AddArgument("--user-agent=Mozilla/5.0 (iPad; CPU OS 11_0 like Mac OS X) AppleWebKit/604.1.34 (KHTML, like Gecko) Version/11.0 Mobile/15A5341f Safari/604.1");
                            break;
                        case "mobile":
                            edgeOptions.AddArgument("--user-agent=Mozilla/5.0 (Linux; Android 8.0; Pixel 2 Build/OPD3.170816.012) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Mobile Safari/537.36");
                            break;
                    }

                    return edgeOptions;

                default:
                    var chromeOptions = new ChromeOptions();

                    switch (screenFormat.ToLowerInvariant())
                    {
                        case "tablet":
                            chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (iPad; CPU OS 11_0 like Mac OS X) AppleWebKit/604.1.34 (KHTML, like Gecko) Version/11.0 Mobile/15A5341f Safari/604.1");
                            break;
                        case "mobile":
                            chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (Linux; Android 8.0; Pixel 2 Build/OPD3.170816.012) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Mobile Safari/537.36");
                            break;
                    }

                    return chromeOptions;
            }
        }

        public static IWebDriver GetRemoteDriver(DriverOptions driverOptions)
        {
            return new RemoteWebDriver(driverOptions);
        }
    }
}