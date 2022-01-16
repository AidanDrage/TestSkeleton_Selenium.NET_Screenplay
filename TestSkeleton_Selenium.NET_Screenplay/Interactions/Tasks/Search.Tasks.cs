using OpenQA.Selenium;
using System;
using TestSkeleton_Selenium.NET_Screenplay.Helpers;
using UITests.Interactions.Page_Element_Repositories;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Tasks
{
    internal class SearchTasks : ISearchTasks
    {
        public Action<IWebDriver, IWaits> For(string searchTerm)
        {
            return (driver, wait) =>
            {
                wait.WaitUntilEnabled(HomePage.SearchBox);
                wait.WaitUntilEnabled(HomePage.SearchButton);

                driver.FindElement(HomePage.SearchBox).SendKeys(searchTerm);
                driver.FindElement(HomePage.SearchButton).Click();
            };
        }
    }
}