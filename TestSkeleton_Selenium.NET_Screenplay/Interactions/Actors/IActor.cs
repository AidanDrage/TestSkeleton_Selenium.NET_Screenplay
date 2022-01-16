using System;
using OpenQA.Selenium;
using TestSkeleton_Selenium.NET_Screenplay.Helpers;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Actors
{
    internal interface IActor
    {
        public void TriesTo(Action<IWebDriver> task);

        public void TriesTo(Action<IWebDriver, IWaits> task);

        public dynamic TriesTo(Func<IWebDriver, IWaits, dynamic> task);

        public dynamic Asks(Func<IWebDriver, dynamic> question);
        bool Asks(Func<IWebDriver, bool> func);
    }
}
