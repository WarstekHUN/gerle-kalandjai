using Gerle_Lib.BaseClasses;
using Gerle_Lib.BaseClasses.Powers;
using Gerle_Lib.Controllers;
using System.Runtime.CompilerServices;

namespace Gerle_Lib.Data
{
    public static class Actors
    {
        public static Actor Narrator = new Actor("Narrátor");
        public static Actor Gerle = new Actor("Gerle", new Power[] {
            new Power("Kajakommandó",20,50,false,"Gerle készített egy almás pitét ami megfeküdte az ellenfele gyomrát, ezért 50 életerőt veszetett."),
            new Power("Pálcasuhintás",20,50,false,"Gerle megsuhintotta pálcáját, amitől ellenfele varázsütésre 50 életerőt vesztett."),
            new Power("Mamuszdobás",50,80,false,"Gerle az acél lemezekből készült mamuszát fénysebességgel vágta ellenfeléhez, amitől ellenfele 80 életerőt vesztett."),
            new SpecialPower("The potions", 75, false, "Gerle megivott egy kupicával megboldogult férjének házi páleszéből, ezért életereje 75-el nőtt.", (SpecialPower thisPower, ref FightingActor currentActor, ref FightingActor opponent) =>
            {
                currentActor.Health += 75;
            }),
            new SpecialPower("Sprechen Sie Deutsche?", 120, false, "Gerle német nyugdíjasnak tettette magát. Az ellenfele összezavarodottságában mostantól 10%-al gyengébben támad.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            {
                if(opp.Actor == Jegykezelo)
                {
                    thisPower.DamageText = "Gerle megpróbálta magát német nyugdíjasnak tetteni, de a jegykezelő tudott németül. A kaller mostantól 15%-al erősebben támad.";
                    opp.DamageModifier = 1.15f;
                }else
                {
                    current.DamageModifier = 1.1f;
                }
            })
        });
        public static Actor Apolo = new Actor("Ápoló", new Power[] {
            new Power("Vérnyomásmérő",20,35,true,"Az ápoló kegyetlenül megszorította Gerle karját így főhősünk 20 életerőt vesztett."),
            new SpecialPower("Gyógyszerek", 20, false, "Az ápoló beadta Gerle napi gyógyszeradagját (nyugtatót), ezért Gerle 20 manát vesztett.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            {
                opp.Mana -= 20;
            }),
            new SpecialPower("Kényszerzubbony",80,false,"Az ápoló kényszerzubbonyba kényszerítette Gerlét a következő körre ezáltal Gerle 45 manát vesztett.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            {
                opp.Mana -= 45;
            }),
            new Power("Ágytál",30,45,false,"Marika fejbe kólintotta Gerlét egy ágytállal, amitől Gerle 30 eleterőt vesztett."),
        });
        public static Actor Galambok = new Actor("Galambok", new Power[] {
            new Power("Csipkedés",10,30,false,"A pajkos jómadarak megcsipkedték Gerle kezét, amitől ő 10 életerőt vesztett."),
            new SpecialPower("Ganébomba",70,false,"A galambraj szőnyegbombázást hajtott végre Gerle felett, így Gerle 35 manát vesztett.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            {
                opp.Mana -= 35;
            }),
            new Power("Mi ez? Egy madár? Egy repülő?",35,60,false,"A galambraj egyenesen belerepült Gerle arcába, így Gerle 35 életerőt vesztett."),
        });
        public static Actor Jegykezelo = new Actor("Jegykezelő", new Power[] {
            //A Mr. WorldWide egy passzív képesség, nem kell lescriptelni ide.
             new SpecialPower("Vakítás",80,false,"A furmányos kaller megvakítja scannerével Gerlét amitől az ő képességei közül kettő randomizáltan felcserélődik.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            {
               var powers = opp.Actor.Powers;
                if (powers.Length > 1)
                {
                    Random rand = new Random();
                    int index1 = rand.Next(powers.Length);
                    int index2;
                    do
                    {
                        index2 = rand.Next(powers.Length);
                    } while (index1 == index2);

                    var temp = powers[index1];
                    powers[index1] = powers[index2];
                    powers[index2] = temp;
                }
            }),
            new Power("Bírságolás",25,40,true,"A kaller megbírságolta Gerlét amiért jegy nélkül utazik a buszon, Gerle 25 életerőt vesztett."),
            new Power("A tömeg ereje",35,50,false,"A dagi kaller elől nincs menekvés,a jegykezelő ráült Gerlére, a földdel tette egyenlőve. Gerle 35 életerőt vesztett."),
        });
        public static Actor Agressziv_kis_ovis = new Actor("Agresszív kis ovis", new Power[] {
           new SpecialPower("Hisztéria",60,false,"Ha 8 körnél hosszabb a harc, Gerle képességeinek a nevei 20%-ban eltorzulnak.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            {
                opp.DamageModifier = 0.8f;
            }),

            //TODO: new Power("Legoland",30 életerő sebzés,120,true/Dodgeolható ha jó billentyű kombinációt nyomunk le.,"Ha nem volt jo a billenytűkombináció újabb 30 életerőt veszít Gerle."),
            new HealingPower("Délutáni szunya", 40, 20, "A kiscsoportos fenegyerek szunyókálás közben 20 életerőt gyűjtött."),
            new Power("Túlerő",80,160,false,"A túlerőben lévő kiscsoportosok könnyű szerrel vontak le Gerle életerejéből 80-at."),
            new HealingPower("Sírás a szülőknek", 70, 40, "Gerlét lefoglalják az ovis szülei, ellátja sebeit az ovodás, 40 életerőre tesz szert."),
        });
        public static Actor Dajka_Laura_neni = new Actor("Dajka (Laura néni)", new Power[] {
            new Power("Kinderkommandó",20,140,false,"Toddlers! Assemble! - jelmondattal hívja harcba Laura néni a csoport többi mozgósítható tagját. Gerle 20 életerőt vesztett."),
            //TODO: new Power("Kajadobálás",Gerle képességei nevének minden 3-dik betűje látszódik csak,140,false,"A dajka a  megmaradt ebédet Gerle arcához vágta. Gerle nem lát ki a "),
            //new SpecialPower("Kajadobálás",140,false,"Gerle képességei nevének minden 3-dik betűje látszódik csak.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            //{

            //}),
            //TODO: new Power("Párna páncél",Ha Laura neni élete már kevés (40 alatt van) pajzsot használ,60,false,"Hárítja Gerle következő támadását."),
            // new SpecialPower("Párna páncél",60,false,"Ha Laura néni élete már kevés (40 alatt van), pajzsot használ, hárítja Gerle következő támadását.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            //{

            //}),
             new SpecialPower("A Férj", 50, true, "Ha a csata több mint 10 körig tart, Laura néni életereje 70%-kal nő.", (SpecialPower thisPower, ref FightingActor current, ref FightingActor opp) =>
            {
                if (SceneController.Turn == 10){
                    current.Health += (ushort)(current.Health * 1.7);
                }
            }),
            new Power("Plüssmackó",45,45,true,"A dajka két kővel kitömött plüssmackót vágott Gerléhez amitől Gerle 45 életerőt vesztett."),
        });
        public static Actor Unoka = new Actor("Unoka");

        public static Actor Jezus = new Actor("Jézus");

        public static Actor Portas = new Actor("Portás");

        public static Actor Sanyi = new Actor("Sanyi");

        public static Actor Anyuka = new Actor("Anyuka");

        public static Actor Random_csavo = new Actor("Random csávó");

        public static Actor Fiminista = new Actor("Femináci");

    }
}
