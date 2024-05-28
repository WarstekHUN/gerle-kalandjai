﻿namespace Gerle_Lib.BaseClasses
{
    public delegate bool PowerEvent(ref FightingActor currentActor, ref FightingActor opponent);

    #region Power (osztály) - comment
    /// <summary>
    /// <c>Power</c> osztály a szereplők képességinek, azok adatainak az osztálya (<c>Power.cs</c>).
    /// </summary>
    #endregion
    public class SpecialPower : Power
    {
        public SpecialPower(string name, ushort mana, bool isDodgeable, string damageText, PowerEvent specialAbility) : base(name, 0, mana, isDodgeable, damageText)
        {
            SpecialAbility = specialAbility;
        }

        #region Minigame - comment
        /// <summary>
        /// <c>Minigame</c> elindítja a "minigame"-et, amely során eldől, hárítható-e a támadás.
        /// </summary>
        #endregion
        public new PowerEvent? Minigame;

        #region SpecialAbility
        /// <summary>
        /// Akkor fut le, hogyha az adott képesség különleges, tehát nem csak simán sebez
        /// </summary>
        #endregion
        public PowerEvent SpecialAbility;
    }

}