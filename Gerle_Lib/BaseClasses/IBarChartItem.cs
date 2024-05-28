using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysColor = System.Drawing.Color;

namespace Gerle_Lib.BaseClasses
{
    public sealed class BarChartItem : IBarChartItem
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public Spectre.Console.Color? Color { get; set; }

        public BarChartItem(string label, double value, SysColor? color = null)
        {
            Label = label;
            Value = value;
            if (color.HasValue)
            {
                Color = new Spectre.Console.Color(color.Value.R, color.Value.G, color.Value.B);
            }
            else
            {
                Color = null;
            }
        }
    }
}
