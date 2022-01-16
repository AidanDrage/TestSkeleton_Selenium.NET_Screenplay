using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using UITests.Interactions.Page_Element_Repositories;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Questions
{
    internal static class HomeQuestions
    {
        public static Func<IWebDriver, bool> IsTheHeaderDisplayed()
        {
            return driver =>
            {
                if (driver.FindElement(HomePage.WikipediaHeader).Displayed)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            };
        }

        public static Func<IWebDriver, List<string>> WhatLanguagesAreDisplayed()
        {
            return driver =>
            {
                var languagelist = new List<string>();
                var langs = driver.FindElements(HomePage.LanguageLinks);

                foreach (var lang in langs)
                {
                    languagelist.Add(lang.Text.Substring(0, lang.Text.IndexOf("\r\n")));
                }

                return languagelist;
            };
        }
    }
}
