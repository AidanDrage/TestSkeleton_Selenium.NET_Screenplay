using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestSkeleton_Selenium.NET_Screenplay.Helpers
{
    /// <summary>
    /// A collection of wait helpers that will help with waiting and reporting
    /// </summary>
    internal interface IWaits
    {
        /// <summary>
        /// Waits until the condition in <param name="condition"></param> evaluates to true
        /// </summary>
        /// <param name="condition">The function to evaluate</param>
        /// <param name="reason">A custom error message, it is suggested to supply this as without it
        /// error reporting will be less useful</param>
        /// <param name="seconds">The max number of seconds to wait before throwing an exception, default 5</param>
        void WaitUntil(Func<IWebDriver, bool> condition, string reason = "", int seconds = 5);

        void WaiUntilNot(Func<IWebDriver, bool> condition, string reason = "", int seconds = 5);

        /// <summary>
        /// Waits until <param name="element"></param> is marked as displayed 
        /// </summary>
        /// <param name="element">The By locator of the element to find</param>
        /// <param name="seconds">The max number of seconds to wait before throwing an exception, default 5</param>
        void WaitUntilVisible(By element, int seconds = 5);

        /// <summary>
        /// Waits until either of the two elements supplied is visible
        /// </summary>
        /// <param name="element1">The By locator of the first element to search for</param>
        /// <param name="element2">The By locator of the second element to search for</param>
        /// <param name="seconds">The max number of seconds to wait before throwing an exception, default 5</param>
        void WaitUntilEitherElementIsVisible(By element1, By element2, int seconds = 5);

        /// <summary>
        /// Waits until <param name="element"></param> is marked as enabled 
        /// </summary>
        /// <remarks>Enabled is usually not as reliable in a UI test as visible due to the way browsers and
        /// selenium work, recommend using WaitUntilVisible() if looking for something that the user will see</remarks>
        /// <param name="element">The By locator of the element to find</param>
        /// <param name="seconds">The max number of seconds to wait before throwing an exception, default 5</param>
        void WaitUntilEnabled(By element, int seconds = 5);


    }

    internal class Waits : IWaits
    {
        private readonly IWebDriver _driver;

        public Waits(IWebDriver driver)
        {
            _driver = driver;
        }

        public void WaitUntil(Func<IWebDriver, bool> condition, string reason = "", int seconds = 5)
        {
            var time = new TimeSpan(0, 0, 0, seconds);
            var wait = new WebDriverWait(_driver, time);
            try
            {
                wait.Until(condition);
            }
            catch
            {
                throw new TimeoutException($"Timeout {reason}");
            }
        }

        public void WaiUntilNot(Func<IWebDriver, bool> condition, string reason = "", int seconds = 5)
        {
            var startTime = Environment.TickCount;

            var time = new TimeSpan(0, 0, 0, seconds);
            var wait = new WebDriverWait(_driver, time);

            while (Environment.TickCount - startTime < seconds * 1000)
            {
                try
                {
                    wait.Until(condition);
                }
                catch (Exception)
                {
                    return;
                }
            }

            throw new ElementNotVisibleException($"Timeout { reason }");
        }

        public void WaitUntilVisible(By element, int seconds = 5)
        {
            var time = new TimeSpan(0, 0, 0, seconds);
            var wait = new WebDriverWait(_driver, time);
            try
            {
                wait.Until(x => x.FindElement(element).Displayed);
            }
            catch
            {
                throw new ElementNotVisibleException($"Timeout: {element} was not visible after {seconds} seconds");
            }
        }

        public void WaitUntilEitherElementIsVisible(By element1, By element2, int seconds = 5)
        {
            var time = new TimeSpan(0, 0, 0, seconds);
            var wait = new WebDriverWait(_driver, time);
            try
            {
                wait.Until(x => x.FindElement(element1).Displayed || x.FindElement(element2).Displayed);
            }
            catch
            {
                throw new ElementNotVisibleException($"Timeout: Neither {element1} or {element2} was not visible after {seconds} seconds");
            }
        }

        public void WaitUntilEnabled(By element, int seconds = 5)
        {
            var time = new TimeSpan(0, 0, 0, seconds);
            var wait = new WebDriverWait(_driver, time);
            try
            {
                wait.Until(x => x.FindElement(element).Enabled);
            }
            catch
            {
                throw new ElementNotInteractableException($"Timeout: {element} was not enabled after {seconds} seconds");
            }
        }
    }
}