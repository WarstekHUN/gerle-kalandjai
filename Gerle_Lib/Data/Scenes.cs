using Gerle_Lib.Data;
using Gerle_Lib.BaseClasses;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Spectre.Console;
using System.Numerics;

namespace Gerle_Lib.Controllers
{
    public static partial class SceneController
    {
        #region Scenes (mező) - comment
        /// <summary>
        /// <c>Scenes</c> mező tartalmazza az összes jelenetet. Ez a játék jeleneteinek tömbje.
        /// </summary>
        #endregion
        //TODO: Ezt itt feltölteni jelenetekkel
        private static Scene[] Scenes = {
            new Scene(new Line[]
            {
                new Line("Egy szép csodás szerdai napon Gerle telefonja egyszer csak megcsörren.", ref Actors.Narrator, "01/01_narrátor.mp3"),
                new Line("Haló! Szia Kicsim! Igeen, minden rendben köszönöm... Na mi történt... Hogy micsodaa?? Dehát ezt nem teheti meg... Najó akkor végére járok ennek... Puszi Drágám, szeretlek!", ref Actors.Gerle, "01/02_Gerle.mp3"),
                new Line("Az ápoló bejön az ajtón", ref Actors.Narrator, "01/03_narrátor.mp3"),
                new Line("Szép napot Gerle! Meséljen, hogyan érzi magát. Minden rendben volt az este folyamán? Nem volt hideg a szobában, nem fázott?", ref Actors.Apolo, "01/04_apolo.mp3"),
                new Line("Szép napot aranyoskám! Nem, minden a legtökéletesebb volt, de képzelje mi történt. Az imént hívott a kislányom: Azt mondta, hogy az én kis egyetlen unokámat bent akarja tartani a dajka" +
                    " este 9-ig és elvileg tegnap is ezt csinálta.", ref Actors.Gerle, "01/05_Gerle.mp3"),
                new Line("Jézusom ez szörnyű! Hogyan tehet ilyet valaki!", ref Actors.Apolo, "01/06_apolo.mp3"),
                new Line("Így van picinyem. Szóval úgy döntöttem, hogy megmentem őt.", ref Actors.Gerle, "01/07_Gerle.mp3"),
                new Line("Dehát Gerle ön is jól tudja, hogy nem hagyhatja el az öregek otthonát, csak akkor, ha 24 órával előtte bead egy kérelmező beadványt 3 példányban," +
                    " amit utána beküldtek, visszaküldtek, félretettek, elveszítettek, megtaláltak, kivizsgáltak, újra elveszítettek, 3 hónapra tőzeg alá temettek, és gyújtósként hasznosítottak az öregek otthona vezetősége által." +
                    " Esetleg akkor távozhat el még, ha sürgős orvosi ellátásra van szüksége, de azt is csak akkor, hogyha az önt elszállítók tudják igazolni, " +
                    "hogy az Országos Mentő Szolgálatnál folytatnak aktív munkaviszonyt, vagy pedig akut halál beállta igazolódott!", ref Actors.Apolo, "01/08_apolo.mp3"),
                new Line("Nem érdekel, nekem meg KELL megmentenem az édes kis unokámat!", ref Actors.Gerle, "01/09_Gerle.mp3"),
                new Line("Sajnálom, de nem engedhetem ezt.", ref Actors.Apolo, "01/10_apolo.mp3"),
                new Line("Erről is a szőröstalpúak tehetnek!", ref Actors.Gerle, "01/11_Gerle.mp3"),
            }, ref Actors.Apolo, new SceneMusic("Harc1_intro_180-bpm", "Harc1_kozep-loop_180-bpm", "Harc1_outro_180-bpm", 180)), //Ez egy olyan jelenet, aminek a végén van harc. A SceneMusic a harc zenéjét veszi be. Csak a fájlnevek kellenek, amik bele vannak rakva a Data/Audio/Music mappába. Fájlkiterjesztés sem kell.
        
            new Scene(new Line[]{
                new Line("Pfúú ez kemény volt. De ne aggódj kicsikém jön már érted a nagyikád. Remélem Marika nem csinál nagyobb problémát", ref Actors.Gerle, "02/01_Gerle.mp3"),
                new Line("Gerle elmegy a legközelebbi buszmegállóhoz. A menetrend szerint 5 perc múlva jön a következő busz. Unalmában elővesz egy szendvicset, és beleharap. Jóízűen szív mélyet a szendvicsből érkező illatokba. " +
                    "Eközben egy macska dörgölőzik a lábához, a szendvicsért kuncsorogva." +
                    " Bélától, a benti elosztótól mindig rendes adagokat kapott, erre most ez a macska el akarja előle enni. Hiába, kemény hely ez az idősek otthona.", ref Actors.Narrator, "02/02_narrátor.mp3"),
                 new ChoiceScreen(new Choice[]
                {
                    new Choice("Nagylelkű leszel és adsz egy kis elemózsiát a cicának ezzel példát mutatsz a burzsoáziának.", SceneVersion.A),
                    new Choice("Bunkó leszel és nem adsz neki semmi kaját mert, eszedbe jut, hogy véletlen reggel saláta helyett füvet használtál az elkészítésekor.", SceneVersion.B)
                }, "03/01_narrátor_A_B.mp3"),
            }),

            new Scene(new Line[]{
                new Line("Gerle végre felszáll a buszra. A buszon már csak egyetlen ülőhely maradt. Ez egy alkoholtól megboldogult férfi mellett volt, pedig még csak reggel 10 óra. Mivel nem tehetett mást, így leült mellé. " +
                    "Egyszer csak lenéző szemekkel megjelenik egy 170cm-es 120kg-os ember: Ottó, a kaller. " +
                    "Először az alkeszhez fordul, akit egy kis vita után le is szállít, majd Gerléhez „gurul”:", ref Actors.Narrator, "03/03_narrátor.mp3"),
                new Line("Üdv Hölgyem! Jegyeket és bérleteket.", ref Actors.Jegykezelo, "03/04_Kaller.mp3"),
                new Line("Szép napot uram! Én már a nyugdíjas éveimben vagyok nekem már nem kell bérletet használnom.", ref Actors.Gerle, "03/05_Gerle.mp3"),
                new Line("Akkor kérem, azonnal mutassa meg valamelyik az ön személyazonosságát igazoló okmányát.", ref Actors.Jegykezelo, "03/06_Kaller.mp3"),
                new Line("Jaj, édesem, nagyon sietek az unokámhoz, nincsenek nálam az irataim. Képzelje a dajkája este 9-ig bent akarja tartani őt. Meg kell, hogy mentsem!", ref Actors.Gerle, "03/07_Gerle.mp3"),
                new Line("Nem érdekel, mutassa valamilyen igazolványát.", ref Actors.Jegykezelo, "03/08_Kaller.mp3"),
                new Line("De hát nincsenek nálam, nagyon siettem, és az otthonban felejtettem. Kérem nézze el most ezt egy ilyen törékeny és kedves öreg nénikének.", ref Actors.Gerle, "03/09_Gerle.mp3"),
                new Line("Így járt, nagyika! Nincs igazolvány? Kap a helyébe egy a Budapesti Közlekedési Központ által meghatározott mértékben kiszabadandó 50.000 magyar forint nagyságértékű pénzbírságmennyiséget!", ref Actors.Jegykezelo, "03/10_Kaller.mp3"),
                new Line("De hát nekem pont annyi a nyugdíjam, nem teheti ezt! Ez ellen muszáj tennem valamit!", ref Actors.Gerle, "03/11_Gerle.mp3"),
                }, ref Actors.Jegykezelo, new SceneMusic("Harc2_intro_180-bpm", "Harc2_kozep-loop_180-bpm", "Harc2_outro_180-bpm", 180)),

             new Scene(new Line[]{
                new Line("Ezek a gonosz kallerek mindig csak megakarnak büntetni. Sosem hiszik el, hogy már nyugdíjas vagyok, ha nincs nálam igazolvány. Bár én ezt egy kicsit bóknak is veszem...", ref Actors.Gerle, "04/01_Gerle.mp3"),
                new Line("Kis idő elteltével Gerle leszáll a buszról. Bár már nem választotta el őt nagy távolság az unokájától, fájó térdei miatt ez mégis soknak tűnt. Mikor már az útjának majdnem a felét megtette, megállt. " +
                    "Szegény nagyon elfáradt így kénytelen volt egy padra leülve pihenni. Miközben a tájban gyönyörködött, megéhezett és elővette a szendvicsét majd elkezdte jóízűen enni. " +
                    "Kisvártatva egyre több galamb jelent meg körülötte. Valószínűleg megérezték a szendvics mennyei illatát. Gerle számára ez nem volt különös, ugyanis már többször járt Velencében, a Szent-Márk téren. Mondjuk a legutóbbi alkalom már több mint 20 éve volt. " +
                    "Miután csipegettek a morzsákból, a galambok egyszerűen megvadultak, teljesen kikeltek magukból. ", ref Actors.Narrator, "04/02_narrátor.mp3"),
                new Line("ÚRISTEN, hát ezekkel meg mi lett!?", ref Actors.Gerle, "04/03_Gerle.mp3"),
                new Line("Gerlének ekkor tűnt fel, hogy valami tényleg nincsen rendben azzal a szendviccsel. " +
                    "Ahogy ette, csak ette, egyre jobban ellazult, és jobb kedvre derült, csak a galambok nem csillapodtak és egyre vadabbak lettek.", ref Actors.Narrator, "04/04_narrátor.mp3"),
                new Line("EZEK MEGTÁMADTAK, SEGÍTSÉG!", ref Actors.Gerle, "04/05_Gerle.mp3"),
                new Line("Mikor kiabálni kezdett, a körülötte lévő emberek nagyon furcsán néztek rá, ugyanis egy galamb, sőt egy lélek se volt Gerle 20 méteres körzetében.", ref Actors.Narrator, "04/06_narrátor.mp3"),
                new Line("Csákány, muter! Pakoljá kifelé! Szétcsípdesem a gégéd!", ref Actors.Galambok, "04/07_galamb.mp3"),
                new Line("Aztán semmi próbálkozás, mer’ véged, te bányarém!", ref Actors.Galambok, "04/08_galamb2.mp3"),
                new Line("*galamb hangok*", ref Actors.Galambok, "04/09_galamb_hangok.mp3"),
                new Line("Miiiiiiiiiiiiiiiiii!? Hát adok én nektek mindjárt valamit, de azt nem teszitek zsebre! Huligánok!", ref Actors.Gerle, "04/10_Gerle.mp3"),
                }, ref Actors.Galambok, new SceneMusic("Harc3_intro_180-bpm.mp3", "Harc3_kozep_loop_180-bpm.mp3", "Harc3_outro_180-bpm.mp3", 180)),

             new Scene(new Line[]{
               new Line("Miután eltelt körülbelül 2 óra, Gerle már újra rendben volt.", ref Actors.Narrator, "05/01_narrátor.mp3"),
               new Line("Hol vagyok? Mennyi az idő? ... Úristen már fél 5 van nekem, nekem ilyenkor már aludnom kéne. Na várjunk, de miért is vagyok itt?", ref Actors.Gerle, "05/02_Gerle.mp3"),
               new Line("Gerle gondolkodik egy pár percet, majd eszébe jut, hogy az unokájáért indult, és már majdnem ott is van. Nem is tétovázik tovább, elindul az ovi felé." +
                   " Kisvártatva betoppan. Mikor körbe néz, elég érdekes alakokat lát. Két furcsa kinézetű apuka beszélget a váróban. " +
                   "Az egyik egy dupla copfos ürge, a másik egy hosszú rasztás hajó fickó volt. Mindketten fekete pólót hordtak, amire az volt írva, hogy free valami. " +
                   "Sajnos Gerle nem tudta teljesen elolvasni, biztosan rockkerek lehettek, azért voltak fekete pólóban. Egy harmadik apuka is megjelenik. " +
                   "Ő teljesen magán kívül van, a közel múltban történtek miatt. Azt ordította a telefonba, hogy:", ref Actors.Narrator, "05/03_narrátor.mp3"),
               new Line("Értsd már meg, hogy nem körülötted forog a világ, vannak rajtad kívül más emberek is!", ref Actors.Random_csavo, "05/04_random_csavo.mp3"),
               new Line("Illetve a váróban egy teljesen kiakadt anyuka is ott volt. Gerle féle fordul, hogy megkérdezze mi a baj.", ref Actors.Narrator, "05/05_narrátor.mp3"),
               new Line("Jajj, édesem mi a baj?", ref Actors.Gerle, "05/06_Gerle.mp3"),
               new Line("A gyerekemet itt bent tartja ez a gonosz óvónő és már nagyon szeretném hazavinni. Valterkám gyere haza, otthon a tesód, Peti már nagyon vár.", ref Actors.Anyuka, "05/07_anyuka.mp3"),
               new Line("Ugyan ne aggódj, én megszabadítom a gyerekeket ettől a banyától.", ref Actors.Gerle, "05/08_Gerle.mp3"),
               new Line("Gerle ezután nagy léptekkel indul a csoport szobája felé, viszont mikor az ajtóhoz ér, egy kicsi, ám agresszív óvodás az útját állja.", ref Actors.Narrator, "05/09_narrátor.mp3"),
               new Line("Édeském, hát miért vagy ilyen ideges?", ref Actors.Gerle, "05/10_Gerle.mp3"),
               new Line("MERT LAURA NÉNI ELVETTE AZ IPAD 18 PRO-MAT ÉS MÉG AZ AIRPODS 67 PRO MAX ULTRA X-EMET IS! NEM HISZEM EL! ADJA VISSZA! ADJA VISSZA! ADJA VISSZA! ADJA VISSZA! L.L. JUNIORT AKAROK HALLGATNI! „T’aves Baxtalo, shavale, T’aves Baxtalo", ref Actors.Agressziv_kis_ovis, "05/11_AKO.mp3"),
             }, ref Actors.Agressziv_kis_ovis, new SceneMusic("Harc4_intro_90-bpm.mp3", "Harc4_kozep_90-bpm.mp3", "Harc4_outro_90-bpm.mp3", 90)),

             new Scene(new Line[]{
               new Line("NA JÓLVAN MOSTMÁR! ELEGEM VAN EBBŐL... ITT MINDENKI OLYAN BUNKÓ ÉS AGGRESSZÍV VELEM... ÉS MÉG MINDIG NINCS ITT AZ UNOKÁM!!! HOL VAN AZ A BOSZI! Mindegy, nem is érdekel, csak kerüljön hozzám a kis édesem.", ref Actors.Gerle, "06/01_Gerle.mp3"),
               new Line("Gerle bekopog a terem ajtaján. Pár másodperccel később az ajtót egy szőke hajú, zöld szemű fiatal hölgy nyitja ki.", ref Actors.Narrator, "06/02_narrátor.mp3"),
               new Line("Üdv kedveském! Ugye te vagy a dadus?", ref Actors.Gerle, "06/03_Gerle.mp3"),
               new Line("Üdvözlöm! Igen én lennék. Kérem maradjunk a magazódásnál, köszönöm! Miben segíthetek?", ref Actors.Dajka_Laura_neni, "06/04_Laura.mp3"),
               new Line("AKKOR AZ ANYÁDAT, AZT!", ref Actors.Gerle, "06/05_Gerle.mp3"),
               new Line("Megkértem önt, hogy maradjunk a magázódásnál. Miben segíthetek?", ref Actors.Dajka_Laura_neni, "06/06_Laura.mp3"),
               new Line("Kéne nekem az unokám, nem tudja merre találom?", ref Actors.Gerle, "06/07_Gerle.mp3"),
               new Line("De, épp az akadémiai anyagot tanítja a G csoportban.", ref Actors.Dajka_Laura_neni, "06/08_Laura.mp3"),
               new Line("Akadémia?", ref Actors.Gerle, "06/09_Gerle.mp3"),
               new Line("Igen, mióta 2 éve kilépett a mátrixból folyamatosan a Hustlers’ Egyetemen van.", ref Actors.Dajka_Laura_neni, "06/10_Laura.mp3"),
               new Line("Merre találom őt?", ref Actors.Gerle, "06/11_Gerle.mp3"),
               new Line("Balra, de vigyázzon, nehogy beleütközzön a játékautójába.", ref Actors.Dajka_Laura_neni, "06/12_Laura.mp3"),
               new Line("Gerle elindul. Lassacskán elér a G csoporthoz. Az ajtón egy nagy G betű szerepel, és közvetlen mellette egy Bugatti formájú elektromos kis autó van, melyen a következő szöveg áll „És a tiéd milyen színű?”. Mikor Gerle benyit, egy meglepő jelenet fogadja: " +
                   "A szobában a faltól félkörívben babzsákok vannak, mindegyikben egy-egy öltönyös óvodás." +
                   " A kör közepén a falnál egy fehér fémtábla, olyan írásokkal, mint „Toddler’s University”, „nők = kiszolgálók”, „woke = cancer”. A tábla előtt egy kigyúrt, öltönyös, kopasz és szakállas óvodás áll. " +
                   "Gerlének egy kis ideig néznie kellett, de felismerte: Az unokája volt az.", ref Actors.Narrator, "06/14_narrátor.mp3"),
               new Line("Nem fogsz úgy becsajozni, hogy egy Lambod van. Tudod miért? Mert a csajok ezt leszarják. Az egyetlenek, akiket érdekel a Lamborghini, akik leállnak az utcán integetni neked, azok 10 éves taknyosok. " +
                   "Vegyél egy Lambot, ha pedofil vagy...", ref Actors.Unoka, "06/15_Unoka.mp3"),
               new Line("mi a...", ref Actors.Gerle, "06/16_Gerle.mp3"),
               new Line("Az ember csak akkor csal meg egy nőt, ha egy másikat szeret. Ha van egy nő, akit igazán szeretek, és elmegyek dugni, de visszajövök hozzá, és valójában csak ő érdekel, akkor ez nem megcsalás." +
                   " Ez testedzés!", ref Actors.Unoka, "06/17_Unoka.mp3"),
               new Line("Az unoka ránéz a Rolexnek álcázott karórájára, majd újból megszólal.", ref Actors.Narrator, "06/18_narrátor.mp3"),
               new Line("Fúúúh, urak! Hát nem is szóltok, hogy így elment az idő? Mára végeztem. Menjetek, emelgessetek még egy picit súlyt levezetésként, aztán lehet elkezdeni a Drop shipping gyakorlatot!", ref Actors.Unoka, "06/19_Unoka.mp3"),
               new Line("A többi teremben lévő óvodás feláll, és egyszerre ad megerősítést az unokának: „Igenis, Middle G!”, majd távozik. Gerle egyedül marad a teremben az unokával.", ref Actors.Narrator, "06/20_narrátor.mp3"),
               new Line("Na mi van, nyanya? Láttam, hogy már az ajtóban sasolsz egy ideje. Ez a Toddler’s University. Ilyeneknek, mint te, nincs ide bemenet.", ref Actors.Unoka, "06/21_Unoka.mp3"),
               new Line("Kisédesem... mi lett veled? Hát nem emlékszel rám? Én vagyok az, a nagyi.", ref Actors.Gerle, "06/22_Gerle.mp3"),
               new Line("Nekem nincs nagyanyám. Engem az utca nevelt.", ref Actors.Unoka, "06/23_Unoka.mp3"),
               new Line("Na jó fiatalúr, ebből elég! Nem tudom, mivel tömte ez a Laura-némber a fejedet, de most ennek véget vetek. Gyerünk, indulás!", ref Actors.Gerle, "06/24_Gerle.mp3"),
               new Line("Gerle odasiet az unokájához, rámarkol a bal karjára, és elkezdi vinni.", ref Actors.Narrator, "06/25_narrátor.mp3"),
               new Line("Héé má! Mi a franc?! Eressz már el! Hogy képzeled ezt?", ref Actors.Unoka, "06/26_Unoka.mp3"),
             }),

             new Scene(new Line[]{
                new Line("Hirtelen betoppan egy apuka. Egy picit zilált volt a frizurája, de egyébként normálisnak tűnt. Egészen addig amíg meg nem szólalt.", ref Actors.Narrator, "07/01_narrátor_A.mp3"),
                new Line("LAURA!! SANYI ITT VAN GYEREKÉRT!", ref Actors.Sanyi, "07/02_Sanyi.mp3"),
                new Line("Jajj, nemár, megint te jöttél érte?. Lali annyival jobb nevelője volt, viszont amióta kiment a németekhez, azóta te meg Csoki jön csak érte...", ref Actors.Dajka_Laura_neni, "07/03_Laura.mp3"),
                new Line("ÚÚÚÚ.... TUDSZ NEKEM MESÉLNI EGY MESÉT?? A KIRÁLYLÁNYOS MESÉT.", ref Actors.Sanyi, "07/04_Sanyi.mp3"),
                new Line("Ahhoz korábban kell bejönni, alvás idő elött.", ref Actors.Dajka_Laura_neni, "07/05_Laura.mp3"),
                new Line("ÉS AZ MIKOR VAN??", ref Actors.Sanyi, "07/06_Sanyi.mp3"),
                new Line("Olyan délután 2 környékén.", ref Actors.Dajka_Laura_neni, "07/07_Laura.mp3"),
                new Line("ÉS AZ MIKOR VAAN??", ref Actors.Sanyi, "07/08_Sanyi.mp3"),
                new Line("Dél után 2 órával.", ref Actors.Dajka_Laura_neni, "07/09_Laura.mp3"),
                new Line("RENDBEN, ADDIG ITT MARADOK.", ref Actors.Sanyi, "07/10_Sanyi.mp3"),
                new Line("De hisz neked saját otthonod van és ráadásul egy gyerekért jöttél.", ref Actors.Dajka_Laura_neni, "07/11_Laura.mp3"),
                new Line("NEM BAJ, Ő IS SZERETNÉ HALLANI BIZTOSAN.", ref Actors.Sanyi, "07/12_Sanyi.mp3"),
                new Line("De az óvoda be fog zárni.", ref Actors.Dajka_Laura_neni, "07/13_Laura.mp3"),
                new Line("JÓ AKKOR ITT ALSZUNK.", ref Actors.Sanyi, "07/14_Sanyi.mp3"),
                new Line("Amíg Laura és Sanyi tovább folytatták a beszélgetésüket, addig Gerlének sikerült elráncigálnia az unokáját. " +
                    "Szegény Andriska nagyon kivolt, még szívesen maradt volna. Azonban Gerle nem feledkezett meg róla, ezért hozott neki egy Snickers-t így máris jókedvre derült. " +
                    "Mikor sikerült Andrisnak lenyugodnia, és már tisztán látott, felismerte, hogy nagymamája csak jót akart neki. " +
                    "Ezután átöleli őt és azt mondja.", ref Actors.Narrator, "07/15_narrátor_A.mp3"),
                new Line("Jajj nagyika, köszönöm, hogy elhoztál. Annyira nehéz a többiek mellett az óvodában. Valamiért sosem értik meg, amit mondok nekik... " +
                    "Nem baj, most már elhoztál, így sikeresebb tudok lenni. Köszönöm!", ref Actors.Unoka, "07/16_Unoka.mp3"),
                new Line("Kincsem, ez nagyon jól esett. Bárcsak több időt tölthetnénk együtt, de muszáj, hogy visszavigyelek a szüleidnek.", ref Actors.Gerle, "07/17_Gerle.mp3"),
                new Line("Ígérem nagyi, ha végre összejön a biznisz, akkor majd elmegyünk Ibizára bulizni egyet.", ref Actors.Unoka, "07/18_Unoka.mp3"),
                new Line("Rendben édesem, én bízok benned.", ref Actors.Gerle, "07/19_Gerle.mp3"),
                new Line("Ezután Gerle és Andris boldogan és jókedvűen sétált hazafelé. Mikor eljött a búcsú ideje mind a ketten könnybe lábadt szemekkel köszöntek el egymástól. " +
                    "Gerle elindult haza, majd mikor visszaért a portánál megszólítja a portás.", ref Actors.Narrator, "07/20_narrátor_A.mp3"),
                new Line("Látom Gerle megint kalandos egy napod volt. Remélem azt tudod, hogy történtek miatt bajba kerülhetsz.", ref Actors.Portas, "07/21_portas_A.mp3"),
                new Line("Ugyan már... Ti fiatalok nem tudjátok milyen is az igazi szeretet. Most pedig állj arrébb aranyoskám, sietek a lakásomba. " +
                    "Már nagyon szeretnék aludni, nagyon kimerített ez nap.", ref Actors.Gerle, "07/22_Gerle.mp3"),
                new Line("Andris tervei sikeresek voltak és pár hónappal később már Gerlével együtt buliztak Ibizán a legjobb DJ-k zenéire. " +
                    "Andris hazajött, viszont Gerle kint maradt, ugyanis számára jobb a kinti élet, nincs annyi időjárás változás, így se a feje, se a végtagjai nem fájnak annyit.. " +
                    "Persze, Andriska rendszeresen látogatta őt. Ekkor mindig csaptak egy jó nagy partyt.", ref Actors.Narrator, "07/23_narrátor_A.mp3"),
             }, Actors.Unoka.Name ,SceneVersion.A),

             new Scene(new Line[]{
                new Line("Gerle nem jut messzire: Laurával, az óvónővel találja szemben magát.", ref Actors.Narrator, "07/01_narrátor_B.mp3"),
                new Line("Őt hagyd ki ebből!", ref Actors.Dajka_Laura_neni, "07/02_Laura.mp3"),
                new Line("Mindketten mélyen egymás szemébe néznek." +
                    " Middle G kiszabadul nagyanyja szorításából, és az óvónő mellé áll. Laura az övére rögzített plüssmacitokok mellé engedi a kezeit." +
                    " Gerle is hasonlóképp tesz, de ott neki édességek vannak. Mindkettőjük összeszűkíti a szemét, csak a másikra koncentrál.", ref Actors.Narrator, "07/03_narrátor_B.mp3"),
                new Line("Állj félre, és nem esik bántódásod.", ref Actors.Gerle, "07/04_Gerle.mp3"),
                new Line("Az nem fog menni.", ref Actors.Dajka_Laura_neni, "07/05_Laura.mp3"),
                new Line("Laura izzad. A teremben lévő egész óvodáscsoport elcsendesül. " +
                    "Egyedül a párbajozni készülők levegővétele hallatszik. " +
                    "A csöndet varjúkárogás töri meg. Mindketten tudják: Amint a madár elrepül, a harc elkezdődik. Néma csendben várnak a madár első szárnycsapására." +
                    "Gerle a cukorért nyúl, Laura a macikért. Dobás. " +
                    "Se a macik, se a cukrok nem találtak célba – a levegőben összeütköztek, és kettőjük között pont középen a padlón landoltak.", ref Actors.Narrator, "07/06_narrátor_B.mp3"),
             }, ref Actors.Dajka_Laura_neni, new SceneMusic("Western_Intro_120-bpm.mp3", "Western_kozep_120-bpm.mp3", "Western_outro_120-bpm.mp3", 120), null ,SceneVersion.B),

             new Scene(new Line[]{
                 new Line("Mikor az életereje az óvodásnak és Laurának is lent van, a harcot a mennyből alászálló messiás szakítja meg", ref Actors.Narrator, "08/01_narrátor_B.mp3"),
                 new Line("Gyermekeiiiiiiiiim! Fejezzétek be a harcot! Jöttem az Atya utasításából igazságot tenni. " +
                     "A Tétény András által képviselt eszmék nagy része káros az emberiségre. " +
                     "Mindenki egyenlő, és Isten minden bárányát egyiránt szereti.", ref Actors.Jezus, "08/02_Jézus_B.mp3"),
                 new Line("Jézus Middle G felé fordul", ref Actors.Narrator, "08/03_narrátor_B.mp3"),
                 new Line("András....", ref Actors.Jezus, "08/04_Jézus.mp3"),
                 new Line("Hirtelen egy rövidre vágott rózsaszín hajú, piercinges viszonylag testesebb anyuka lép be a beszélgetésbe hívatlanul, aki igazából az örökbe fogadott gyerekéért jött.", ref Actors.Narrator, "08/05_narrátor_B.mp3"),
                 new Line("Aha, szóval mindenki egyenlő, mi? Peeersze! Mert ezt tényleg igaz is! Az összes férfi egy utolsó senkiházi! " +
                     "Mindegyik egy „creep”! Mindegyik csak a hatalomra és a nők kihasználására vágyik! Slayyyy!", ref Actors.Fiminista, "08/06_Femináci.mp3"),
                 new Line("Gyermekem, ez nem igaz. Isten az embert a saját képére teremtette. Isten a nőt és a férfit egyenlőnek teremtette, mert mindkettő Isten képmása.", ref Actors.Jezus, "08/07_Jézus_B.mp3"),
                 new Line("Jaaaj hagyjál már! Ki vagy te? Jézus Krisztus....", ref Actors.Fiminista, "08/08_Femináci.mp3"),
                 new Line("Pontosan, Emőke. Jézus vagyok. Jézus Krisztus. Lorem ipsum dolor sit amet, consectetur adipiscing elit.", ref Actors.Jezus, "08/09_Jézus_B.mp3"),
                 new Line("Jézus ekkor felvett a mellettük lévő asztalról egy vízzel teli poharat, majd borrá változtatta.", ref Actors.Narrator, "08/10_narrátor_B.mp3"),
                 new Line("Ez itt az én vérem", ref Actors.Jezus, "08/11_Jézus_B.mp3"),
                 new Line("Nyújtá a nő felé Jézus a poharat", ref Actors.Narrator, "08/12_narrátor_B.mp3"),
                 new Line("Az utóbbi szavak elhangzására a femináci aktivistán egyértelműen látszódott, hogy átkapcsolt benne valami. Körülbelül fél percnek kellett eltennie, mire újból meg tudott szólalni.", ref Actors.Narrator, "08/13_narrátor_B.mp3"),
                 new Line("Ámen!", ref Actors.Fiminista, "08/14_Femináci.mp3"),
                 new Line("Mondá a nő, majd ijedve elrohant.", ref Actors.Narrator, "08/15_narrátor_B.mp3"),
                 new Line("Ezután a rövid reklámszünet után, Isten fia folytatta a monológját. Rátette a jobb kezét Middle G homlokára.", ref Actors.Narrator, "08/16_narrátor_B.mp3"),
                 new Line("András, neked adom a racionalitás ajándékát. Használd felelősséggel!", ref Actors.Jezus, "08/17_Jézus_B.mp3"),
                 new Line("mondta Jézus, majd nagy fényességgel eltűnt.", ref Actors.Narrator, "08/18_narrátor_B.mp3"),
                 new Line("mondta Jézus, majd nagy fényességgel eltűnt. Az unoka, aki látszólag felismerte Gerle méltóságát és bölcsességét, megkönnyebbülten öleli át őt.", ref Actors.Narrator, "08/19_narrátor_B.mp3"),
                 new Line("Nagyi, végre itt vagy! Milyen jó, hogy jöttél! Nem is kérdés, hogy te vagy az igazi hősünk!", ref Actors.Unoka, "08/20_Unoka.mp3"),
                 new Line("Gerle meghatódik az unoka szeretetétől és hálájától.", ref Actors.Narrator, "08/21_narrátor_B.mp3"),
                 new Line("Drágám, nem hagytalak volna egy percig sem veszélyben. Most pedig haza viszlek, ahol vár a családod és a biztonság.", ref Actors.Gerle, "08/22_Gerle.mp3"),
                 new Line("Az unoka boldogan mosolyog, és együtt hagyják el az óvodát kéz a kézben, kezükben egymáséval, ahol már várják őt a szülei." +
                     "A győzelem édes íze még hosszan megmarad Gerle számára, hiszen nemcsak az unokáját mentette meg, hanem rámutatott az őszinte " +
                     "szeretetre és a bölcsesség erejére is.", ref Actors.Narrator, "08/23_narrátor_B.mp3"),
             }, Actors.Jezus.Name , SceneVersion.B),
             new EndCreditScene(new EndCredit[]
             {
                 new EndCredit("Gerle Kalandjai", "Köszönjük, hogy végigjátszottad a játékunkat!"),
                 new EndCredit("Írta", "Balogh Levente, Gáspár Mihály, Kluitenberg Alex, Tatár Mátyás Bence"),
                 new EndCredit("Szereposztás", " "),
                 new EndCredit("Gerle", "Balogh Levente"),
                 new EndCredit("Aggresszív óvodás", "Tatár Domonkos"),
                 new EndCredit("Laura", "Tatár Mátyás Bence"),
                 new EndCredit("Jegyellenőr", "Balogh Levente"),
                 new EndCredit("Ápoló", "Tatár Mátyás Bence"),
                 new EndCredit("Portás", "Gáspár Mihály"),
                 new EndCredit("Galamb 1", "Simon Viktor"),
                 new EndCredit("Galamb 2", "Gáspár Mihály"),
                 new EndCredit("Unoka", "Tatár Mátyás Bence"),
                 new EndCredit("Olvasó", "Kluitenberg Alex"),
                 new EndCredit("Jézus", "Kluitenberg Alex"),
                 new EndCredit("Femináci", "Balogh Levente"),
                 new EndCredit("Random ","Gáspár Mihály"),
                 new EndCredit("Anyuka", "Kluitenberg Alex"),
                 new EndCredit("Sanyi", "Balogh Levente"),
                 new EndCredit("Fejlesztők", "Balogh Levente, Gáspár Mihály, Kluitenberg Alex, Tatár Mátyás Bence"),
                 new EndCredit("Zenei producer", "Kluitenberg Alex")
             }, "GERLE-final-track-levente-godmode_147-bpm", 130)
        };
    }
}