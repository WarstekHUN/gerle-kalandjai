using Gerle_Lib.BaseClasses;
using Gerle_Lib.BaseClasses.Powers;
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
        public static uint Turn { get; private set; }

        public enum FightEndingReason
        {
            PlayerDeath,
            EnemyDeath
        }

        public const byte MANA_PER_ROUND = 80;

        #region CurrentCheckpoint (tulajdonság) - comment
        /// <summary>
        /// <c>CurrentCheckpoint</c> tulajdonság tartalmazza, a játékos hanyadik jeleneten, illetve harcon van túl.
        /// </summary>
        #endregion
        public static uint CurrentCheckpoint { get; set; } = 0;
        #region InitFight (metódus) - comment
        /// <summary>
        /// <c>InitFight</c> metódus elindítja a játékos számára a harcot. 
        /// </summary>
        #endregion
        public static FightEndingReason InitFight(Scene scene)
        {
            Actor opponentCharacter = scene.Opponent!;

            //Körökre osztott harcrendszer
            Turn = 0;
            FightEndingReason? fightEnd = null;

            Player player = new Player(ref Actors.Gerle);

            FightingActor opponent = player.SetupOpponent(ref opponentCharacter);

            MusicController.PlayMusic(scene.FightMusic);

            while (fightEnd == null)
            {
                if (Turn != 0)
                {
                    player.Mana += MANA_PER_ROUND;
                    opponent.Mana += MANA_PER_ROUND;
                }

                FightingActor currentTurnActor;
                FightingActor currentTurnOpponent;

                if (Turn % 2 == 0)
                {
                    currentTurnActor = player;
                    currentTurnOpponent = opponent;
                    if (Turn == uint.MaxValue) Turn = 1;
                }
                else
                {
                    currentTurnActor = opponent;
                    currentTurnOpponent = player;
                }

                List<Power> attacks = currentTurnActor.Think();

                bool death = false;

                /*Task.Run(() =>
                {*/
                foreach (Power attack in attacks)
                {
                    if (attack != null)
                    {
                        if (attack is SpecialPower)
                        {
                            ((SpecialPower)attack).SpecialAbility(ref currentTurnActor, ref currentTurnOpponent);

                            if (currentTurnOpponent.Health <= 0)
                            {
                                death = true;
                            }
                        }
                        else
                        {
                            death = currentTurnActor.Attack(attack);
                        }

                        //TODO: attack.damageText kiírása

                        if (currentTurnActor is Player)
                            SoundEffectController.PlayEffect(SoundEffectController.SoundEffects.DealDamage);
                        else
                            SoundEffectController.PlayEffect(SoundEffectController.SoundEffects.ReceiveDamage);

                        //3mp várakozás, hogy a játékos el tudja olvasni a damageText-et.
                        Thread.Sleep(3000);

                        if (death)
                        {
                            break;
                        }
                    }
                }
                //});

                if (death)
                {
                    if (currentTurnOpponent == player)
                    {
                        fightEnd = FightEndingReason.EnemyDeath;
                        MusicController.EndMusic();
                    }
                    else
                    {
                        fightEnd = FightEndingReason.PlayerDeath;
                        SoundEffectController.PlayEffect(SoundEffectController.SoundEffects.LoseGame);
                        MusicController.StopMusic();
                    }

                    void ResetPowers(Actor actor)
                    {
                        foreach(Power power in opponentCharacter.Powers!)
                        {
                            if(power.IsUsed is not null) power.IsUsed = false;
                        }
                    }

                    //A képességek elhasználtságának visszaállítása
                    //Ref alapúnak kéne lennie
                    Task.WaitAll(Task.Run(() => ResetPowers(opponentCharacter)), Task.Run(() => ResetPowers(player.Actor)));
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
                FightEndingReason? fightEnd = null;
                if (Scenes[i].Opponent is not null)
                {
                    //Ez is egy referencia alapú passzolás, csak a C# nem akarja egyértelművé tenni, mert minek az
                    fightEnd = InitFight(Scenes[i]);
                }

                if (fightEnd is not null)
                {
                    if (fightEnd == FightEndingReason.EnemyDeath)
                    {
                        CurrentCheckpoint = i;
                        ProgressController.SaveToFile();
                        //TODO: Ellenfél legyőzve UI
                    }
                    else
                    {
                        //TODO: Meghalás UI

                    }

                }
                else
                {
                    ProgressController.SaveToFile();
                }
            }
        }
    }
}