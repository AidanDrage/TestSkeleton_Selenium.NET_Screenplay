using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using TechTalk.SpecFlow;

namespace TestSkeleton_Selenium.NET_Screenplay.Hooks
{
    [Binding]
    public sealed class TestCaseIdLog
    {
        [AfterScenario(Order = 4)]
        public void AfterScenario(ScenarioContext context, IConfiguration config, ILogger logger)
        {
            var reportOutputBool = Convert.ToBoolean(config.GetSection("ReportingOptions:TestCaseIdLog:Enabled").Value);
            if (!reportOutputBool) return;

            var testName = context.ScenarioInfo.Title;
            var ids = context.ScenarioInfo.Tags.ToList();

            foreach (var id in ids.Where(id => id.All(char.IsDigit)))
            {
                logger.Information(", {scenarioTitle}, {id}", testName, id);
            }
        }
    }
}
