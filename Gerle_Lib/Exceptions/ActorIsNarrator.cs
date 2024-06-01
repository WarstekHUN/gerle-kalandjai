namespace Gerle_Lib.Exceptions
{
    public class ActorIsNotFighterException : Exception 
    {
        public ActorIsNotFighterException() : base("Olyan Actorra lett meghívva egy harci függvény, aminek nincsenek képességei.")
        {

        }    
    }
}
