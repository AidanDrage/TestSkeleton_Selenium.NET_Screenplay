using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using TechTalk.SpecFlow;

namespace TestSkeleton_Selenium.NET_Screenplay.Hooks
{
    /// <summary>
    /// This set of hooks contains all of the Extent reports creation code,
    /// all data is pulled from the scenario/feature context & nothing is
    /// logged from elsewhere in the code
    /// </summary>
    [Binding]
    internal sealed class ExtentReportHooks
    {
        private static IConfiguration _config;
        private static string _reportFolder;

        private static ExtentReports _report;
        [ThreadStatic]
        private static ExtentTest _feature;
        [ThreadStatic]
        private static ExtentTest _scenario;
        [ThreadStatic]
        private static ExtentTest _step;

        private DateTime _stepStartTime;

        /// <summary>
        /// Sets up the report before the test run (So there will only be 1 report)
        /// </summary>
        [BeforeTestRun(Order = 3)]
        private static void BeforeTestRun()
        {
            //Have to build a config section here even though its done elsewhere since this hook has to be static and theres no way of injecting it
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Local.json", true)
                .AddEnvironmentVariables()
                .Build();

            _reportFolder = _config.GetSection("ReportingOptions:ExtentReports:OutputFolder").Value;

            Directory.CreateDirectory(_reportFolder);

            _report = new ExtentReports();
            _report.AttachReporter(new ExtentHtmlReporter(_reportFolder));
        }

        /// <summary>
        /// Creates an entry in the report for each feature
        /// </summary>
        [BeforeFeature(Order = 3)]
        private static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _report.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        /// <summary>
        /// Creates the scenario within the report
        /// </summary>
        /// <param name="scenarioContext"></param>
        /// <param name="env"></param>
        [BeforeScenario(Order = 3)]
        private void BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        /// <summary>
        /// Sets the time of step start (so the step knows how long it has taken)
        /// </summary>
        [BeforeStep(Order = 3)]
        private void BeforeStep()
        {
            _stepStartTime = DateTime.Now;
        }

        /// <summary>
        /// Creates the step in the report and logs the right information to it
        /// depending on if it has passed or failed
        /// </summary>
        /// <param name="scenarioContext"></param>
        /// <param name="driver"></param>
        [AfterStep(Order = 3)]
        private void AfterStep(ScenarioContext scenarioContext, IWebDriver driver)
        {
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            _step = _scenario.CreateNode(new GherkinKeyword($"{stepType}"), $"{scenarioContext.StepContext.StepInfo.Text}");

            //Failure behaviour
            if (scenarioContext.TestError != null)
            {
                _step.Fail($"<b>Failure reason:</b> {scenarioContext.TestError.Message}")
                    .Log(Status.Info, $"<b>Failed in:</b> {_stepStartTime - DateTime.Now:g}")
                    .Log(Status.Info, $"<b>Stack Trace:</b> {scenarioContext.TestError.StackTrace.Replace("at ", "<br />at ")}")
                    .Log(Status.Info, "details",
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(GetScreenshotBase64(driver)).Build());
                return;
            }

            //Pass behaviour
            _step.Pass($"Passed")
                .Log(Status.Info, $"{_stepStartTime - DateTime.Now:g}");
            if (stepType == "Then") _step.AddScreenCaptureFromBase64String(GetScreenshotBase64(driver));
        }

        private string GetScreenshotBase64(IWebDriver driver)
        {
            return ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
        }

        /// <summary>
        /// Adds the system info to the dashboard of the report then writes the
        /// report to a file
        /// </summary>
        [AfterTestRun(Order = 3)]
        private static void AfterTestRun()
        {
            if (!Convert.ToBoolean(_config.GetSection("ReportingOptions:ExtentReports:Enabled").Value)) return;

            _report.Flush();

            var newReportName = $"{_reportFolder}TestReport{DateTime.Now:yyMMddhhmm}.html";

            //Renames the report (since Extent Reports got rid of the ability to set a report name)
            File.Move($"{_reportFolder}index.html", $"{_reportFolder}TestReport{DateTime.Now:yyMMddhhmm}.html", true);
        }
    }
}
