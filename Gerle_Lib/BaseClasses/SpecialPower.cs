namespace Gerle_Lib.BaseClasses
{
    ///// <summary>
    ///// adssad
    ///// </summary>
    ///// <param name="currentActor">asd</param>
    ///// <param name="opponent">252352535</param>
    ///// <returns></returns>
    //public delegate Func<FightingActor, FightingActor, bool>PowerEvent(ref FightingActor currentActor, ref FightingActor opponent);

    public delegate bool PowerEvent(ref FightingActor actor1, ref FightingActor actor2);

    #region Power (osztály) - comment
    /// <summary>
    /// <c>Power</c> osztály a szereplők képességinek, azok adatainak az osztálya (<c>Power.cs</c>).
    /// </summary>
    #endregion
    public class SpecialPower : Power
    {
        public SpecialPower(string name, ushort mana, bool isDodgeable, string damageText,
            PowerEvent specialAbility) : base(name, 0, mana, isDodgeable, damageText)
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