using Bogus;
using OpenQA.Selenium;
using TestSkeleton_Selenium.NET_Screenplay.Helpers;
using TestSkeleton_Selenium.NET_Screenplay.Interactions.Actors.ActorTypes;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Actors
{
    internal class ActorFactory : IActorFactory
    {
        private enum Title
        {
            Mr,
            Ms,
            Mrs,
            Dr,
            Prof,
            Rev,
            Sir,
            Dame
        }

        private readonly IWebDriver _driver;
        private readonly IWaits _waits;

        public ActorFactory(IWebDriver driver, IWaits waits)
        {
            _driver = driver;
            _waits = waits;
        }

        public IActor GenerateActor(string actorType, string name = null)
        {
            switch (actorType.ToLowerInvariant())
            {
                default:
                    return Visitor(_driver, _waits, name);
            }
        }

        private static IActor Visitor(IWebDriver driver, IWaits waits, string name)
        {
            Visitor fakeVisitor = new Faker<Visitor>()
                .RuleFor(u => u.Title, f => f.PickRandom<Title>().ToString())
                .RuleFor(u => u.FirstName, (f, u) => name == null ? f.Name.FirstName() : $"{name}")
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName());
            fakeVisitor.Driver = driver;
            fakeVisitor.Waits = waits;

            return fakeVisitor;
        }
    }
}
