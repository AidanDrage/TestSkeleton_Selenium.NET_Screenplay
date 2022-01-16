using OpenQA.Selenium;

namespace UITests.Interactions.Page_Element_Repositories
{
    internal static class HomePage
    {
        internal static By WikipediaHeader => By.CssSelector("h1.central-textlogo-wrapper");
        internal static By LanguageLinks => By.CssSelector("div.central-featured-lang");
        internal static By SearchBox => By.CssSelector("input#searchInput");
        internal static By SearchButton => By.CssSelector("button.pure-button");
        
    }
}
