﻿using Gerle_Lib.BaseClasses;
using MenuSystem;
using Spectre.Console;
using Spectre.Console.Rendering;
using System.Runtime.InteropServices;
using BarChartItem = Gerle_Lib.BaseClasses.BarChartItem;
using SysColor = System.Drawing.Color; // Alias System.Drawing.Color to avoid ambiguity

class Program
{
    public static readonly string bgColorHex = "#0c0c0c";
    public static readonly SysColor bgColor = System.Drawing.ColorTranslator.FromHtml(bgColorHex);



    private static Menu mainMenu = new Menu(new[] { "temp", "Játék 📁", "Beállítások 📝", "Kilépés 🚪" }, new Action[] {
                    () => LiveRefresher(),
                    GameMenu,
                    SettingsMenu,
                    Exit
                });

    struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    // Import the necessary functions from user32.dll
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

    /// <summary>
    /// A program belépési pontja.
    /// </summary>
    /// <param name="args">A parancssori argumentumok.</param>
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.BackgroundColor = ConsoleColor.Gray;

        string[] creators = { "Tatár Mátyás Bence", "Kluitenberg Alex", "Gáspár Mihály", "Balogh Levente" };
        string shuffledCreators = string.Join(", ", creators.OrderBy(x => Guid.NewGuid()));


        Menu.SetCreator(shuffledCreators);

        //Hippity-Hoppity, your code is now my property! - Dani.

        // Get the handle of the console window
        IntPtr consoleWindowHandle = GetForegroundWindow();
        // Maximize the console window
        ShowWindow(consoleWindowHandle, 3);
        // Get the screen size
        GetWindowRect(consoleWindowHandle, out Rect screenRect);
        // Resize and reposition the console window to fill the screen
        int width = screenRect.Right - screenRect.Left;
        int height = screenRect.Bottom - screenRect.Top;
        MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);
        //Console.ReadKey();

        Thread.Sleep(120);
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

    static void LiveRefresher()
    {
        while (true)
        {
            Console.Clear();
            TemplateScene();

            DeathScreenSelection result = Program.DeathScreen("JÁTÉK VÉGE");
            if (result == Program.DeathScreenSelection.Restart)
            {
                Console.WriteLine("Restarting game...");
            }
            else if (result == Program.DeathScreenSelection.Exit)
            {
                Console.WriteLine("Exiting game...");
            }

            //System.Threading.Thread.Sleep(100000);
        }
    }

    public enum DeathScreenSelection
    {
        Restart,
        Exit
    }

    public static DeathScreenSelection DeathScreen(string customMessage)
    {
        AnsiConsole.Clear();

        // Display custom message in large text
        var figletText = new FigletText(customMessage)
            .Centered()
            .Color(Color.Red);
        AnsiConsole.Write(figletText);

        // Create a selection menu
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Meghaltál. Most mihez kezdesz?[/]")
                .AddChoices(new[] { "Újrakezdés (korábbi mentési pont betöltése)", "Kilépés" })
                .HighlightStyle(new Style(Color.Yellow)));

        // Return the selected option
        return selection switch
        {
            "Újrakezdés (korábbi mentési pont betöltése)" => DeathScreenSelection.Restart,
            "Kilépés" => DeathScreenSelection.Exit,
            _ => DeathScreenSelection.Exit
        };
    }

    /// <summary>
    /// A sablon jelenet megjelenítése, sorok és spacerek.
    /// </summary>
    static void TemplateScene(string UsedPowerText = "Sakármit Döjcs", string centerText = "centerText", string otherText = "otherText", string UsedBy= "UsedBy", string enemyName="enemyName")
    {
        
        var grid = new Grid();
        grid.AddColumn(new GridColumn());

        SysColor col100 = BeautyWriter.FromColor(ConsoleColor.Gray);

        var BossHPItems = new List<BarChartItem>
                {
                    new BarChartItem("", 50, SysColor.IndianRed),
                    new BarChartItem("", 100, bgColor),
                };
        var BossHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(BossHPItems, "")));
        BossHP.Border = BoxBorder.None;
        BossHP.Header = new PanelHeader($"[red3_1 bold underline]{enemyName}[/]").Justify(Justify.Center);
        BossHP.Expand = false;
        BossHP.Padding = new Padding(1, 1, 1, 1);



        var YourHPBar = new List<BarChartItem>
                {
                    new BarChartItem("", 50, SysColor.IndianRed),
                    new BarChartItem("", 100, bgColor),
                };        
        var YourHP = new Panel(
            Align.Center(
                ProgressBarMaker.CreateBarChart(YourHPBar, "")
                )
            );
        YourHP.Border = BoxBorder.None;
        //YourHP.Header = new PanelHeader(($"[green bold underline]A te adataid[/]")).Justify(Justify.Center);
        YourHP.Header = new PanelHeader(($"[indianred1]Gerle élete[/]")).Justify(Justify.Center);

        var YourManBar = new List<BarChartItem>
                {
                    new BarChartItem("", 100, SysColor.RebeccaPurple),
                    new BarChartItem("", 100, bgColor),
                };

        var YourMana = new Panel(
            Align.Center(
                               ProgressBarMaker.CreateBarChart(YourManBar, "")
                                              )
                       );
        YourMana.Border = BoxBorder.None;
        //YourMana.Header = new PanelHeader(($"[green bold underline]A te adataid[/]")).Justify(Justify.Center);
        YourMana.Header = new PanelHeader(($"[purple_2]Gerle manája[/]")).Justify(Justify.Center);

        var yourGrid = new Grid();
        yourGrid.AddColumn(new GridColumn());
        yourGrid.AddColumn(new GridColumn());
        yourGrid.AddRow(BeautyWriter.Spacer(1), BeautyWriter.Spacer(1));
        yourGrid.AddRow(YourHP, YourMana);


        var rows = new List<dynamic>() {
            BossHP,
            BeautyWriter.Spacer(1),
            new Rule("[yellow]Használt erő[/]"),
            BeautyWriter.Spacer(1),
            new Text(UsedPowerText, new Style(Color.Red, Color.Black)).Centered(),
            new Text(centerText, new Style(Color.Green, Color.Black)).Centered(),
            new Text(otherText, new Style(Color.Blue, Color.Black)).Centered(),
            BeautyWriter.Spacer(2),
            new Rule($""),
            yourGrid,
            BeautyWriter.Spacer(2),
            new Rule("[yellow]Akciók[/]"),
            BeautyWriter.Spacer(1),

        };

        foreach (var item in rows)
        {
            grid.AddRow(item);
        }


        AnsiConsole.Write(grid);

        
    }

    /// <summary>
    /// Megjeleníti az akciókártyákat egy vízszintes menüben, amely lehetővé teszi a felhasználó számára, hogy balra és jobbra nyilakkal válasszon, majd az Enter billentyűvel aktiválja az akciót.
    /// </summary>
    static void DisplayActionCards()
    {
        string[] actionCards = { 
            "Attack", "Defend", "Heal", "Special Move", 
            "test",
        };
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            TemplateScene();

            var actionGrid = new Grid();
            foreach (var _ in actionCards)
            {
                actionGrid.AddColumn(new GridColumn().Width(AnsiConsole.Console.Profile.Width / actionCards.Length));
            }

            var row = new List<IRenderable>();

            for (int i = 0; i < actionCards.Length; i++)
            {
                var card = new Panel(actionCards[i])
                {
                    Border = BoxBorder.Rounded,
                    Padding = new Padding(1, 1, 1, 1)
                };

                if (i == selectedIndex)
                {
                    card.Border = BoxBorder.Double;
                    card.Header = new PanelHeader("[bold yellow]Selected[/]").Justify(Justify.Center);
                    
                }

                row.Add(card);
            }

            actionGrid.AddRow(row.ToArray());

            AnsiConsole.Write(actionGrid);

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.LeftArrow)
            {
                selectedIndex = (selectedIndex == 0) ? actionCards.Length - 1 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                selectedIndex = (selectedIndex == actionCards.Length - 1) ? 0 : selectedIndex + 1;
            }
            else if (key == ConsoleKey.Enter)
            {
                TriggerAction(actionCards[selectedIndex]);
                break;
            }
        }
    }

    /// <summary>
    /// Aktiválja a kiválasztott akciót és megjeleníti a konzolon.
    /// </summary>
    /// <param name="action">Az aktiválandó akció neve.</param>
    static void TriggerAction(string action)
    {
        AnsiConsole.MarkupLine($"[bold green]Action triggered: {action}[/]");
        Console.ReadKey();
    }

    /// <summary>
    /// OUTDATED
    /// A sablon jelenet megjelenítése.
    /// </summary>
    static void TemplateScene2()
    {
        var grid = new Grid();
        grid.AddColumn(new GridColumn());

        var random = new Random();
        var BossHPItems = new List<BarChartItem>
                {
                    new BarChartItem("Életerő", 50, SysColor.IndianRed),
                    //new BarChartItem("", 100, SysColor.Teal),
                };
        var BossHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(BossHPItems, "")));
        BossHP.Border = BoxBorder.None;
        BossHP.Header = new PanelHeader("[red3_1 bold underline]Ellenfél adatai[/]").Justify(Justify.Center);

        var YourHPItems = new List<BarChartItem>
                {
                    new BarChartItem("Életerő", 50, SysColor.IndianRed),
                    new BarChartItem("Mana", 100, SysColor.RebeccaPurple),
                };
        var YourHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(YourHPItems, "")));
        YourHP.Border = BoxBorder.Double;
        YourHP.Header = new PanelHeader(($"[green bold underline]A te adataid[/]")).Justify(Justify.Center);
        YourHP.Width = 100;

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

            YourHPItems[0].Value = Math.Clamp(YourHPItems[0].Value + random.Next(-12, 12), 0, 100);
            YourHPItems[1].Value = Math.Clamp(YourHPItems[1].Value + random.Next(-12, 12), 0, 100);

            // Update the UI
            BossHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(BossHPItems, "")))
            {
                Border = BoxBorder.None,
                Header = new PanelHeader("[red3_1 bold underline]Ellenfél adatai[/]").Justify(Justify.Center),
                Width = 100
            };
            YourHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(YourHPItems, "")))
            {
                Border = BoxBorder.None,
                Header = new PanelHeader($"[green bold underline]A te adataid[/]").Justify(Justify.Center),
                Width = 100
            };

            layout["Top"].Update(BossHP);
            layout["Center"].Update(YourHP);

            AnsiConsole.Clear();
            AnsiConsole.Write(layout);

            Thread.Sleep(1_000); // Pause for a while to simulate time passing
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

    static int masterVolume = 100;
    static int musicVolume = 50;
    static int castVolume = 70;
    static int sfxVolume = 100;
    /// <summary>
    /// A hangbeállítások menü megjelenítése.
    /// </summary>
    static void SoundSettingsMenu()
    {
        Menu sm = new Menu(new string[] { "Fő hangerő🔊", "Zene hangereje🎵", "Szinkron hangereje🗣️", "Effektek hangereje🪄" }, new Action[] {
                    () => OpenMasterVolumeMenu(),
                    () => OpenMusicVolumeMenu(),
                    () => OpenCastVolumeMenu(),
                    () => OpenSFXVolumeMenu(),
                }, true, mainMenu);
    }

    
    static void OpenMasterVolumeMenu()
    {
        int mertek = AnsiConsole.Prompt(
        new TextPrompt<int>("Add meg [green]1 és 100[/] között a [bold yellow]játék[/] hangerejét! ")
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
        masterVolume = mertek;
        PrintVolume();
    }


    /// <summary>
    /// A zene hangerejének menüje megjelenítése.
    /// </summary>
    static void OpenMusicVolumeMenu()
    {
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
        musicVolume = mertek;
        PrintVolume();
    }


    static void OpenCastVolumeMenu()
    {
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
        castVolume = mertek;
        PrintVolume();
    }
    
    static void OpenSFXVolumeMenu()
    {
        int mertek = AnsiConsole.Prompt(
        new TextPrompt<int>("Add meg [green]1 és 100[/] között az [bold yellow]effektek[/] hangerejét! ")
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
        sfxVolume = mertek;
        PrintVolume();
    }


    static void PrintVolume()
    {
        Console.Clear();

        //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");

        var VolumeSet = new List<BarChartItem>
                {
                    new BarChartItem("[green]Zene hangereje[/]", musicVolume, SysColor.LightGreen),
                    new BarChartItem("[yellow]Szinkron hangereje[/]", castVolume, SysColor.Yellow),
                    new BarChartItem("[blue]SFX hangereje[/]", sfxVolume, SysColor.LightBlue),
                    new BarChartItem("[pink3]Master hangereje[/]", masterVolume, SysColor.LightPink),
                    new BarChartItem("[gray]Max[/]", 100, bgColor),
                };
        var VolumeSetPan = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(VolumeSet, "")));
        VolumeSetPan.Border = BoxBorder.Rounded;
        VolumeSetPan.Header = new PanelHeader("[green]Beállítások[/]");
        AnsiConsole.Write(VolumeSetPan);
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
