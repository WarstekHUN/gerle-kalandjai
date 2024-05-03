public class Actor
{
    public string Name = "Default Actor";
    public const ushort MaxHealth = 100;
    public const ushort MaxMana = 50;
    public Power[] Powers { get; set; }

    public Actor(string name, Power[] powers)
    {
        Name = name;
        Powers = powers;
    }
}