using FluentAssertions;
using TechTalk.SpecFlow;
using TestSkeleton_Selenium.NET_Screenplay.Interactions.Tasks;
using static TestSkeleton_Selenium.NET_Screenplay.Interactions.Questions.UrlQuestions;
using static TestSkeleton_Selenium.NET_Screenplay.Interactions.Questions.HomeQuestions;
using static TestSkeleton_Selenium.NET_Screenplay.Interactions.Questions.ArticleQuestions;
using System.Collections.Generic;
using TestSkeleton_Selenium.NET_Screenplay.Interactions.Actors;
using System;

namespace TestSkeleton_Selenium.NET_Screenplay.Tests.Steps
{
    [Binding]
    internal class WikipediaSteps
    {
        private readonly IActorFactory ActorFactory;
        private readonly IUrlNavigationTasks Navigate;
        private readonly ISearchTasks Search;
        
        private IActor Holden;
        private string SearchTerm;

        public WikipediaSteps(IActorFactory actorFactory, IUrlNavigationTasks urlNavigation, ISearchTasks searchTasks)
        {
            ActorFactory = actorFactory;
            Navigate = urlNavigation;
            Search = searchTasks;
        }

        #region Givens
        [Given(@"The User has Navigated to the wikipedia home page")]
        public void GivenTheUserHasNavigatedToHttpsWww_Wikipedia_Org()
        {
            Holden = ActorFactory.GenerateActor("James Holden");
            Holden.TriesTo(Navigate.ToWikipediaHomePage());
        }
        #endregion

        #region Whens
        [When(@"The User Searches for ""(.*)""")]
        public void WhenTheUserSearchesFor(string searchTerm)
        {
            SearchTerm = searchTerm;
            Holden.TriesTo(Search.For(searchTerm));
        }
        #endregion

        #region Thens
        [Then(@"The Wikipedia Homepage will be displayed")]
        public void ThenTheWikipediaHomepageWillBeDisplayed()
        {
            Uri currenturl = Holden.Asks(WhatURLAmIOn());
            currenturl.ToString().Should().Be("https://www.wikipedia.org/", "because the wikipedia homepage should be displayed");

            bool headerDisplayed = Holden.Asks(IsTheHeaderDisplayed());
            headerDisplayed.Should().BeTrue("because the wikipedia homepage should be displayed");
        }
        
        [Then(@"Then The Wikipedia Homepage will list (.*) as one of the top 10 languages")]
        public void ThenThenTheWikipediaHomepageWillListEnglishAsOneOfTheTopLanguages(string language)
        {
            List<string> languages = Holden.Asks(WhatLanguagesAreDisplayed());
            languages.Should().Contain(language, $"because {language} should be displayed on the hompeage");
        }
        
        [Then(@"The Wikipedia article that the user search for will be displayed")]
        public void ThenTheWikipediaArticleThatTheUserSearchForWillBeDisplayed()
        {
            string article = Holden.Asks(WhatArticleAmIOn());
            article.Should().Be(SearchTerm, "because we should be on the article we searched for");
        }
        #endregion
    }
}
