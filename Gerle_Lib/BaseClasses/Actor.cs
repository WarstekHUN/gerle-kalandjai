#region Actor (osztály) - comment
#endregion
using Gerle_Lib.BaseClasses;

/// <summary>
/// <c>Actor</c> osztály a szereplők, adataiknak az osztálya (<c>Actor.cs</c>).
/// </summary>
public class Actor
{
    #region Name (tulajdonság) - comment
    /// <summary>
    /// <c>Name</c> méző a szereplő / Actor nevét tartalmazza. Ha nem adunk meg neki értéket akkor marad a default: "Default Actor" értékénél.
    /// </summary>
    #endregion
    public string Name = "Default Actor";
    #region MaxHealth (mező) - Comment
    /// <summary>
    /// <c>MaxHealth</c> mező a játékos maximális életerejét tartalmazza. A játékosnak ennél a számnál nem lehet nagyobb életereje. Adott esetben ez a szám a "100".
    /// </summary>
    #endregion
    public const ushort MaxHealth = 100;
    #region MaxMana (mező) - Comment
    /// <summary>
    /// <c>MaxMana</c> mező a játékos maximális manaszintjét tartalmazza. A játékosnak ennél a számnál nem lehet nagyobb manaszintje. Adott esetben ez a szám az "50".
    /// </summary>
    #endregion
    public const ushort MaxMana = 50;
    #region Power (tulajdonság)  - comment
    /// <summary>
    /// <c>Powers</c> tulajdonság a szereplők képességeit tartalmazza (Power.cs).
    /// </summary>
    #endregion
    public Power[]? Powers { get; init; }
    #region Aggression (tulajdonság)
    /// <summary>
    /// Megadja egy 1-től 10-ig terjedő skálán, hogy mennyire aggresszív az adott Actor
    /// </summary>
    #endregion
    public byte Aggression { get; init; }
    #region AverageManaCostOfPowers (tulajdonság)
    /// <summary>
    /// Megadja, hogy a karakter képességei átlagosan hány manába kerülnek
    /// </summary>
    #endregion
    public float? AverageManaCostOfPowers { get; init; }
    #region LeastManaExpensivePower (tulajdonság)
    /// <summary>
    /// Megadja, hogy melyik a legkevesebb manát igénylő képessége az Actornek,
    /// </summary>
    #endregion
    public Power? LeastManaExpensivePower { get; init; }
    #region Actor (paraméteres konstruktor) - comment
    /// <summary>
    /// <c>Actor</c> paraméteres konstruktorral beállíthatjuk a szereplők nevét, képességeit.
    /// </summary>
    #endregion
    public Actor(string name, Power[]? powers = null, byte aggression = 5)
    {
        Name = name;
        Powers = powers;
        Aggression = Math.Clamp(aggression, (byte)1, (byte)10);
        AverageManaCostOfPowers = (float?)powers?.Average(el => el.Mana);
        LeastManaExpensivePower = powers?.MinBy(el => el.Mana);
    }
}