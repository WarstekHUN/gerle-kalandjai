using Gerle_Lib.BaseClasses;
using Gerle_Lib.BaseClasses.Powers;
using Gerle_Lib.Exceptions;
#region FightingActor (osztály) - comment
#endregion
/// <summary>
/// <c>FightingActor</c> osztály az éppen lezajló harc résztvevőinek (<c>Actor</c>) adatait, a harc menetét kezeli (<c>FightingActor.cs</c>). Az <c>Actor</c> osztály tulajdonságait konstruktorait örökli meg.
/// </summary>
public class FightingActor
{
    public Actor Actor { get; init; }

    private ushort _Health { get; set; } = Actor.MaxHealth;

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

    private ushort _Mana { get; set; } = Actor.MaxMana;

    #region Mana (mező) - comment
    /// <summary>
    /// <c>Mana</c> mező az éppen harcoló karakter manaszintjét tartalmazza.
    /// </summary>
    #endregion
    public ushort Mana
    {
        get => _Mana;
        set {
            _Mana = Math.Max(value, (ushort)0);

        }
    }

    /// <summary>
    /// A karakter minden támadásakor az alap sebzése beszorzásra kerül ezzel az értékkel.
    /// </summary>
    public float DamageModifier { get; set; } = 1f;

    #region Opponent (mező) - comment
    /// <summary>
    /// <c>Opponent</c> mező az éppen harcoló karakter ellenségét tartalmazza.
    /// </summary>
    #endregion
    public FightingActor Opponent { get; set; }

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
    /// <exception cref="ActorIsNotFighterException"></exception>
    #endregion
    public virtual List<Power> Think()
    {
        List<Power> chosenPowers = new List<Power>();

        if (_Mana == 0) return chosenPowers;
        if (Actor.Powers is null) throw new ActorIsNotFighterException();
        if (_Mana < Actor.LeastManaExpensiveAttackingPower!.Mana) return chosenPowers;

        ushort virtualMana = _Mana;
        List<Power> notUsedPowers = Actor.Powers.Where(el => el.Mana <= Mana && el is not HealingPower && (el.IsUsed is null || el.IsUsed == false)).ToList();
        List<Power> affordableNotUsedPowers() => notUsedPowers.Intersect(Actor.Powers.Where(el => el.Mana <= Mana)).ToList();

        void UsePower(Power power)
        {
            virtualMana -= power.Mana;
            notUsedPowers.Remove(power);
            chosenPowers.Add(power);
            if (power.IsUsed is not null) power.IsUsed = true;
        }

        //Az ellenfél életereje kisebb-e, mint a legnagyob damageű elérhető támadás? Van rá elég mana?
        if (Actor.MostDamagingPower?.Damage >= Opponent.Health && Actor.MostDamagingPower.Mana <= Mana)
        {
            //Jobban megéri inkább támadni, mint védekezni.
            UsePower(Actor.MostDamagingPower);
        }
        else
        {
            //Inkább healelni érdemesebb
            //Kell-e healelni? Van-e healelő képesség?
            if (Health <= 35 && Actor.HealingPowers?.Length > 0)
            {
                //Elhasználom azt a képességet, ami a legtöbb életerőt tölti, de még meg tudom venni a jelenlegi manámból.
                UsePower(Actor.HealingPowers.Where(el => el.IsUsed is not null || el.IsUsed == false).OrderBy(el => el.Health).ThenBy(el => el.Mana).First(el => el.Mana < Mana));
            }
        }

        //Eldöntjük, hogy akarunk-e támadni, maradt-e még elhasználható mana
        if (Random.Shared.Next(Actor.Aggression - 5, 10) >= 3 && virtualMana >= Actor.LeastManaExpensiveAttackingPower.Mana)
        {
            //Támadunk
            //Ha 5-ös az aggresszió, akkor minimum 0-szor, maximum 2-ször támadunk
            byte numberOfAttackTries = (byte)Random.Shared.Next(0, Math.Max(Actor.Aggression - 3, 1) + 1);

            byte i = 0;

            while (virtualMana >= Actor.LeastManaExpensiveAttackingPower.Mana && i < numberOfAttackTries)
            {
                //Összes képesség, amire van elég Mana
                List<Power> currentlyAvailablePowers = affordableNotUsedPowers();
                UsePower(currentlyAvailablePowers[Random.Shared.Next(0, currentlyAvailablePowers.Count)]);
                i++;
            }
        }

        return chosenPowers;
    }

    #region Attack (metódus) - comment
    /// <summary>
    /// <c>Attack</c> metódus az éppen harcoló karakter manaszintjét vizsgálva eldönti, hogy tudja-e a megadott képességet használni. Ez alapján fog az ellenfélre (<c>Opponent</c>) képességhez kötött sebzést mérni.
    /// True-t ad vissza, ha az ellenfél meghalt
    /// </summary>
    #endregion
    public bool Attack(Power power)
    {
        if (_Mana - power.Mana < 0)
        {
            return false;
        }

        _Mana -= power.Mana;
        Opponent.RecieveDamage((ushort)Math.Round(power.Damage * DamageModifier));
        //TODO: Képernyő közepére jelenjen meg a szöveg 3 másodpercre, ami kiírja a power.DamageText-et.
        //BeautyWriter.Write($"{power.DamageText}");
        return Opponent.Health == 0;
    }
    #region DealDamage (metódus) - comment
    /// <summary>
    /// <c>DealDamage</c> metódus a bemeneti paraméterként megadott <c>damage</c> változó alapján a sebzés nagyságát vonja le a karakter ellenfelének életerejéből.
    /// </summary>
    #endregion
    public virtual void RecieveDamage(ushort damage)
    {
        short tempHealth = (short)Health;
        short result = (short)(tempHealth - (short)damage);
        if (result <= 0)
        {
            Health = 0;
        }
        else
        {
            Health = (ushort)result;
        }


        //TODO: Hangeffekt lejátszás
        //throw new NotImplementedException("Hangeffekt-lejátszás hiányzik");
    }
}
