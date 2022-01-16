using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TestSkeleton_Selenium.NET_Screenplay.Helpers;
using UITests.Interactions.Page_Element_Repositories;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Tasks
{
    internal class UrlNavigationTasks : IUrlNavigationTasks
    {
        public Action<IWebDriver, IWaits> ToWikipediaHomePage()
        {

            return (driver, wait) =>
            {
                driver.Navigate().GoToUrl("https://www.wikipedia.org/");
                wait.WaitUntilVisible(HomePage.WikipediaHeader);
            };
        }
    }
}
