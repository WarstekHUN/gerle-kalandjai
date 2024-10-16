using Gerle_Lib.Exceptions;
using Gerle_Lib.UIReleated;

namespace Gerle_Lib.BaseClasses
{
    public class Player : FightingActor
    {
        public Player(ref Actor actor) : base(ref actor) { }

        /// <summary>
        /// Felülírja az alap Think karakter-"AI"-t egy grafikus kezelőfelülettel
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ActorIsNotFighterException"></exception>
        public override List<Power> Think()
        {
            if (Actor.Powers != null && Actor.Powers.Length > 0)
                return UI.FightingUI(Actor.Powers, true, Mana, (short)Opponent.Health, Health, Opponent.Actor.Name);
            else
                throw new ActorIsNotFighterException();
        }
    }
}
