# UML vazlat

## Actor

A szereplők maguk

- Name: `public get; private set; string` A szerplő neve
- MaxHealth: `public const ushort` A szereplő maximális életereje
- MaxMana:`public const ushort` A szereplő maximális manaszintje
- Powers: `public get; protected set; Power[]` A szerplő képességei

## Power

- Name: `public get; init; string` a képesség neve
- Damage: `public get; init; ushort` a képesség sebzése
- Mana: `public get; init; ushort` a képesség mana-szükséglete
- IsDodgeAble: `public get; bool` Megadja, hogy a támadás dodgeolható-e
- Power(name: `string`, damage: `ushort`, mana: `ushort`)
- Minigame(): `public virtual void?` Elindítja a képesség elhárításához a minigamet

## FightingActor : Actor

- Health: `public get; private set; ushort`
- Mana: `publoc get; protected set; ushort`
- Opponent: `public get; init; ref:FightingActor` Az adott épp harocoló karakter ellensége
<!--  SetOpponent(`ref:FightingActor`): `void` Beállítja az adott éppen harcoló karakter ellenségét -->
- Think(): `public void` Az NPC-k nél releváns, ez dönti el, hogy milyen támadást használjon
- Attack(kepesseg): `public bool` Megsebzi az Opponent karaktert a képesség sebzésével, levonja a manat. Ha true, volt elég mana. Ha false, nem volt.
- DealDemage(ushort): `public void` Sebez a FightingActoron

## Scene

- Lines: `Line[]` A szereplők által elmondott sorok
- Opponent: `ref Actor` Amenyniben van opponent, van a scene végén fight
- PlayScene(): `void` Lejátsza az adott jelenetet
- Scene(lines: `Line[]`, isFight: `bool`)

## Line

- Text: `string` A sor szövege
- Talker: `ref:Actor` A sor beszélője - Ha nincs, akkor ez a narrátor
- VoiceFile: `string` A hangfelvétel fájlja
- NoiseFile: `string?` A háttérzaj fájlja
- PlayLine(): `void` Lejátsza az adott sort
- Line(text: `string`, talker: `ref Actor`, voiceFile: `string`, noiseFile: `string?`)

## SceneController: *static*

- Scenes: `private static Scene[]` A játék jeleneteinek tömbje
- CurrentCheckpoint(): `public get; private set; static uint` Megadja, hogy a játékos hanyadik jeleneten, és veleegyütt harcon jutott túl.
- InitFight(Actor opponent): `public static void` Elkezd egy harcot, megvan adva ki az ellenfél.
- PlayScenes(checkpoint: `uint`): `public static void` Elkezdi lejátszani a jeleneteket a checkpoint-tól.

## ProgressController: *static*

- LoadFromSaveFile(): `public static bool` Megpróbálja megkeresni és kiolvasni a mentésfájl tartalmát és betölteni. True, ha sikerült. False, ha nem.
- SaveToFile(): `public static void` Lementi a felhasználó AppData mappájába az állást

## SettingsController: *static*
- MasterVolume: `public static float` Minimum értéke: 0, Maximum értéke: 1.
- MusicVolume: `public static float` Minimum értéke: 0, Maximum értéke: 1.
- FXVolume: `public static float` Minimum értéke: 0, Maximum értéke: 1.
- DialogueVolume: `public static float` Minimum értéke: 0, Maximum értéke: 1.

## MenuItem
- Szöveg: `public string` A menüelem megjelenített szövege.
- Ikon: `public string` Az ikon a menüelem szövege mellett.
- Szín: `public ConsoleColor` A menüelem szövegének színe.
- Almenü: `public List<MenuItem>` A menüelem alá tartozó almenük listája.
- Művelet: `public Action` A menüelem kiválasztásakor végrehajtandó művelet.
+ MenuItem(szöveg: `string`, ikon: `string`, szín: `ConsoleColor`, művelet: `Action` = null, almenü: `List<`MenuItem`> = null`): `public` Konstruktor a menüelem inicializálásához szöveggel, ikonnal, színnel, opcionálisan művelettel és almenü listával.

## Screen
- menüElemek: `private List<MenuItem>` A képernyőn megjelenített menüelemek listája.
- főKiválasztottIndex: `private int` Az aktuálisan kiválasztott fő menüelem indexe.
- alMenüKiválasztottIndex: `private int` Az aktuálisan kiválasztott almenü elem indexe.
- alMenüAktív: `private bool` Jelző, amely megmutatja, hogy az almenü jelenleg aktív-e.
+ Screen(menüElemek: `List<`MenuItem`>`): `public` Konstruktor a képernyő inicializálásához menüelemek listájával.
+ MenütMegjelenít(): `public void` Metódus a menüelemek megjelenítésére a képernyőn, kezeli mind a fő, mind az almenü megjelenítését.
+ MenütFuttat(): `public void` Metódus a felhasználói bemenetek kezelésére és a menüelemek közötti navigálásra.
