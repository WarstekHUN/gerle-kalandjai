public class FightingActor : Actor
{
    private ushort currHP;
    private ushort currMana;
    private FightingActor opponent;

    public FightingActor(Power[] powers) : base(powers)
    {
        currHP = MaxHP;
        currMana = MaxMana;
    }

    public void SetOpponent(FightingActor newOpponent)
    {
        opponent = newOpponent;
    }

    public void Think()
    {

    }

    public void Attack(Power power)
    {

    }

    public void DealDamage(ushort damage)
    {
        currHP = (ushort)(currHP > damage ? currHP - damage : 0);
    }
}
