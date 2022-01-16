using System;
using OpenQA.Selenium;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Questions
{
    internal static class UrlQuestions
    {
        public static Func<IWebDriver, Uri> WhatURLAmIOn()
        {
            return driver => new Uri(driver.Url.ToString());
        }
    }
}
