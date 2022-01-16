using OpenQA.Selenium;
using System;
using TestSkeleton_Selenium.NET_Screenplay.Helpers;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Tasks
{
    interface ISearchTasks
    {
        Action<IWebDriver, IWaits> For(string searchTerm);
    }
}
