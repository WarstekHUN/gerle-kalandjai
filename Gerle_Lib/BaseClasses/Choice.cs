namespace Gerle_Lib.BaseClasses
{
    public class Choice
    {
        #region Text (tulajdonság)
        /// <summary>
        /// A választási lehetőség leírását tartalmazza
        /// </summary>
        #endregion
        public string Text { get; init; }

        #region SceneVersion (tulajdonság)
        /// <summary>
        /// Megadja, hogy a választás melyik történetvonalat képviseli
        /// </summary>
        #endregion
        public SceneVersion SceneVersion { get; init; }

        public Choice(string text, SceneVersion sceneVersion)
        {
            Text = text;
            SceneVersion = sceneVersion;
        }
    }
}
