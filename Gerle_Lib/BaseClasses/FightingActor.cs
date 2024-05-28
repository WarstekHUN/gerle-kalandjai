using Gerle_Lib.BaseClasses;
#region FightingActor (osztály) - comment
#endregion
/// <summary>
/// <c>FightingActor</c> osztály az éppen lezajló harc résztvevőinek (<c>Actor</c>) adatait, a harc menetét kezeli (<c>FightingActor.cs</c>). Az <c>Actor</c> osztály tulajdonságait konstruktorait örökli meg.
/// </summary>
public class FightingActor : Actor
{
    #region Health (mező) - comment
    /// <summary>
    /// <c>Health</c> mező az éppen harcoló karakter életerejét tartalmazza.
    /// </summary>
    #endregion
    private ushort Health;
    #region Mana (mező) - comment
    /// <summary>
    /// <c>Mana</c> mező az éppen harcoló karakter manaszintjét tartalmazza.
    /// </summary>
    #endregion
    private ushort Mana;
    #region Opponent (mező) - comment
    /// <summary>
    /// <c>Opponent</c> mező az éppen harcoló karakter ellenségét tartalmazza.
    /// </summary>
    #endregion
    private FightingActor Opponent;
    #region FightingActor (paraméteres konstruktor) - comment
    /// <summary>
    /// <c>FightingActor</c> paraméteres konstruktor az éppen harcoló karakter életerejét, manaszintjét és ellenfelét adja meg. Minden harcot maximális életerővel és manszinttel kezdik meg a karakterek.
    /// </summary>
    #endregion
    public FightingActor(string name, ref Actor opponent, Power[] powers) : base(name)
    {
        Health = MaxHealth;
        Mana = MaxMana;
        Opponent = (FightingActor)opponent;
    }
    #region Think (metódus) - comment
    /// <summary>
    /// <c>Think</c> metódus az NPC-k (Non-Playable Character) esetében dönti el, milyen képesséhet használjanak.
    /// </summary>
    #endregion
    public void Think()
    {
        if (Mana >= Powers[0].Mana)
        {
            Attack(Powers[0]);
        }
    }
    #region Attack (metódus) - comment
    /// <summary>
    /// <c>Attack</c> metódus az éppen harcoló karakter manaszintjét vizsgálva eldönti, hogy tudja-e a megadott képességet használni. Ez alapján fog az ellenfélre (<c>Opponent</c>) képességhez kötött sebzést mérni.
    /// </summary>
    #endregion
    public bool Attack(Power power)
    {
        BeautyWriter bw = new BeautyWriter();
        if (Mana - power.Mana < 0) { 
            bw.Write(($"{Name} próbált támadni a {power.Name} képességgel, de nem volt elég mana!")); 
            return false; 
        }
        else {
        Mana -= power.Mana;
        Opponent.DealDamage(power.Damage);
        bw.Write($"{Name} támadott a {power.Name} képességgel, {power.Damage} sebzést okozva!");
        return true;
        }
    }
    #region DealDamage (metódus) - comment
    /// <summary>
    /// <c>DealDamage</c> metódus a bemeneti paraméterként megadott <c>damage</c> változó alapján a sebzés nagyságát vonja le a karakter ellenfelének életerejéből.
    /// </summary>
    #endregion
    public void DealDamage(ushort damage)
    {
        BeautyWriter bw = new BeautyWriter();
        Health = (ushort)Math.Max(Health -  damage, 0);
        bw.Write($"{Name} {damage} sebzést szenvedett el. Hátralevő életereje: {Health}");
        //TODO: Hangeffekt lejátszás
        throw new NotImplementedException("Hangeffekt-lejátszás hiányzik");
    }
}
