using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerle_Lib.BaseClasses
{

    public class Screen
    {
        private List<MenuItem> menuItems;
        private int mainSelectedIndex = 0;
        private int subSelectedIndex = 0;
        private bool isSubMenuActive = false;

        public Screen(List<MenuItem> menuItems)
        {
            this.menuItems = menuItems;
        }

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu (horizontal):");
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == mainSelectedIndex)
                {
                    Console.ForegroundColor = menuItems[i].Color;
                    Console.Write($" > {menuItems[i].Icon} {menuItems[i].Text} < ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"   {menuItems[i].Icon} {menuItems[i].Text}   ");
                }
            }

            if (isSubMenuActive && menuItems[mainSelectedIndex].SubMenu.Count > 0)
            {
                Console.WriteLine("\n\nVertical Sub Menu:");
                for (int i = 0; i < menuItems[mainSelectedIndex].SubMenu.Count; i++)
                {
                    if (i == subSelectedIndex)
                        Console.WriteLine($"> {menuItems[mainSelectedIndex].SubMenu[i].Text} <");
                    else
                        Console.WriteLine($"  {menuItems[mainSelectedIndex].SubMenu[i].Text}  ");
                }
            }
        }

        public void RunMenu()
        {
            ConsoleKey key;
            do
            {
                DisplayMenu();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                key = keyInfo.Key;

                if (isSubMenuActive)
                {
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            subSelectedIndex = Math.Max(0, subSelectedIndex - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            subSelectedIndex = Math.Min(menuItems[mainSelectedIndex].SubMenu.Count - 1, subSelectedIndex + 1);
                            break;
                        case ConsoleKey.Enter:
                            menuItems[mainSelectedIndex].SubMenu[subSelectedIndex].Action?.Invoke();
                            isSubMenuActive = false;
                            break;
                        case ConsoleKey.Escape:
                            isSubMenuActive = false;
                            break;
                    }
                }
                else
                {
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            mainSelectedIndex = Math.Max(0, mainSelectedIndex - 1);
                            break;
                        case ConsoleKey.RightArrow:
                            mainSelectedIndex = Math.Min(menuItems.Count - 1, mainSelectedIndex + 1);
                            break;
                        case ConsoleKey.Enter:
                            if (menuItems[mainSelectedIndex].SubMenu.Count > 0)
                            {
                                isSubMenuActive = true;
                                subSelectedIndex = 0; // Reset to top of submenu
                            }
                            else
                            {
                                menuItems[mainSelectedIndex].Action?.Invoke();
                            }
                            break;
                    }
                }
            } while (key != ConsoleKey.Escape || isSubMenuActive);

            Console.WriteLine("\nMenu closed.");
        }
    }
}
