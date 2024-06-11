using Gerle_Lib.BaseClasses;
using Gerle_Lib.Controllers;
using Gerle_Lib.UIReleated;
using MenuSystem;
using Spectre.Console;
using System;
using System.Linq;
using System.Runtime.InteropServices;

class Program
{
    public static int masterVolume = 100;
    public static int musicVolume = 50;
    public static int castVolume = 70;
    public static int sfxVolume = 100;

    static void Main(string[] args)
    {
        SoundEffectController.LoadEffects();
        UI.InitializeUI();
    }
}
