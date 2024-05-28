using Gerle_Lib.BaseClasses;
using MenuSystem;
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

    static void TemplateScene()
    {
        //BeautyWriter.WriteLine("[bold yellow on blue]Meglévő folytatása![/] :globe_showing_europe_africa:");

        
        var grid = new Grid();
        grid.AddColumn(new GridColumn());   
        //grid.AddColumn(new GridColumn());   
        

        var BossHPItems = new List<BarChartItem>
        {
            new BarChartItem("Életerő", 100, SysColor.IndianRed),
            new BarChartItem("Mana", 100, SysColor.RebeccaPurple),
            new BarChartItem("Idő Támadásig", 100, SysColor.Green),
        };
        var BossHP = new Panel(Align.Center(ProgressBarMaker.CreateBarChart(BossHPItems, "")));
        BossHP.Border = BoxBorder.Rounded;
        BossHP.Header = new PanelHeader("[red3_1 bold underline]Ellenfél adatai[/]");
        //BossHP.Expand();

        var YourHPItems = new List<BarChartItem>
        {
            new BarChartItem("Életerő", 100, SysColor.IndianRed),
            new BarChartItem("Mana", 100, SysColor.RebeccaPurple),
            new BarChartItem("Idő Támadásig", 100, SysColor.Green),
        };
        //ProgressBarMaker.RenderBarChart(YourHP, "A te adataid:");
        var YourHP = new Panel (Align.Center(ProgressBarMaker.CreateBarChart(YourHPItems, "")));
        YourHP.Border = BoxBorder.Rounded;
        YourHP.Header = new PanelHeader($"[green bold underline]A te adataid[/]");
        //YourHP.Expand();

        grid.AddRow(BossHP);
        grid.AddEmptyRow();
        grid.AddRow(YourHP);
        //AnsiConsole.Write(grid);


        var layout = new Layout("Root")
    .SplitRows(
        new Layout("Top"),
        new Layout("Center"),
        new Layout("Bottom"));

        layout["Top"].Update(BossHP).Size(7);
        layout["center"].Update(YourHP).Size(7);

        AnsiConsole.Write(layout);

        Console.ReadKey();

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

    /// <summary>
    /// A hangbeállítások menü megjelenítése.
    /// </summary>
    static void SoundSettingsMenu()
    {
        Menu sm = new Menu(new string[] { "Zene hangereje🎵", "Szinkron hangereje 🗣️" }, new Action[] {
            () => BeautyWriter.Write("[bold yellow on blue]Zene hangereje![/] :globe_showing_europe_africa:"),
            () => BeautyWriter.Write("[bold yellow on blue]Szinkron hangereje![/] :globe_showing_europe_africa:")
        }, true, mainMenu);
    }

    /// <summary>
    /// Kilép a programból.
    /// </summary>
    static void Exit()
    {
        BeautyWriter.Write("[bold yellow on blue]Kilépés![/] :globe_showing_europe_africa:");
    }
}
