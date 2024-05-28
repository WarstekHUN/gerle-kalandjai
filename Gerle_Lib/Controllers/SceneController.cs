using Gerle_Lib.BaseClasses;
using Gerle_Lib.Data;

namespace Gerle_Lib.Controllers
{
    #region SceneController (osztály) - comment
    /// <summary>
    /// <c>SceneController</c> osztály tárolja, kezeli a jeleneteket, harcokat, valamint tárolja hol tart a játékos a forgatókönyvben (<c>SceneController.cs</c>).
    /// </summary>
    #endregion
    public static partial class SceneController
    {
        public enum FightEndingReason
        {
            PlayerDeath,
            EnemyDeath
        }

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
        public static FightEndingReason InitFight(Actor opponentCharacter)
        {
            Scene currentscene = Scenes[CurrentCheckpoint];
            //Körökre osztott harcrendszer
            uint turn = 0;
            FightEndingReason? fightEnd = null;

            FightingActor player = new FightingActor(ref Actors.Gerle);
            FightingActor opponent = player.SetupOpponent(ref opponentCharacter);
            while (fightEnd == null)
            {
                FightingActor currentTurnActor;
                FightingActor currentTurnOpponent;

                if (turn % 2 == 0)
                {
                    currentTurnActor = player;
                    currentTurnOpponent = opponent;
                    if (turn == uint.MaxValue - 1) turn = 0;
                }
                else
                {
                    currentTurnActor = opponent;
                    currentTurnOpponent = player;
                }

                Power? attack = currentTurnActor.Think();

                bool death = false;

                if (attack != null)
                {
                    if (attack is SpecialPower)
                    {
                        if (((SpecialPower)attack).SpecialAbility(ref currentTurnActor, ref currentTurnOpponent)) death = true;
                    }
                    else
                    {
                        if (currentTurnActor.Attack(attack)) death = true;
                    }
                }

                if (death)
                {
                    if (currentTurnOpponent == player)
                    {
                        fightEnd = FightEndingReason.EnemyDeath;
                    }
                    else
                    {
                        fightEnd = FightEndingReason.PlayerDeath;
                    }
                }
            }

            return (FightEndingReason)fightEnd;
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
                    //Ez is egy referencia alapú passzolás, csak a C# nem akarja egyértelművé tenni, mert minek az
                    InitFight(Scenes[i].Opponent);
                }

                CurrentCheckpoint = i;

                ProgressController.SaveToFile();
            }
        }
    }
}