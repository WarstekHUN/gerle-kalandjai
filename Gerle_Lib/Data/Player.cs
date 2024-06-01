using Gerle_Lib.BaseClasses;
namespace Gerle_Lib.Data
{
    public class Player : FightingActor
    {
        public Player(ref Actor actor) : base(ref actor) {}

        /// <summary>
        /// Felülírja az alap Think karakter-"AI"-t egy grafikus kezelőfelülettel
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override List<Power> Think()
        {
            //TODO: Enemy Healthbar, felette enemy nevének megjelenítése
            //TODO: Player healthbar, manabar
            throw new NotImplementedException();
        }
    }
}
