public class Actor
{
    public const string Name = "Default Actor";
    public const ushort MaxHP = 100;
    public const ushort MaxMana = 50;
    public Power[] Powers { get; set; }

    public Actor(Power[] powers)
    {
        Powers = powers;
    }
}