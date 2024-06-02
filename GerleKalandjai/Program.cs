using Gerle_Lib.BaseClasses;
using MenuSystem;
using NAudio.Wave;
using Spectre.Console;
using BarChartItem = Gerle_Lib.BaseClasses.BarChartItem;
using SysColor = System.Drawing.Color; // Alias System.Drawing.Color to avoid ambiguity


class Program
{
    private static Menu mainMenu = new Menu(new[] { "temp", "Játék 📁", "Beállítások 📝", "Kilépés 🚪" }, new Action[] {
                    TemplateScene,
                    GameMenu,
                    SettingsMenu,
                    Exit
                });

    /// <summary>
    /// A program belépési pontja.
    /// </summary>
    /// <param name="args">A parancssori argumentumok.</param>
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        string[] creators = { "Tatár Mátyás Bence", "Kluitenberg Alex", "Gáspár Mihály", "Balogh Levente" };
        string shuffledCreators = string.Join(", ", creators.OrderBy(x => Guid.NewGuid()));

        Menu.SetCreator(shuffledCreators);
        mainMenu.SetToScreen();
    }

    /// <summary>
    /// A játék menü megjelenítése.
    /// </summary>
    static void GameMenu()
    {
        Menu sm = new Menu(new string[] { "Meglévő folytatása 🆕", "Új játék 📂" }, new Action[] {
                    () => BeautyWriter.Write("[bold yellow on blue]Meglévő folytatása![/] :globe_showing_europe_africa:"),
                    NewGameMenu
                }, true, mainMenu);
    }

    /// <summary>
    /// A sablon jelenet megjelenítése.
    /// </summary>
    static void TemplateScene()
    {
        var grid = new Grid();
        grid.AddColumn(new GridColumn());

        var random = new Random();
        var BossHPItems = new List<BarChartItem>
                {
                    new BarChartItem("Életerő", 100, SysColor.IndianRed),
                    new BarChartItem("Mana", 100, SysColor.RebeccaPurple),
                    new BarChartItem("Idő Támadásig", 100, SysColor.Green),
                };
        var BossHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(BossHPItems, "")));
        BossHP.Border = BoxBorder.Rounded;
        BossHP.Header = new PanelHeader("[red3_1 bold underline]Ellenfél adatai[/]");

        var YourHPItems = new List<BarChartItem>
                {
                    new BarChartItem("Életerő", 100, SysColor.IndianRed),
                    new BarChartItem("Mana", 100, SysColor.RebeccaPurple),
                    new BarChartItem("Idő Támadásig", 100, SysColor.Green),
                };
        var YourHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(YourHPItems, "")));
        YourHP.Border = BoxBorder.Rounded;
        YourHP.Header = new PanelHeader($"[green bold underline]A te adataid[/]");

        grid.AddRow(BossHP);
        grid.AddEmptyRow();
        grid.AddRow(YourHP);

        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Top"),
                new Layout("Center"),
                new Layout("Bottom"));

        layout["Top"].Update(BossHP).Size(7);
        layout["Center"].Update(YourHP).Size(7);

        AnsiConsole.Write(layout);

        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            // Simulate random changes
            BossHPItems[0].Value = Math.Clamp(BossHPItems[0].Value + random.Next(-12, 12), 0, 100);
            BossHPItems[1].Value = Math.Clamp(BossHPItems[1].Value + random.Next(-12, 12), 0, 100);
            BossHPItems[2].Value = Math.Clamp(BossHPItems[2].Value + random.Next(-12, 12), 0, 100);

            YourHPItems[0].Value = Math.Clamp(YourHPItems[0].Value + random.Next(-12, 12), 0, 100);
            YourHPItems[1].Value = Math.Clamp(YourHPItems[1].Value + random.Next(-12, 12), 0, 100);
            YourHPItems[2].Value = Math.Clamp(YourHPItems[2].Value + random.Next(-12, 12), 0, 100);

            // Update the UI
            BossHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(BossHPItems, "")))
            {
                Border = BoxBorder.Rounded,
                Header = new PanelHeader("[red3_1 bold underline]Ellenfél adatai[/]")
            };
            YourHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(YourHPItems, "")))
            {
                Border = BoxBorder.Rounded,
                Header = new PanelHeader($"[green bold underline]A te adataid[/]")
            };

            layout["Top"].Update(BossHP);
            layout["Center"].Update(YourHP);

            AnsiConsole.Clear();
            AnsiConsole.Write(layout);

            Thread.Sleep(2_000); // Pause for a while to simulate time passing
        }

    }

    /// <summary>
    /// Az új játék menü megjelenítése.
    /// </summary>
    static void NewGameMenu()
    {
        Menu sm = new Menu(new string[] { "Nehézség ✂️", "Játék neve🔤" }, new Action[] {
                    () => BeautyWriter.Write("[bold yellow on blue]Nehézség![/] :globe_showing_europe_africa:"),
                    () => BeautyWriter.Write("[bold yellow on blue]Játék neve![/] :globe_showing_europe_africa:")
                }, true, mainMenu);
    }

    /// <summary>
    /// A beállítások menü megjelenítése.
    /// </summary>
    static void SettingsMenu()
    {
        Menu sm = new Menu(new string[] { "Hang 📋" }, new Action[] { SoundSettingsMenu }, true, mainMenu);
    }

    static int castVolume = 70;
    static int musicVolume = 50;
    /// <summary>
    /// A hangbeállítások menü megjelenítése.
    /// </summary>
    static void SoundSettingsMenu()
    {
        Menu sm = new Menu(new string[] { "Zene hangereje🎵", "Szinkron hangereje 🗣️" }, new Action[] {
                    () => OpenMusicVolumeMenu(),
                    () => OpenCastVolumeMenu()
                }, true, mainMenu);
    }

    
    static void OpenCastVolumeMenu()
    {
        //Menu sm = new Menu(new string[] { $"Jelenlegi hangerő: {musicVolume.ToString()}%", "Hangerő növelése 🔊", "Hangerő csökkentése 🔉", "Alkalmazás ✅" }, new Action[]
        //{
        //            () => ApplySoundVolumeMenu(),
        //            () => VolumeUp(),
        //            () => VolumeDown(),
        //            () => ApplySoundVolumeMenu(),
        //}, true, mainMenu);


        int mertek = AnsiConsole.Prompt(
        new TextPrompt<int>("Add meg [green]1 és 100[/] között a [bold yellow]szinkron[/] hangerejét! ")
            .PromptStyle("green")
            .ValidationErrorMessage("[red]Ez nem egy szám![/]")
            .Validate(age =>
            {
                return age switch
                {
                    <= 0 => ValidationResult.Error("[red]Az érték nem lehet 1-nél kisebb![/]"),
                    >= 101 => ValidationResult.Error("[red]Az érték nem lehet 100-nál kisebb![/]"),
                    _ => ValidationResult.Success(),
                };
            })
        );

        Console.Clear();


        var VolumeSet = new List<BarChartItem>
                {
                    new BarChartItem("[green]Zene hangereje[/]", musicVolume, SysColor.Green),
                    new BarChartItem("[yellow]Szinkron hangereje[/]", castVolume, SysColor.Green),
                    new BarChartItem("[pink3]Max[/]", 100, SysColor.Transparent),
                };
        var VolumeSetPan = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(VolumeSet, "")));
        VolumeSetPan.Border = BoxBorder.Rounded;
        VolumeSetPan.Header = new PanelHeader("[green]Beállítások[/]");

        castVolume = mertek;

        AnsiConsole.Write(VolumeSetPan);


        //BeautyWriter.WriteLine($"[green] A zene hangereje:[/] [bold deepskyblue1]{mertek}[/][gold3_1]%[/]");
    }


    /// <summary>
    /// A zene hangerejének menüje megjelenítése.
    /// </summary>
    static void OpenMusicVolumeMenu()
    {
        //Menu sm = new Menu(new string[] { $"Jelenlegi hangerő: {musicVolume.ToString()}%", "Hangerő növelése 🔊", "Hangerő csökkentése 🔉", "Alkalmazás ✅" }, new Action[]
        //{
        //            () => ApplySoundVolumeMenu(),
        //            () => VolumeUp(),
        //            () => VolumeDown(),
        //            () => ApplySoundVolumeMenu(),
        //}, true, mainMenu);


        int mertek = AnsiConsole.Prompt(
        new TextPrompt<int>("Add meg [green]1 és 100[/] között a [bold green3]zene[/] hangerejét! ")
            .PromptStyle("green")
            .ValidationErrorMessage("[red]Ez nem egy szám![/]")
            .Validate(age =>
            {
                return age switch
                {
                    <= 0 => ValidationResult.Error("[red]Az érték nem lehet 1-nél kisebb![/]"),
                    >= 101 => ValidationResult.Error("[red]Az érték nem lehet 100-nál kisebb![/]"),
                    _ => ValidationResult.Success(),
                };
            })
        );
        
        Console.Clear();


        var VolumeSet = new List<BarChartItem>
                {
                    new BarChartItem("[green]Zene hangereje[/]", mertek, SysColor.Green),
                    new BarChartItem("[green]Alapbeállítás[/]", musicVolume, SysColor.Transparent),
                };
        var VolumeSetPan = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(VolumeSet, "")));
        VolumeSetPan.Border = BoxBorder.Rounded;
        VolumeSetPan.Header = new PanelHeader("[green]Beállítások[/]");

        musicVolume = mertek;

        AnsiConsole.Write(VolumeSetPan);


        //BeautyWriter.WriteLine($"[green] A zene hangereje:[/] [bold deepskyblue1]{mertek}[/][gold3_1]%[/]");
    }

    /// <summary>
    /// A hangerejének beállításainak menüje megjelenítése.
    /// </summary>
    static void ApplySoundVolumeMenu()
    {
        Menu sm = new Menu(new string[] { $"Jóváhagyja a beállításokat? A hangerő: {musicVolume.ToString()}%", "Alkalmazás ✅", "Mégse ❌" }, new Action[]
        {
                    ApplySoundVolumeMenu,
                    () => BeautyWriter.Write("[bold green3_1]Jóváhagyva![/]"),
                    () => OpenMusicVolumeMenu()
        }, true, mainMenu);
    }

    /// <summary>
    /// Növeli a hangerőt.
    /// </summary>
    static void VolumeUp()
    {
        musicVolume++;
        OpenMusicVolumeMenu();
    }

    /// <summary>
    /// Csökkenti a hangerőt.
    /// </summary>
    static void VolumeDown()
    {
        musicVolume--;
        OpenMusicVolumeMenu();
    }

    /// <summary>
    /// Kilép a programból.
    /// </summary>
    static void Exit()
    {
        BeautyWriter.Write("[bold yellow on blue]Kilépés![/] :globe_showing_europe_africa:");
    }
}
