using Gerle_Lib.BaseClasses.Powers;

namespace Gerle_Lib.BaseClasses
{
    #region Power (osztály) - comment
    /// <summary>
    /// <c>Power</c> osztály a szereplők képességinek, azok adatainak az osztálya (<c>Power.cs</c>).
    /// </summary>
    #endregion
    public class Power
    {
        #region Name (tulajdonság) - comment
        /// <summary>
        /// <c>Name</c> tulajdonság a képesség nevét tartalmazza. Pl: repülés. Ezt az értéket csak konstruktor állíthatja!
        /// </summary>
        #endregion
        public string Name { get; init; }
        #region Damage (tulajdonság) - comment
        /// <summary>
        /// <c>Damage</c> tulajdonság a képesség sebzésének mértékét, nagyságát tartalmazza. Pl: "10". Ezt az értéket csak konstruktor állíthatja!
        /// </summary>
        #endregion
        public ushort Damage { get; init; }
        #region DamageText (mező) - comment
        /// <summary>
        /// <c>DamageText</c> mező tartalmazza, 
        /// </summary>
        #endregion
        public string DamageText { get; set; }
        #region Mana (tulajdonság) - comment
        /// <summary>
        /// <c>Mana</c> tulajdonság a képesség mana-szükségletét tartalmazza. Pl: "5". Ezt az értéket csak konstruktor állíthatja!
        /// </summary>
        #endregion
        public ushort Mana { get; init; }
        #region IsDodgeable (mező) - comment
        /// <summary>
        /// <c>IsDodgeable</c> egy mező, amely megadja / eldönti, hogy ki lehet-e védeni az ellenség támadását. Csak olvasható!
        /// </summary>
        #endregion
        public bool? IsDodgeable { get; init; } = null;
        #region IsUsed (mező) - comment
        /// <summary>
        /// <c>IsUsed</c> egy mező, amely megadja / eldönti, hogy használták e már a képességet. Ha null, akkor korlátlanul használható a képesség!
        /// </summary>
        #endregion
        public bool IsUsed { get; set; }
        #region Power (paraméteres konstruktor) - comment
        /// <summary>
        /// <c>Power</c> paraméteres konstruktor beállítja a képességek nevét, sebzéseiknek mértékét, manaszükségletüket valamint azt, hogy ki lehet-e térni előle.
        /// </summary>
        #endregion
        public Power(string name, ushort damage, ushort mana, bool isDodgeable, string damageText, bool isUsed)
        {
            Name = name;
            Damage = damage;
            Mana = mana;
            IsDodgeable = isDodgeable;
            DamageText = damageText;
            IsUsed = isUsed;
        }
        #region Minigame - comment
        /// <summary>
        /// <c>Minigame</c> elindítja a "minigame"-et, amely során eldől, hárítható-e a támadás.
        /// </summary>
        #endregion
        public PowerEvent? Minigame;
    }

}