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

        public static void LiveRefresher()
        {
            while (true)
            {
                Console.Clear();
                //TemplateScene();
                string[] actionCards = {
                "Attack", "Defend", "Heal", "Special Move", "Test"
            };
                List<string> SelectedCards = SelectActionCards(actionCards);
                SelectedActionsPrint(SelectedCards);

                //DeathScreenSelection result = DeathScreen("JÁTÉK VÉGE");
                //if (result == DeathScreenSelection.Restart)
                //{
                //    Console.WriteLine("Restarting game...");
                //}
                //else if (result == DeathScreenSelection.Exit)
                //{
                //    Console.WriteLine("Exiting game...");
                //}
            }
        }



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
                .AddItem("Megmaradt mana", yourMana - selectedPowerManaCost, Color.Purple)
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

        
        
        #region Select Action Cards

        public static List<string> SelectActionCards(
            
            string[] actionCards
            
            )
        {

            var selectedIndexes = new HashSet<int>();
            int currentIndex = 0;

            while (true)
            {
                Console.Clear();
                TemplateFightScene();

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

                    if (selectedIndexes.Contains(i) && i == currentIndex)
                    {
                        card.Border = BoxBorder.Double;
                        card.Header = new PanelHeader("[bold yellow]X[/]").Justify(Justify.Center);
                    }
                    else if (selectedIndexes.Contains(i))
                    {
                        card.Border = BoxBorder.Double;
                        card.Header = new PanelHeader("[bold red]X[/]").Justify(Justify.Center);
                    }
                    else if (i == currentIndex)
                    {
                        card.Border = BoxBorder.Rounded;
                        card.Header = new PanelHeader("[bold yellow]X[/]").Justify(Justify.Center);
                    }

                    row.Add(card);
                }

                actionGrid.AddRow(row.ToArray());
                AnsiConsole.Write(actionGrid);

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow)
                {
                    currentIndex = (currentIndex == 0) ? actionCards.Length - 1 : currentIndex - 1;
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    currentIndex = (currentIndex == actionCards.Length - 1) ? 0 : currentIndex + 1;
                }
                else if (key == ConsoleKey.Enter)
                {
                    if (selectedIndexes.Contains(currentIndex))
                    {
                        selectedIndexes.Remove(currentIndex);
                    }
                    else
                    {
                        selectedIndexes.Add(currentIndex);
                    }
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    List<string> actions = selectedIndexes.Select(index => actionCards[index]).ToList();
                    SelectedActionsPrint(actions);
                    return actions;
                    break;
                }
            }
        }

        public static void SelectedActionsPrint(List<string> actions)
        {
            AnsiConsole.MarkupLine($"[bold green]Actions triggered: {string.Join(", ", actions)}[/]");
            Console.ReadKey();
        }

        #endregion


        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
