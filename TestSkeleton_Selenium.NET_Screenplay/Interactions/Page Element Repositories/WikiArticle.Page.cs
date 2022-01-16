using OpenQA.Selenium;

namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Page_Element_Repositories
{
    internal static class ArticlePage
    {
        internal static By ArticleHeader => By.CssSelector("h1#firstHeading");
    }
}
