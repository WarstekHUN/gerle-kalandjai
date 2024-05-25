namespace Gerle_Lib.Actors
{
    public static class Actors
    {
        public static Actor Gerle = new Actor("Gerle", new Power[] {
            new Power("Kajakommandó",20,50,false,"Gerle készített egy almás pitét ami megfeküdte az ellenfele gyomrát, ezért 50 életerőt veszetett."),
            new Power("Pálcasuhintás",20,50,false,"Gerle megsuhintotta pálcáját, amitől ellenfele varázsütésre 50 életerőt vesztett."),
            new Power("Mamuszdobás",50,80,false,"Gerle az acél lemezekből készült mamuszát fénysebességgel vágta ellenfeléhez, amitől ellenfele 80 életerőt vesztett."),
            //TODO: new Power("The potions",75,75,false,"Életerőtöltés")
            //TODO: new Power("Sprechen Sie Deutsche?",)
        });

    }
}
