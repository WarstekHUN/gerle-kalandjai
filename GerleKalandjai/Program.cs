// See https://aka.ms/new-console-template for more information
using Gerle_Lib.BaseClasses;
List<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem("Játék", "📁", ConsoleColor.Green, () => Console.WriteLine("Játék"), new List<MenuItem> {
                new MenuItem("Meglévő folytatása", "🆕", ConsoleColor.Cyan, () => Console.WriteLine("Meglévő folytatása")),
                new MenuItem("Új játék", "📂", ConsoleColor.Magenta, () => Console.WriteLine("Új játék")),
                new MenuItem("Kilépés", "🚪", ConsoleColor.Red, () => Environment.Exit(0))
            }),
            new MenuItem("Beállítások", "📝", ConsoleColor.Yellow, () => Console.WriteLine("Edit"), new List<MenuItem> {
                new MenuItem("Nehézség", "✂️", ConsoleColor.Blue, () => Console.WriteLine("Cut")),
                new MenuItem("Hang", "📋", ConsoleColor.Blue, () => Console.WriteLine("Hang")),
                new MenuItem("Zene", "📥", ConsoleColor.Blue, () => Console.WriteLine("Zene"))
            }),
            new MenuItem("Példa", "👀", ConsoleColor.Blue, () => Console.WriteLine("View"), new List<MenuItem> {
                new MenuItem("Pont", "+", ConsoleColor.Green, () => Console.WriteLine("Pont")),
                new MenuItem("Pont", "-", ConsoleColor.Green,  () => Console.WriteLine("Pont")),
                new MenuItem("Resszőcske", "🔄", ConsoleColor.Green, () => Console.WriteLine("Resszőcske"))
            })
        };

Screen screen = new Screen(menuItems);
screen.RunMenu();