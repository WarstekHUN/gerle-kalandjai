using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerle_Lib.BaseClasses
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public ConsoleColor Color { get; set; }
        public List<MenuItem> SubMenu { get; set; }
        public Action Action { get; set; }

        public MenuItem(string text, string icon, ConsoleColor color, Action action = null, List<MenuItem> subMenu = null)
        {
            Text = text;
            Icon = icon;
            Color = color;
            Action = action;
            SubMenu = subMenu ?? new List<MenuItem>();
        }
    }

}
