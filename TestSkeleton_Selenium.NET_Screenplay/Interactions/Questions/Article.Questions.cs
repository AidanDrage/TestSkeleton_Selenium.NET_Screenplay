using OpenQA.Selenium;
using System;
using TestSkeleton_Selenium.NET_Screenplay.Interactions.Page_Element_Repositories;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Questions
{
    internal static class ArticleQuestions
    {
        public static Func<IWebDriver, string> WhatArticleAmIOn()
        {
            return driver =>
            {
                return driver.FindElement(ArticlePage.ArticleHeader).Text;
            };
        }
    }
}
