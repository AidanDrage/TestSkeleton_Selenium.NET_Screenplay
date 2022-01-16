namespace TestSkeleton_Selenium.NET_Screenplay.Interactions.Actors
{
    internal interface IActorFactory
    {
        /// <summary>
        /// Generates an actor of your choosing
        /// </summary>
        /// <param name="actorType">The type of actor to generate</param>
        /// <param name="name">An optional first name</param>
        /// <returns></returns>
        IActor GenerateActor(string actorType, string name = null);
    }
}