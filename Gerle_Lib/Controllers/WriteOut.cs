using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fullPath = Path.Combine(documentsPath, "Gerle");

                string jsonData = JsonConvert.SerializeObject(gameData, Formatting.None);
                File.WriteAllText(fullPath, jsonData);
        }
    }
}
