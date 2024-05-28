using Gerle_Lib.Controllers;
using Newtonsoft.Json;

#region ProgressController (osztály) - comment
/// <summary>
/// <c>ProgressController</c> osztály a játékos játékbeli eredményeit, teljesítményét, valamint előrehaladását kezeli, tárolja (<c>ProgressController.cs</c>). 
/// </summary>
#endregion
public static class ProgressController
{
    #region LoadFromSaveFile (metódus) - comment
    /// <summary>
    /// <c>LoadFromSaveFile</c> metódus megpróbálja megkeresni és kiolvasni a mentésfájl tartalmát, utána pedig betölteni. True, ha sikerült. False, ha nem.
    /// </summary>
    #endregion
    public static bool LoadFromSaveFile()
    {
        throw new NotImplementedException();
    }

    #region SaveToFile (metódus) - comment
    /// <summary>
    /// <c>SaveToFile</c> metódus lementi a felhasználó "Dokumentumok" mappájába az előrehaladását (<c>Progress</c>).
    /// </summary>
    #endregion
    public static void SaveToFile()
    {
        GameData gameData = new GameData(
            SceneController.CurrentCheckpoint,
            SettingsController.MusicVolume,
            SettingsController.FXVolume,
            SettingsController.DialogueVolume
        );

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string fullPath = Path.Combine(documentsPath, "Gerle");

        string jsonData = JsonConvert.SerializeObject(gameData, Formatting.None);
        File.WriteAllText(fullPath, jsonData);
    }
}
