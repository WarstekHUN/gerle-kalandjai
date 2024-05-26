using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerle_Lib.Controllers
{
    public class GameData
    {
        public uint CurrentCheckpoint { get; set; }
        public float MusicVolume { get; set; }
        public float FXVolume { get; set; }
        public float DialogueVolume { get; set; }

        public GameData(uint currentCheckpoint, float musicVolume, float fXVolume, float dialogueVolume)
        {
            CurrentCheckpoint = currentCheckpoint;
            MusicVolume = musicVolume;
            FXVolume = fXVolume;
            DialogueVolume = dialogueVolume;
        }
    }
}
