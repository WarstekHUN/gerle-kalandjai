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
    private static Scene[] Scenes = { };
    #region CurrentCheckpoint (tulajdonság) - comment
    /// <summary>
    /// <c>CurrentCheckpoint</c> tulajdonság tartalmazza, a játékos hanyadik jeleneten, illetve harcon van túl.
    /// </summary>
    #endregion
    public static uint CurrentCheckpoint { get; private set; }
    #region InitFight (metódus) - comment
    /// <summary>
    /// <c>InitFight</c> metódus elindítja a játékos számára a harcot. 
    /// </summary>
    #endregion
    public static void InitFight()
    {
        throw new NotImplementedException();
    }
    #region PlayScenes (metódus) - comment
    /// <summary>
    /// <c>PlayScenes</c> a jeleneteket játssza le az ellenőrző pontoktól (<c>CurrentCheckpoint</c>).
    /// </summary>
    #endregion
    public static void PlayScenes(uint checkpoint)
    {
        throw new NotImplementedException();
    }
}
