using OpenQA.Selenium;
using System;
using TestSkeleton_Selenium.NET_Screenplay.Helpers;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Tasks
{
    /// <summary>
    /// Tasks to do with navigating to different areas of the site
    /// </summary>
    internal interface IUrlNavigationTasks
    {
        Action<IWebDriver, IWaits> ToWikipediaHomePage();
    }
}