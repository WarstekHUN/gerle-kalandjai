using System;

namespace GameModel
{
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

    public class Line
    {
        public string Text { get; set; }
        public Actor ActorRef { get; set; }
        public string VoiceFile { get; set; }
        public string NoiseFile { get; set; }
    }

}
