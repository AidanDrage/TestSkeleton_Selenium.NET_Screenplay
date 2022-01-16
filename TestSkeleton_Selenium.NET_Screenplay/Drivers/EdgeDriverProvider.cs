using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace TestSkeleton_Selenium.NET_Screenplay.Drivers
{
    static class EdgeDriverProvider
    {
        public static IWebDriver GetEdgeDriver()
        {
            var options = new EdgeOptions();

            IWebDriver driver = new EdgeDriver(@"..\..\..\..\drivers", options);
            return driver;
        }
    }
}