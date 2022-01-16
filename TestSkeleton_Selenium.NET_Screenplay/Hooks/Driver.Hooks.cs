using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Drawing;
using TechTalk.SpecFlow;

namespace TestSkeleton_Selenium.NET_Screenplay.Hooks
{
    /// <summary>
    /// This set of hooks contains functions related to driver creation
    /// and clear down
    /// </summary>
    [Binding]
    internal sealed class DriverHooks
    {
        /// <summary>
        /// Before each scenario we need to call the driver factory to create a driver
        /// then we need to register it to the Scenario Context DI container so
        /// it is accessible for the lifetime of the test
        /// </summary>
        /// <param name="scenarioContext">The context of this scenario, the driver will be
        /// registered to the container in this context</param>
        /// <param name="config">The configuration files of the project so we can check
        /// if we are meant to be running locally or remotely</param>
        /// <param name="env">Environment configuration (set elsewhere in the hooks) for
        /// use by the driver factory</param>
        [BeforeScenario(Order = 2)]
        public void BeforeScenario(ScenarioContext scenarioContext, IConfiguration config)
        {
            IWebDriver driver;

            driver = Drivers.DriverFactory.GetDriver(true, "Chrome");
            driver.Manage().Window.Maximize();

            scenarioContext.ScenarioContainer.RegisterInstanceAs(driver);
        }

        /// <summary>
        /// After the scenario quits and disposes of the driver so the next scenario can
        /// make one completely independently of this scenario
        /// </summary>
        /// <param name="driver"></param>
        [AfterScenario(Order = 2)]
        public void AfterScenario(IWebDriver driver)
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
