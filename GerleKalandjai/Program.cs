//using Gerle_Lib.BaseClasses;
//List<MenuItem> menuItems = new List<MenuItem>
//        {
//            new MenuItem("Játék", "📁", ConsoleColor.Green, () => Console.WriteLine("Játék"), new List<MenuItem> {
//                new MenuItem("Meglévő folytatása", "🆕", ConsoleColor.Cyan, () => Console.WriteLine("Meglévő folytatása")),
//                new MenuItem("Új játék", "📂", ConsoleColor.Magenta, () => Console.WriteLine("Új játék")),
//                new MenuItem("Kilépés", "🚪", ConsoleColor.Red, () => Environment.Exit(0))
//            }),
//            new MenuItem("Beállítások", "📝", ConsoleColor.Yellow, () => Console.WriteLine("Edit"), new List<MenuItem> {
//                new MenuItem("Nehézség", "✂️", ConsoleColor.Blue, () => Console.WriteLine("Cut")),
//                new MenuItem("Hang", "📋", ConsoleColor.Blue, () => Console.WriteLine("Hang")),
//                new MenuItem("Zene", "📥", ConsoleColor.Blue, () => Console.WriteLine("Zene"))
//            }),
//            new MenuItem("Példa", "👀", ConsoleColor.Blue, () => Console.WriteLine("View"), new List<MenuItem> {
//                new MenuItem("Pont", "+", ConsoleColor.Green, () => Console.WriteLine("Pont")),
//                new MenuItem("Pont", "-", ConsoleColor.Green,  () => Console.WriteLine("Pont")),
//                new MenuItem("Resszőcske", "🔄", ConsoleColor.Green, () => Console.WriteLine("Resszőcske"))
//            })
//        };

//Screen screen = new Screen(menuItems);
//screen.RunMenu();
using Gerle_Lib.BaseClasses;
using MenuSystem;
using Spectre.Console;

class Program
{
    private static Menu mainMenu;
    private static Action<string> WriteAction;

    private static BeautyWriter bw = new BeautyWriter();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        
        
        WriteAction = text => bw.Write(text);

        Menu.SetCreator("Tatár Mátyás Bence, Kluitenberg Alex, Gáspár Mihály, Balogh Levente");
        
        mainMenu = new Menu(new[] { "Játék 📁", "Beállítások 📝", "Kilépés 🚪" }, new Action[] {
            GameMenu,
            SettingsMenu,
            Exit
        });
    }

    static void GameMenu()
    {
        Menu sm = new Menu(new string[] { "Meglévő folytatása 🆕", "Új játék 📂" }, new Action[] {
            () => WriteAction("[bold yellow on blue]Meglévő folytatása![/] :globe_showing_europe_africa:"),
            NewGameMenu
        }, true, mainMenu);
    }

    static void NewGameMenu()
    {
        Menu sm = new Menu(new string[] { "Nehézség ✂️", "Játék neve🔤" }, new Action[] {
            () => WriteAction("[bold yellow on blue]Nehézség![/] :globe_showing_europe_africa:"),
            () => WriteAction("[bold yellow on blue]Játék neve![/] :globe_showing_europe_africa:")
        }, true, mainMenu);
    }

    static void SettingsMenu()
    {
        Menu sm = new Menu(new string[] { "Hang 📋" }, new Action[] { SoundSettingsMenu }, true, mainMenu);
    }

    static void SoundSettingsMenu()
    {
        Menu sm = new Menu(new string[] { "Zene hangereje🎵", "Szinkron hangereje 🗣️" }, new Action[] {
            () => WriteAction("[bold yellow on blue]Zene hangereje![/] :globe_showing_europe_africa:"),
            () => WriteAction("[bold yellow on blue]Szinkron hangereje![/] :globe_showing_europe_africa:")
        }, true, mainMenu);
    }

    static void Exit()
    {
        WriteAction("[bold yellow on blue]Kilépés![/] :globe_showing_europe_africa:");
    }
}
