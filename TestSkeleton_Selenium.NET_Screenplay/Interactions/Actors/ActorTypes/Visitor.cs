using System;
using OpenQA.Selenium;
using TestSkeleton_Selenium.NET_Screenplay.Helpers;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Actors.ActorTypes
{
    /// <summary>
    /// An object that represents the current user,
    /// a new user should be mocked per test to
    /// maintain each test being Atomic
    /// </summary>
    internal class Visitor : IActor
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IWebDriver Driver { get; set; }

        public IWaits Waits { get; set; }

        public void TriesTo(Action<IWebDriver> task)
        {
            task.Invoke(Driver);
        }

        public void TriesTo(Action<IWebDriver, IWaits> task)
        {
            task.Invoke(Driver, Waits);
        }

        public dynamic TriesTo(Func<IWebDriver, IWaits, dynamic> task)
        {
            return task.Invoke(Driver, Waits);
        }

        public dynamic Asks(Func<IWebDriver, dynamic> question)
        {
            return question.Invoke(Driver);
        }

        public bool Asks(Func<IWebDriver, bool> question)
        {
            return question.Invoke(Driver);
        }
    }
}
