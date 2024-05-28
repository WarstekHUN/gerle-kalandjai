using Gerle_Lib.Actors;
using Gerle_Lib.BaseClasses;
#region SceneController (osztály) - comment
/// <summary>
/// <c>SceneController</c> osztály tárolja, kezeli a jeleneteket, harcokat, valamint tárolja hol tart a játékos a forgatókönyvben (<c>SceneController.cs</c>).
/// </summary>
#endregion
public static class SceneController
{
    #region Scenes (mező) - comment
    /// <summary>
    /// <c>Scenes</c> mező tartalmazza az összes jelenetet. Ez a játékjelenetek tömbje.
    /// </summary>
    #endregion
    //TODO: Ezt itt feltölteni jelenetekkel
    private static Scene[] Scenes = {
        new Scene(new Line[]
        {
            new Line("Az ápoló bejön az ajtón", ref Actors.Narrator, "01_01_narrator"),
            new Line("Szép napot Gerle! Meséljen, hogyan érzi magát. Minden rendben volt az este folyamán? Nem volt hideg a szobában, nem fázott?", ref Actors.Apolo, "01_02_apolo")
        }, ref Actors.Apolo!)    
    };

    #region CurrentCheckpoint (tulajdonság) - comment
    /// <summary>
    /// <c>CurrentCheckpoint</c> tulajdonság tartalmazza, a játékos hanyadik jeleneten, illetve harcon van túl.
    /// </summary>
    #endregion
    public static uint CurrentCheckpoint { get; private set; } = 0;
    #region InitFight (metódus) - comment
    /// <summary>
    /// <c>InitFight</c> metódus elindítja a játékos számára a harcot. 
    /// </summary>
    #endregion
    public static void InitFight(Actor opponent)
    {
        Scene currentscene = Scenes[CurrentCheckpoint];
        
    }
    #region PlayScenes (metódus) - comment
    /// <summary>
    /// <c>PlayScenes</c> a jeleneteket játssza le az ellenőrző pontoktól (<c>CurrentCheckpoint</c>).
    /// </summary>
    #endregion
    public static void PlayScenes(uint checkpoint)
    {
        if (checkpoint >= Scenes.Length)
        {
            BeautyWriter.Write("Hibás mentésfájl.");
            Thread.Sleep(5000);
            Environment.Exit(2);
        }


        for (uint i = checkpoint; i < Scenes.Length; i++)
        {
            Scenes[i].PlayScene();
            if (Scenes[i].Opponent is not null)
            {
                InitFight(Scenes[i].Opponent!);
            }

            CurrentCheckpoint = i;
        }
    }
}
