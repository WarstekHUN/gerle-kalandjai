using Gerle_Lib.Data;

namespace Gerle_Lib.Controllers
{
    public static partial class SceneController
    {
        #region Scenes (mező) - comment
        /// <summary>
        /// <c>Scenes</c> mező tartalmazza az összes jelenetet. Ez a játék jeleneteinek tömbje.
        /// </summary>
        #endregion
        //TODO: Ezt itt feltölteni jelenetekkel
        private static Scene[] Scenes = {
            new Scene(new Line[]
            {
                new Line("Az ápoló bejön az ajtón", ref Actors.Narrator, "01_01_narrator"),
                new Line("Szép napot Gerle! Meséljen, hogyan érzi magát. Minden rendben volt az este folyamán? Nem volt hideg a szobában, nem fázott?", ref Actors.Apolo, "01_02_apolo")
            }, ref Actors.Apolo)
        };
    }
}