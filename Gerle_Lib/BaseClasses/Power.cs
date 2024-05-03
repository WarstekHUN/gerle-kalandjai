public class Power
{
    public string Name { get; init; }
    public ushort Damage { get; init; }
    public ushort Mana { get; init; }
    public string? DodgeText { get; init; }

    public bool IsDodgeable
    {
        get => DodgeText is not null;
    }

    public Power(string name, ushort damage, ushort mana, string? dodgeText)
    {
        Name = name;
        Damage = damage;
        Mana = mana;
        DodgeText = dodgeText;
    }

    public virtual void Minigame()
    {
        throw new NotImplementedException();
    }
}
