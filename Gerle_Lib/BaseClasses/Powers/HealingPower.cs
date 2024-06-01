namespace Gerle_Lib.BaseClasses.Powers
{
    #region Power (osztály) - comment
    /// <summary>
    /// <c>Power</c> osztály a szereplők képességinek, azok adatainak az osztálya (<c>Power.cs</c>).
    /// </summary>
    #endregion
    public class HealingPower : SpecialPower
    {
        public HealingPower(string name, ushort mana, ushort health, string abilityText) : base(name, mana, false, abilityText,
            (SpecialPower thisPower, ref FightingActor current, ref FightingActor opponent) =>
        {
            current.Health += health;
        })
        { }
    }

}