public class FightingActor : Actor
{
    private ushort Health;
    private ushort Mana;
    private FightingActor Opponent;

    public FightingActor(string name, ref Actor opponent, Power[] powers) : base(name, powers)
    {
        Health = MaxHealth;
        Mana = MaxMana;
        Opponent = (FightingActor)opponent;
    }

    public void Think()
    {
        throw new NotImplementedException();
    }

    public bool Attack(Power power)
    {
        if (Mana - power.Mana < 0) return false;

        Opponent.DealDamage(power.Damage);
        return true;
    }

    public void DealDamage(ushort damage)
    {
        Health = (ushort)Math.Max(Health -  damage, 0);
        //TODO: Hangeffekt lejátszás
        throw new NotImplementedException("Hangeffekt-lejátszás hiányzik");
    }
}
