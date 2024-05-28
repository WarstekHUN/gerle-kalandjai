using Newtonsoft.Json;

namespace Gerle_Lib.Controllers
{
    public class WriteOut
    {
        public static void Main()
        { 
            GameData gameData = new GameData(
                SceneController.CurrentCheckpoint,
                SettingsController.MusicVolume,
                SettingsController.FXVolume,
                SettingsController.DialogueVolume
                );
                string json = JsonConvert.SerializeObject(gameData);
                Console.WriteLine(json);
        }
    }
}
