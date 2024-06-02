using Gerle_Lib.Data;
using Gerle_Lib.BaseClasses;

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
                new Line("Szép napot Gerle! Meséljen, hogyan érzi magát. Minden rendben volt az este folyamán? Nem volt hideg a szobában, nem fázott?", ref Actors.Apolo, "01/02_Apolo")
            }, ref Actors.Apolo, new SceneMusic("01_start", "01_loop", "01_end", 160)) //Ez egy olyan jelenet, aminek a végén van harc. A SceneMusic a harc zenéjét veszi be. Csak a fájlnevek kellenek, amik bele vannak rakva a Data/Audio/Music mappába. Fájlkiterjesztés sem kell.
        };
    }
}