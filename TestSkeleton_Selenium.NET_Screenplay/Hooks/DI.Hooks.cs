using Microsoft.Extensions.Configuration;
using Serilog;
using TechTalk.SpecFlow;
using TestSkeleton_Selenium.NET_Screenplay.Helpers;
using TestSkeleton_Selenium.NET_Screenplay.Interactions.Actors;
using TestSkeleton_Selenium.NET_Screenplay.Interactions.Tasks;

namespace TestSkeleton_Selenium.NET_Screenplay.Hooks
{
    /// <summary>
    /// This set of hooks contains functions to do with setting up the framework
    /// and configuring the IoC container
    /// </summary>
    [Binding]
    public sealed class DiHooks
    {
        private static IConfigurationRoot _configuration;
        private static ILogger _logger;

        /// <summary>
        /// Builds the objects that wont change throughout the entire test run
        /// The Configuration Sources
        /// The Logger
        /// The Test Environment Information
        /// They will still be registered to the container per scenario
        /// </summary>
        /// <para>
        /// We aren't using .NET Core's inbuilt CreateDefaultBuilder() method
        /// (Which would do most of this for us) because Specflow expects us
        /// to use its inbuilt BoDi IoC container instead of
        /// Microsoft.Extensions.DependencyInjection.
        /// </para>
        [BeforeTestRun(Order = 1)]
        public static void BeforeTestRun()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Local.json", true)
                .AddEnvironmentVariables()
                .Build();

            var testCaseIdReportPath = _configuration.GetSection("ReportingOptions:TestCaseIdLog:Path").Value;
            _logger = new LoggerConfiguration()
                //This is the log file that the TestCaseIDLog will write to, for other logging purposes other files will need to be added
                .WriteTo.Map(
                    "TestCaseIDLog",
                    "TestCaseIDLog",
                    (s, t) => t.File(testCaseIdReportPath)
                )
                .CreateLogger();
        }

        /// <summary>
        /// Registers the Configuration, Logger and environment information.
        /// Then registers the correct set of tasks and questions for the environment. 
        /// </summary>
        /// <param name="scenarioContext"></param>
        [BeforeScenario(Order = 1)]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            //Register Config File
            scenarioContext.ScenarioContainer.RegisterInstanceAs<IConfiguration>(_configuration);

            //Register Logger
            scenarioContext.ScenarioContainer.RegisterInstanceAs(_logger);

            //Register Tasks and Questions
            RegisterTasks(scenarioContext);

            //Register Waits
            scenarioContext.ScenarioContainer.RegisterTypeAs<Waits, IWaits>();

            //Register Actor Factory
            scenarioContext.ScenarioContainer.RegisterTypeAs<ActorFactory, IActorFactory>();
        }

        private static void RegisterTasks(IScenarioContext scenarioContext)
        {
            scenarioContext.ScenarioContainer.RegisterTypeAs<SearchTasks, ISearchTasks>();
            scenarioContext.ScenarioContainer.RegisterTypeAs<UrlNavigationTasks, IUrlNavigationTasks>();
        }
    }
}

