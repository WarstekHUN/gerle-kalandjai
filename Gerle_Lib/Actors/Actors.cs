namespace Gerle_Lib.Actors
{
    public static class Actors
    {
        public static Actor Narrator = new Actor("Narrátor");
        public static Actor Gerle = new Actor("Gerle", new Power[] {
            new Power("Kajakommandó",20,50,false,"Gerle készített egy almás pitét ami megfeküdte az ellenfele gyomrát, ezért 50 életerőt veszetett."),
            new Power("Pálcasuhintás",20,50,false,"Gerle megsuhintotta pálcáját, amitől ellenfele varázsütésre 50 életerőt vesztett."),
            new Power("Mamuszdobás",50,80,false,"Gerle az acél lemezekből készült mamuszát fénysebességgel vágta ellenfeléhez, amitől ellenfele 80 életerőt vesztett."),
            //TODO: new Power("The potions",75,75,false,"Életerőtöltés")
            //TODO: new Power("Sprechen Sie Deutsche?",)
        });
        public static Actor Apolo = new Actor("Ápoló", new Power[] {
            new Power("Vérnyomásmérő",20,35,true,"Az ápoló kegyetlenül megszorította Gerle karját így főhősünk 20 életerőt vesztett."),
            //TODO: new Power("Gyógyszerek",20-at visz le Gerle manájából,50,true,"Az ápoló beadta Gerle napi gyógyszeradagját (nyugtatót) és Gerle 20 manát vesztett."),
            //TODO: new Power("Kényszerzubbony",45-öt visz le Gerle manájából,80,false,"Az ápoló kényszerzubbonyba kényszerítette Gerlét a következő körre ezáltal Gerle 45 manát vesztett."),
            new Power("Ágytál",30,45,true,"Marika fejbe kólintotta Gerlét egy ágytállal, amitől Gerle 30 eleterőt vesztett."),
        });
        public static Actor Galambok = new Actor("Galambok", new Power[] {
            new Power("Csipkedés",10,30,true,"A pajkos jómadarak megcsipkedték Gerle kezét, amitől ő 10 életerőt vesztett."),
            //TODO: new Power("Ganébomba",35-öt visz le Gerle manájából,70,false,"A galambraj szőnyegbombázást hajtott végre Gerle felett, így Gerle 35 manát vesztett."),
            new Power("Mi ez? Egy madár? Egy repülő?",35,60,false,"A galambraj egyenesen belerepült Gerle arcába, így Gerle 35 életerőt vesztett."),
        });
        public static Actor Jegykezelo = new Actor("Jegykezelő", new Power[] {
            //TODO: new Power("Vakítás",Megvakítja Gerlét/felcserélődik két képessége,80,false,"A furmányos kaller megvakítja scannerével Gerlét amitő ő két képessége randomizáltan felcserélődik."),
            //TODO: new Power("Mr. Worldwide",Ki tudja védeni a sprechen sie deutsche? képességet, valamint 15%-al erősebben fog a dodge után sebezni,50,false,"Ki tudja védeni a sprechen sie deutsche? képességet, valamint 15%-al erősebben fogja a dodge után Gerlét sebezni."),
            new Power("Bírságolás",25,40,true,"A kaller megbírságolta Gerlét amiért jegy nélkül utazik a buszon, Gerle 25 életerőt vesztett."),
            new Power("A tömeg ereje",35,50,false,"A dagi kaller elől nincs menekvés,a jegykezelő ráült Gerlére, a földdel tette egyenlőve. Gerle 35 életerőt vesztett."),
        });
        public static Actor Agressziv_kis_ovis = new Actor("Agresszív kis ovis", new Power[] {
            //TODO: new Power("Hisztéria",Ha 8 körnél hosszabb a harc sikít az ovis Gerle képességeinek a nevei 20%-ban eltorzulnak,60,false,"Gerle érzékszervei felmondják a szolgálatot."),
            //TODO: new Power("Legoland",30 életerő sebzés,120,true/Dodgeolható ha jó billentyű kombinációt nyomunk le.,"Ha nem volt jo a billenytűkombináció újabb 30 életerőt veszít Gerle."),
            //TODO: new Power("Délutáni szunya",20 életerőt tölt,40,false,"A kiscsoportos fenegyerek szunyókálás közben 20 életerőt gyűjtött."),
            new Power("Túlerő",80,160,false,"A túlerőben lévő kiscsoportosok könnyű szerrel vontak le Gerle életerejéből 80-at."),
            //TODO: new Power("Sírás a szülőknek",Gerlét lefoglajlák az ovis szülei/ ellátja sebeit az ovodás 40 életerőre tesz szert,70,false,"40 életerőre tett szert az óvodás."),
        });
        public static Actor Dajka_Laura_neni = new Actor("Dajka (Laura néni)", new Power[] {
            new Power("Kinderkommandó",20,140,false,"Toddlers! Assemble! - jelmondattal hívja harcba Laura néni a csoport többi mozgósítható tagját. Gerle 20 életerőt vesztett."),
            //TODO: new Power("Kajadobálás",Gerle képességei nevének minden 3-dik betűje látszódik csak,140,false,"A dajka a  megmaradt ebédet Gerle arcához vágta. Gerle nem lát ki a "),
            //TODO: new Power("Párna páncél",Ha Laura neni élete már kevés (40 alatt van) pajzsot használ,60,false,"Hárítja Gerle következő támadását."),
            //TODO: new Power("A Férj",70% heal,50,,"Ha sokáig húzódik a csata (10 kör), a dajkának a férje képes megvédeni őt. Ez időt ad a dajka számára, aki így képes visszatölteni teljes életerőének 70%-át."),
            new Power("Plüssmackó",45,45,true,"A dajka két kővel kitömött plüssmackót vágott Gerléhez amitől Gerle 45 életerőt vesztett."),
        });
        public static Actor Unoka = new Actor("Unoka", new Power[] {});
    }
}
