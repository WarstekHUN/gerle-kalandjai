using Gerle_Lib.BaseClasses;
#region FightingActor (osztály) - comment
#endregion
/// <summary>
/// <c>FightingActor</c> osztály az éppen lezajló harc résztvevőinek (<c>Actor</c>) adatait, a harc menetét kezeli (<c>FightingActor.cs</c>). Az <c>Actor</c> osztály tulajdonságait konstruktorait örökli meg.
/// </summary>
public class FightingActor
{
    private Actor Actor { get; init; }

    private ushort _Health {  get; set; }

    #region Health (mező) - comment
    /// <summary>
    /// <c>Health</c> mező az éppen harcoló karakter életerejét tartalmazza.
    /// </summary>
    #endregion
    public ushort Health 
    {
        get => _Health;
        //Imádom, hogy csak ezekkel a castokkal fogadja el, mert különben azt hiszi, hogy ez egy byte...
        set => _Health = Math.Clamp(value, (ushort)0, (ushort)100);
    }

    private ushort _Mana { get; set; }

    #region Mana (mező) - comment
    /// <summary>
    /// <c>Mana</c> mező az éppen harcoló karakter manaszintjét tartalmazza.
    /// </summary>
    #endregion
    public ushort Mana
    {
        get => _Mana;
        set => Math.Max(value, (ushort)0);
    }
    
    #region Opponent (mező) - comment
    /// <summary>
    /// <c>Opponent</c> mező az éppen harcoló karakter ellenségét tartalmazza.
    /// </summary>
    #endregion
    public FightingActor Opponent { get; private set; }

    #region FightingActor (paraméteres konstruktor) - comment
    /// <summary>
    /// <c>FightingActor</c> paraméteres konstruktor az éppen harcoló karakter életerejét, manaszintjét és ellenfelét adja meg. Minden harcot maximális életerővel és manszinttel kezdik meg a karakterek.
    /// </summary>
    #endregion
#pragma warning disable CS8618 // Lehet null az opponent
    public FightingActor(ref Actor actor)
#pragma warning restore CS8618
    {
        Actor = actor;
        Health = Actor.MaxHealth;
        Mana = Actor.MaxMana;
    }

    public FightingActor SetupOpponent(ref Actor opponent)
    {
        FightingActor opponentFighter = new FightingActor(ref opponent);
        Opponent = opponentFighter;
        return opponentFighter;
    }

    #region Think (metódus) - comment
    /// <summary>
    /// <c>Think</c> metódus az NPC-k (Non-Playable Character) esetében dönti el, milyen képesséhet használjanak.
    /// </summary>
    #endregion
    public virtual Power? Think()
    {
        throw new NotImplementedException();
    }

    #region Attack (metódus) - comment
    /// <summary>
    /// <c>Attack</c> metódus az éppen harcoló karakter manaszintjét vizsgálva eldönti, hogy tudja-e a megadott képességet használni. Ez alapján fog az ellenfélre (<c>Opponent</c>) képességhez kötött sebzést mérni.
    /// True-t ad vissza, ha az ellenfél meghalt
    /// </summary>
    #endregion
    public bool Attack(Power power)
    {
        if (_Mana - power.Mana < 0) { 
            return false; 
        }
        else {
            _Mana -= power.Mana;
            Opponent.RecieveDamage(power.Damage);
            //TODO: Képernyő közepére jelenjen meg a szöveg 3 másodpercre, ami kiírja a power.DamageText-et.
            //BeautyWriter.Write($"{power.DamageText}");
            return true;
        }
    }
    #region DealDamage (metódus) - comment
    /// <summary>
    /// <c>DealDamage</c> metódus a bemeneti paraméterként megadott <c>damage</c> változó alapján a sebzés nagyságát vonja le a karakter ellenfelének életerejéből.
    /// </summary>
    #endregion
    public void RecieveDamage(ushort damage)
    {
        Health -= damage;
        //TODO: Hangeffekt lejátszás
        throw new NotImplementedException("Hangeffekt-lejátszás hiányzik");
    }
}
