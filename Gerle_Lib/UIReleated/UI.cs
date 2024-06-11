using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Spectre.Console;
using Spectre.Console.Rendering;
using Gerle_Lib.BaseClasses;
using Gerle_Lib.Controllers;
using MenuSystem;
using BarChartItem = Gerle_Lib.BaseClasses.BarChartItem;
using SysColor = System.Drawing.Color;
using Gerle_Lib.Data;
namespace Gerle_Lib.UIReleated
{
    public class UI
    {
        public static readonly string bgColorHex = "#0c0c0c";
        public static readonly SysColor bgColor = System.Drawing.ColorTranslator.FromHtml(bgColorHex);

        public static int MasterVolume { get; set; } = 100;
        public static int MusicVolume { get; set; } = 50;
        public static int CastVolume { get; set; } = 70;
        public static int SfxVolume { get; set; } = 100;

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

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        public static void InitializeUI()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.BackgroundColor = ConsoleColor.Gray;

            string[] creators = { "Tatár Mátyás Bence", "Kluitenberg Alex", "Gáspár Mihály", "Balogh Levente" };
            string shuffledCreators = string.Join(", ", creators.OrderBy(x => Guid.NewGuid()));

            Menu.SetCreator(shuffledCreators);

            IntPtr consoleWindowHandle = GetForegroundWindow();
            ShowWindow(consoleWindowHandle, 3);
            GetWindowRect(consoleWindowHandle, out Rect screenRect);
            int width = screenRect.Right - screenRect.Left;
            int height = screenRect.Bottom - screenRect.Top;
            MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);
            Thread.Sleep(120);
            mainMenu.SetToScreen();
        }

        #region LiveRefresher (metódus) - comment
        /// <summary>
        /// <c>LiveRefresher</c> metódus tesztadatokkal hívja meg a <c>CutsceneUI</c> metódust.
        /// </summary>
        public static void LiveRefresher()
        {
            while (true)
            {
                Console.Clear();

                var hero = new Actor("Hero");
                var villain = new Actor("Villain");
                var narrator = new Actor("Narrator");

                var lines = new Line[]
    {
        new Line("Once upon a time in a faraway land...", ref narrator, "narrator_intro.mp3"),
        new Line("I will defeat you, villain!", ref hero, "hero_taunt.mp3"),
        new Line("You cannot stop me, hero!", ref villain, "villain_reply.mp3"),
        new Line("The battle between good and evil has begun.", ref narrator, "narrator_battle.mp3")
    };

                CutsceneUI(lines);
                Console.ReadKey();

                //// Sample Powers
                //var actionPowers = new List<Power>
                //{
                //    new Power("Attack", 20, 10, true, "You dealt 20 damage."),
                //    new Power("Defend", 0, 5, false, "You defended."),
                //    new Power("Heal", 0, 15, false, "You healed 20 HP."),
                //    new Power("Special Move", 30, 20, true, "You dealt 30 damage."),
                //    new Power("Test", 10, 5, true, "You tested the action.")
                //};

                //// Current Mana and Health for testing purposes
                //ushort currentMana = 50;
                //ushort enemyHealth = 100;
                //ushort yourHealth = 100;

                //// Test the FightingUI function with canSelectPowers as true
                //bool canSelectPowers = true;
                //List<Power> selectedPowers = FightingUI(actionPowers, canSelectPowers, currentMana, enemyHealth, yourHealth, "Ellenség Neve");

                //// Display the selected powers
                //Console.Clear();
                //AnsiConsole.MarkupLine($"[bold green]Selected Powers:[/]");
                //foreach (var power in selectedPowers)
                //{
                //    AnsiConsole.MarkupLine($"- {power.Name} (Mana: {power.Mana}, Damage: {power.Damage})");
                //}
                //Console.ReadKey();

                //// Test the FightingUI function with canSelectPowers as false
                //canSelectPowers = false;
                //List<Power> enemyActions = FightingUI(actionPowers, canSelectPowers, currentMana, enemyHealth, yourHealth, "Ellenség 2 neve");

                //// Display the enemy actions
                //Console.Clear();
                //AnsiConsole.MarkupLine($"[bold red]Enemy Actions:[/]");
                //foreach (var power in enemyActions)
                //{
                //    AnsiConsole.MarkupLine($"- {power.Name} (Mana: {power.Mana}, Damage: {power.Damage})");
                //}
                //Console.ReadKey();


                // Test EndCreditUI
                var credits = new EndCredit[]
                {
            new EndCredit("Lead Developer", "Balogh Levente"),
            new EndCredit("Sound Engineer", "Kluitenberg Alex"),
            new EndCredit("Story Artist", "Gáspár Mihály"),
            new EndCredit("Game Designer", "Tatár Mátyás Bence")
                };

                EndCreditUI(credits);

                // Wait for user input before restarting the loop
                Console.WriteLine("Press any key to restart...");
                Console.ReadKey();
            }
        }

        #endregion


        #region Choice Screen
        public enum ChoiceScreenSelection
        {
            Restart,
            Exit
        }

        public static DeathScreenSelection ChoiceScreen(Choice choice)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(BeautyWriter.Spacer(6));
            var figletText = new FigletText("Válaszz!")
                .Centered()
                .Color(Color.Red);
            AnsiConsole.Write(figletText);

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[red]{choice.Text}[/]")
                    .AddChoices(new[] { "A", "B" })
                    .HighlightStyle(new Style(Color.Yellow)));

            return selection switch
            {
                "Újrakezdés (korábbi mentési pont betöltése)" => DeathScreenSelection.Restart,
                "Kilépés" => DeathScreenSelection.Exit,
                _ => DeathScreenSelection.Exit
            };
        }
        #endregion



        #region Death Screen
        public enum DeathScreenSelection
        {
            Restart,
            Exit
        }

        public static DeathScreenSelection DeathScreen(string customMessage)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(BeautyWriter.Spacer(6));
            var figletText = new FigletText(customMessage)
                .Centered()
                .Color(Color.Red);
            AnsiConsole.Write(figletText);

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[red]Meghaltál. Most mihez kezdesz?[/]")
                    .AddChoices(new[] { "Újrakezdés (korábbi mentési pont betöltése)", "Kilépés" })
                    .HighlightStyle(new Style(Color.Yellow)));

            return selection switch
            {
                "Újrakezdés (korábbi mentési pont betöltése)" => DeathScreenSelection.Restart,
                "Kilépés" => DeathScreenSelection.Exit,
                _ => DeathScreenSelection.Exit
            };
        }
        #endregion





        public static void GameMenu()
        {
            if (ProgressController.LoadFromSaveFile())
            {
                Menu sm = new Menu(new string[] { "Meglévő folytatása 🆕", "Új játék 📂" }, new Action[] {
                            () => SceneController.PlayScenes(),
                            NewGameMenu
                        }, true, mainMenu);
            }
            else
            {
                Menu sm = new Menu(new string[] { "Új játék 📂" }, new Action[] {
                            NewGameMenu
                        }, true, mainMenu);
            }
        }

        public static void NewGameMenu()
        {
            SceneController.CurrentCheckpoint = 0;
            SceneController.PlayScenes();
        }

        #region CutsceneUI (metódus) - comment
        /// <summary>
        /// <c>CutsceneUI</c> metódus a jelenet sorait jeleníti meg egymás alatt, középre igazítva.
        /// A sorok színe a beszélő (Actor) alapján változik.
        /// A sorok egyenként jelennek meg, gépelési effektussal.
        /// </summary>
        public static async Task<SceneVersion> CutsceneUI(Line[] lines)
        {
            AnsiConsole.Clear();

            foreach (var line in lines)
            {
                var grid = new Grid();
                grid.AddColumn(new GridColumn().Alignment(Justify.Center));
                
                Task voiceOver = Task.Run(() => line.PlayAudioFile(line.VoiceFile));

                if (line is ChoiceScreen choice)
                {
                    return choice.PresentChoiceToPlayer().SceneVersion;
                }

                var text = new Text(line.Text);
                text.Justify(Justify.Center);

                if (line.Talker != null)
                {
                    if (line.Talker == Actors.Narrator)
                    {
                        text = new Text(line.Talker.Name + ": " + line.Text, new Style(Color.Gold1, Color.Black));
                    }
                    else if (line.Talker == Actors.Gerle)
                    {
                        text = new Text(line.Talker.Name + ": " + line.Text, new Style(Color.Green, Color.Black));
                    }
                    else if (line.Talker == Actors.Apolo || line.Talker == Actors.Unoka)
                    {
                        text = new Text(line.Talker.Name + ": " + line.Text, new Style(Color.White, Color.Black));
                    }
                    else
                    {
                        text = new Text(line.Talker.Name + ": " + line.Text, new Style(Color.Red, Color.Black));
                    }
                }
                else
                {
                    text = new Text(line.Text, new Style(Color.White, Color.Black));
                }

                /*
                // Typing effect
                foreach (char c in line.Text)
                {
                    Console.Write(c);
                    Thread.Sleep(50); // Adjust the speed of the typing effect
                }

                Console.WriteLine();*/

                grid.AddRow(text.Centered());

                //AnsiConsole.Clear();

                while(!voiceOver.IsCompleted)
                {
                    AnsiConsole.Write(grid);
                    Thread.Sleep(1200);
                    AnsiConsole.Clear();
                }

                await voiceOver;

                Thread.Sleep(2000); // Wait for 2 seconds before showing the next line

            }

            return SceneVersion.BASE;
        }
        #endregion


        //#region CutsceneUI (metódus) - comment
        ///// <summary>
        ///// <c>CutsceneUI</c> metódus a jelenet sorait jeleníti meg egymás alatt, középre igazítva.
        ///// A sorok színe a beszélő (Actor) alapján változik.
        ///// A sorok egyenként jelennek meg, gépelési effektussal.
        ///// </summary>
        //public static void CutsceneUI(List<Line> lines)
        //{
        //    var layout = new Layout("Root")
        //        .SplitColumns(
        //            new Layout("Left"),
        //            new Layout("Center")
        //                .SplitRows(
        //                    new Layout("Top"),
        //                    new Layout("Middle"),
        //                    new Layout("Bottom")),
        //            new Layout("Right"));

        //    layout["Left"].Size(1);
        //    layout["Right"].Size(1);
        //    layout["Top"].Size(1);
        //    layout["Bottom"].Size(1);

        //    var grid = new Grid();
        //    grid.AddColumn(new GridColumn());

        //    foreach (var line in lines)
        //    {
        //        var text = new Text(line.Text);

        //        if (line.Talker != null)
        //        {
        //            switch (line.Talker.Name)
        //            {
        //                case "Narrator":
        //                    text = new Text(line.Text, new Style(Color.Gold1, Color.Black));
        //                    break;
        //                case "Hero":
        //                    text = new Text(line.Text, new Style(Color.Green, Color.Black));
        //                    break;
        //                case "Villain":
        //                    text = new Text(line.Text, new Style(Color.Red, Color.Black));
        //                    break;
        //                default:
        //                    text = new Text(line.Text, new Style(Color.White, Color.Black));
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            text = new Text(line.Text, new Style(Color.White, Color.Black));
        //        }

        //        // Typing effect
        //        var typedText = "";
        //        foreach (char c in line.Text)
        //        {
        //            typedText += c;
        //            var typingText = new Text(typedText);

        //            var tempGrid = new Grid();
        //            tempGrid.AddColumn(new GridColumn());

        //            foreach (var row in grid.Rows)
        //            {
        //                tempGrid.AddRow(row.ToArray());
        //            }

        //            tempGrid.AddRow(typingText.Centered());

        //            layout["Middle"].Update(new Panel(tempGrid).Expand());

        //            AnsiConsole.Clear();
        //            try
        //            {
        //                AnsiConsole.Write(layout);
        //            }
        //            catch
        //            {
        //                AnsiConsole.Write(" ");
        //            }

        //            Thread.Sleep(50); // Adjust the speed of the typing effect
        //        }

        //        grid.AddRow(text.Centered());
        //        Thread.Sleep(2000); // Wait for 2 seconds before showing the next line
        //    }
        //}
        //#endregion


        #region Beállítások
        public static void SettingsMenu()
        {
            Menu sm = new Menu(new string[] { "Hang 📋" }, new Action[] { SoundSettingsMenu }, true, mainMenu);
        }

        public static void SoundSettingsMenu()
        {
            Menu sm = new Menu(new string[] { "Fő hangerő🔊", "Zene hangereje🎵", "Szinkron hangereje🗣️", "Effektek hangereje🪄" }, new Action[] {
                        () => OpenMasterVolumeMenu(),
                        () => OpenMusicVolumeMenu(),
                        () => OpenCastVolumeMenu(),
                        () => OpenSFXVolumeMenu(),
                    }, true, mainMenu);
        }

        public static void OpenMasterVolumeMenu()
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
            MasterVolume = mertek;
            PrintVolume();
        }

        public static void OpenMusicVolumeMenu()
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
            MusicVolume = mertek;
            PrintVolume();
        }

        public static void OpenCastVolumeMenu()
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
            CastVolume = mertek;
            PrintVolume();
        }

        public static void OpenSFXVolumeMenu()
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
            SfxVolume = mertek;
            PrintVolume();
        }

        public static void PrintVolume()
        {
            Console.Clear();

            var VolumeSet = new List<BarChartItem>
                    {
                        new BarChartItem("[green]Zene hangereje[/]", MusicVolume, SysColor.LightGreen),
                        new BarChartItem("[yellow]Szinkron hangereje[/]", CastVolume, SysColor.Yellow),
                        new BarChartItem("[blue]SFX hangereje[/]", SfxVolume, SysColor.LightBlue),
                        new BarChartItem("[pink3]Master hangereje[/]", MasterVolume, SysColor.LightPink),
                        new BarChartItem("[gray][/]", 100, bgColor),
                    };
            var VolumeSetPan = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(VolumeSet, "")));
            VolumeSetPan.Border = BoxBorder.Rounded;
            VolumeSetPan.Header = new PanelHeader("[green]Beállítások[/]");
            AnsiConsole.Write(VolumeSetPan);
        }
        #endregion



        #region TemplateFightScene
        public static void TemplateFightScene(
    string UsedPowerName = "Sakármit Döjcs", string DemageText = "centerText", string otherText = "otherText",
    string enemyName = "enemyName", ushort enemyHealth = 50,
    ushort yourHealth = 100, ushort yourMana = 100, ushort selectedPowerManaCost = 10
)
        {
            var grid = new Grid();
            grid.AddColumn(new GridColumn());

            SysColor col100 = BeautyWriter.FromColor(ConsoleColor.Gray);

            var BossHPItems = new List<BarChartItem>
    {
        new BarChartItem("", enemyHealth, SysColor.IndianRed),
        new BarChartItem("", 100, bgColor),
    };
            var BossHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(BossHPItems, "")));
            BossHP.Border = BoxBorder.None;
            BossHP.Header = new PanelHeader($"[red3_1 bold underline]{enemyName}[/]").Justify(Justify.Center);
            BossHP.Expand = false;
            BossHP.Padding = new Padding(1, 1, 1, 1);

            var YourHPBar = new List<BarChartItem>
    {
        new BarChartItem("", yourHealth, SysColor.IndianRed),
        new BarChartItem("", 100, bgColor),
    };
            var YourHP = new Panel(
                Align.Center(
                    ProgressBarMaker.CreateBarChart(YourHPBar, "")
                )
            );
            YourHP.Border = BoxBorder.None;
            YourHP.Header = new PanelHeader(($"[indianred1]Gerle élete[/]")).Justify(Justify.Center);

            var YourManBar = new BreakdownChart()
                .AddItem("Megmaradt mana", yourMana, Color.Purple)
                .AddItem("Felhasznált Mana", selectedPowerManaCost, Color.Purple3);

            var YourMana = new Panel(YourManBar);
            YourMana.Border = BoxBorder.None;
            YourMana.Header = new PanelHeader(($"[purple_2]Gerle manája[/]")).Justify(Justify.Center);

            var yourGrid = new Grid();
            yourGrid.AddColumn(new GridColumn());
            yourGrid.AddColumn(new GridColumn());
            yourGrid.AddRow(BeautyWriter.Spacer(1), BeautyWriter.Spacer(1));
            yourGrid.AddRow(YourHP, YourMana);

            var rows = new List<dynamic>()
    {
        BossHP,
        BeautyWriter.Spacer(1),
        new Rule("[yellow]Használt erő[/]"),
        BeautyWriter.Spacer(1),
        new Text(UsedPowerName, new Style(Color.Red, Color.Black)).Centered(),
        new Text(DemageText, new Style(Color.Green, Color.Black)).Centered(),
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


        #endregion




        public static void TemplateScene(
            bool doIneedCards = true,
            string UsedPowerText = "Sakármit Döjcs", string centerText = "centerText", string otherText = "otherText", string enemyName = "enemyName")
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



        //#region Select Action Cards

        //public static List<string> SelectActionCards(

        //    string[] actionCards

        //    )
        //{

        //    var selectedIndexes = new HashSet<int>();
        //    int currentIndex = 0;

        //    while (true)
        //    {
        //        Console.Clear();
        //        TemplateFightScene();

        //        var actionGrid = new Grid();
        //        foreach (var _ in actionCards)
        //        {
        //            actionGrid.AddColumn(new GridColumn().Width(AnsiConsole.Console.Profile.Width / actionCards.Length));
        //        }

        //        var row = new List<IRenderable>();
        //        for (int i = 0; i < actionCards.Length; i++)
        //        {
        //            var card = new Panel(actionCards[i])
        //            {
        //                Border = BoxBorder.Rounded,
        //                Padding = new Padding(1, 1, 1, 1)
        //            };

        //            if (selectedIndexes.Contains(i) && i == currentIndex)
        //            {
        //                card.Border = BoxBorder.Double;
        //                card.Header = new PanelHeader("[bold yellow]X[/]").Justify(Justify.Center);
        //            }
        //            else if (selectedIndexes.Contains(i))
        //            {
        //                card.Border = BoxBorder.Double;
        //                card.Header = new PanelHeader("[bold red]X[/]").Justify(Justify.Center);
        //            }
        //            else if (i == currentIndex)
        //            {
        //                card.Border = BoxBorder.Rounded;
        //                card.Header = new PanelHeader("[bold yellow]X[/]").Justify(Justify.Center);
        //            }

        //            row.Add(card);
        //        }

        //        actionGrid.AddRow(row.ToArray());
        //        AnsiConsole.Write(actionGrid);

        //        var key = Console.ReadKey(true).Key;
        //        if (key == ConsoleKey.LeftArrow)
        //        {
        //            currentIndex = (currentIndex == 0) ? actionCards.Length - 1 : currentIndex - 1;
        //        }
        //        else if (key == ConsoleKey.RightArrow)
        //        {
        //            currentIndex = (currentIndex == actionCards.Length - 1) ? 0 : currentIndex + 1;
        //        }
        //        else if (key == ConsoleKey.Enter)
        //        {
        //            if (selectedIndexes.Contains(currentIndex))
        //            {
        //                selectedIndexes.Remove(currentIndex);
        //            }
        //            else
        //            {
        //                selectedIndexes.Add(currentIndex);
        //            }
        //        }
        //        else if (key == ConsoleKey.Spacebar)
        //        {
        //            List<string> actions = selectedIndexes.Select(index => actionCards[index]).ToList();
        //            SelectedActionsPrint(actions);
        //            return actions;
        //            break;
        //        }
        //    }
        //}

        #region SelectActionCards (function)

        public static List<Power> SelectActionCards(Power[] actionPowers, ref ushort currentMana, ref ushort enemyHealth, ref ushort yourHealth, string enemyName)
        {
            var selectedIndexes = new HashSet<int>();
            int currentIndex = 0;
            var initialMana = currentMana;
            var initialEnemyHealth = enemyHealth;

            while (true)
            {
                Console.Clear();
                var selectedPowerNames = selectedIndexes.Select(i => actionPowers[i].Name).ToList();
                var selectedPowerMana = selectedIndexes.Select(i => actionPowers[i].Mana).Sum(m => (int)m);
                var selectedPowerDamage = selectedIndexes.Select(i => actionPowers[i].Damage).Sum(d => (int)d);

                // Calculate the remaining mana after the selected powers
                var remainingMana = (int)initialMana - selectedPowerMana;
                remainingMana = Math.Max(remainingMana, 0); // Ensure remaining mana is not negative
                var updatedEnemyHealth = (int)initialEnemyHealth - selectedPowerDamage;

                // Re-render the fight scene with updated values
                TemplateFightScene(
                    enemyName: enemyName,
                    enemyHealth: (ushort)updatedEnemyHealth,
                    yourHealth: yourHealth,
                    yourMana: (ushort)remainingMana,
                    selectedPowerManaCost: (ushort)selectedPowerMana
                );

                var actionGrid = new Grid();
                foreach (var _ in actionPowers)
                {
                    actionGrid.AddColumn(new GridColumn().Width(AnsiConsole.Console.Profile.Width / actionPowers.Length));
                }

                var row = new List<IRenderable>();
                for (int i = 0; i < actionPowers.Length; i++)
                {
                    var card = new Panel($"{actionPowers[i].Name}\nMana: {actionPowers[i].Mana}")
                    {
                        Border = BoxBorder.Rounded,
                        Padding = new Padding(1, 1, 1, 1)
                    };

                    if (i == currentIndex && actionPowers[i].Mana > remainingMana && !selectedIndexes.Contains(i))
                    {
                        card.Border = BoxBorder.Double;
                        card.BorderColor(Color.OrangeRed1);
                        card.Header = new PanelHeader("[bold orangered1]❌[/]").Justify(Justify.Center);
                    }
                    else if (actionPowers[i].Mana > remainingMana && !selectedIndexes.Contains(i))
                    {
                        card.BorderColor(Color.Red);
                        card.Header = new PanelHeader("[bold red]❌[/]").Justify(Justify.Center);
                    }
                    else if (selectedIndexes.Contains(i) && i == currentIndex)
                    {
                        card.Border = BoxBorder.Double;
                        card.BorderColor(Color.Green3_1);
                        card.Header = new PanelHeader("[bold yellow]X[/]").Justify(Justify.Center);
                    }
                    else if (selectedIndexes.Contains(i))
                    {
                        card.BorderColor(Color.Green3_1);
                        card.Header = new PanelHeader("[bold green3_1]✅[/]").Justify(Justify.Center);
                    }
                    else if (i == currentIndex)
                    {
                        card.Border = BoxBorder.Double;
                        card.BorderColor(Color.Yellow);
                        card.Header = new PanelHeader("[bold yellow]X[/]").Justify(Justify.Center);
                    }

                    row.Add(card);
                }

                actionGrid.AddRow(row.ToArray());
                AnsiConsole.Write(actionGrid);

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow)
                {
                    currentIndex = (currentIndex == 0) ? actionPowers.Length - 1 : currentIndex - 1;
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    currentIndex = (currentIndex == actionPowers.Length - 1) ? 0 : currentIndex + 1;
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    if (selectedIndexes.Contains(currentIndex))
                    {
                        selectedIndexes.Remove(currentIndex);
                        currentMana = (ushort)Math.Min(initialMana, currentMana + actionPowers[currentIndex].Mana);
                        enemyHealth = (ushort)Math.Min(initialEnemyHealth, enemyHealth + actionPowers[currentIndex].Damage);
                    }
                    else if (actionPowers[currentIndex].Mana <= remainingMana)
                    {
                        selectedIndexes.Add(currentIndex);
                        currentMana -= actionPowers[currentIndex].Mana;
                        enemyHealth = (ushort)Math.Max(0, enemyHealth - actionPowers[currentIndex].Damage);
                    }
                }
                else if (key == ConsoleKey.Enter)
                {
                    List<Power> selectedPowers = selectedIndexes.Select(index => actionPowers[index]).ToList();
                    return selectedPowers;
                }
            }
        }

        #endregion
        #region FightingUI (function)

        public static List<Power> FightingUI(Power[] inputPowers, bool canSelectPowers, ushort currentMana, ushort enemyHealth, ushort yourHealth, string enemyName)
        {
            ushort dummyCurrentMana = currentMana;
            ushort dummyEnemyHealth = enemyHealth;
            ushort dummyYourHealth = yourHealth;

            if (canSelectPowers)
            {
                var selectedPowers = SelectActionCards(inputPowers, ref dummyCurrentMana, ref dummyEnemyHealth, ref dummyYourHealth, enemyName);

                // Play the selected powers using dummy2 references
                ushort replayCurrentMana = currentMana;
                ushort replayEnemyHealth = enemyHealth;
                ushort replayYourHealth = yourHealth;

                foreach (var power in selectedPowers)
                {
                    Console.Clear();
                    replayCurrentMana -= power.Mana;
                    replayEnemyHealth = (ushort)Math.Max(0, replayEnemyHealth - power.Damage);

                    // Simulate the action on the UI
                    TemplateFightScene(
                        UsedPowerName: power.Name,
                        DemageText: power.DamageText,
                        enemyName: enemyName,
                        enemyHealth: replayEnemyHealth,
                        yourHealth: replayYourHealth,
                        yourMana: replayCurrentMana,
                        selectedPowerManaCost: power.Mana
                    );
                    Thread.Sleep(1000);
                }

                // Update original references after replay
                currentMana = replayCurrentMana;
                enemyHealth = replayEnemyHealth;
                yourHealth = replayYourHealth;

                return selectedPowers;
            }
            else
            {
                ushort replayYourHealth = yourHealth;

                foreach (var power in inputPowers)
                {
                    Console.Clear();

                    // Simulate the enemy action on the UI
                    TemplateFightScene(
                        UsedPowerName: power.Name,
                        DemageText: power.DamageText,
                        enemyName: enemyName,
                        enemyHealth: enemyHealth,
                        yourHealth: replayYourHealth,
                        yourMana: currentMana,
                        selectedPowerManaCost: power.Mana
                    );
                    Thread.Sleep(1000);

                    // Update player's health based on enemy attack
                    replayYourHealth = (ushort)Math.Max(0, replayYourHealth - power.Damage);
                }

                // Update original health reference after enemy attack replay
                yourHealth = replayYourHealth;

                return new List<Power>();
            }
        }

        #endregion



        public static void SelectedActionsPrint(List<string> actions)
        {
            AnsiConsole.MarkupLine($"[bold green]Actions triggered: {string.Join(", ", actions)}[/]");
            Console.ReadKey();
        }

        #region EndCreditUI (metódus) - comment
        /// <summary>
        /// <c>EndCreditUI</c> metódus a játék végén megjeleníti a stáblistát.
        /// A nevek egymás alatt, középre igazítva jelennek meg.
        /// </summary>
        public static void EndCreditUI(EndCredit[] credits)
        {
            var grid = new Grid();
            grid.AddColumn(new GridColumn());

            foreach (var credit in credits)
            {
                var roleText = new Text(credit.Role, new Style(Color.Yellow, Color.Black)).Centered();
                var nameText = new Text(credit.Name, new Style(Color.White, Color.Black)).Centered();

                grid.AddRow(roleText);
                grid.AddRow(nameText);
                grid.AddRow(new Text("").Centered()); // Add an empty row for spacing

                AnsiConsole.Clear();
                AnsiConsole.Write(grid);

                Thread.Sleep(2000); // Wait for 2 seconds before showing the next credit
            }
        }
        #endregion


        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
