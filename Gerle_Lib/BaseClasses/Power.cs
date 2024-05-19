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
    #region Mana (tulajdonság) - comment
    /// <summary>
    /// <c>Mana</c> tulajdonság a képesség mana-szükségletét tartalmazza. Pl: "5". Ezt az értéket csak konstruktor állíthatja!
    /// </summary>
    #endregion
    public ushort Mana { get; init; }
    #region DodgeText (tulajdonság) - comment
    /// <summary>
    /// <c>DodgeText</c> tulajdonság a "minigame" / harc elején jelenik meg, figyelmeztető jelleggel. Pl: "Védd ki a XY ütéseit!". Ezt az értéket csak konstruktor állíthatja!
    /// </summary>
    #endregion
    public string? DodgeText { get; init; }
    #region IsDodgeable (mező) - comment
    /// <summary>
    /// <c>IsDodgeable</c> egy számított mező, amely megadja / eldönti, hogy ki lehet-e védeni az ellenség támadását. Ha van <c>DodgeText</c>, akkor ki lehet. Csak olvasható!
    /// </summary>
    #endregion
    public bool IsDodgeable
    {
        get => DodgeText is not null;
    }
    #region Power (paraméteres konstruktor) - comment
    /// <summary>
    /// <c>Power</c> paraméteres konstruktor beállítja a képességek nevét, sebzéseiknek mértékét, manaszükségletüket valamint azt, hogy ki lehet-e térni előle.
    /// </summary>
    #endregion
    public Power(string name, ushort damage, ushort mana, string? dodgeText)
    {
        Name = name;
        Damage = damage;
        Mana = mana;
        DodgeText = dodgeText;
    }
    #region Minigame (virtuális metódus) - comment
    /// <summary>
    /// <c>Minigame</c> egy virtuális metódus amely elindítja a "minigame"-et, amely során eldől, hárítható-e a támadás.
    /// </summary>
    #endregion
    public virtual void Minigame()
    {
        throw new NotImplementedException();
    }
}
