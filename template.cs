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
            // Implement AI thinking logic for NPC attacks
        }

        public void Attack(Power power)
        {
            // Implement attacking logic here
        }

        public void DealDamage(ushort damage)
        {
            currHP = (ushort)(currHP > damage ? currHP - damage : 0);
        }
    }

}
