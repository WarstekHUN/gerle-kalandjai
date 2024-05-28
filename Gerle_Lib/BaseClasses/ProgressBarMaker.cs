using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysColor = System.Drawing.Color;

namespace Gerle_Lib.BaseClasses
{
    public class ProgressBarMaker
    {
        public static void RenderBarChart(IEnumerable<BarChartItem> items, string chartLabel, int width = 500)
        {
            var barChart = new BarChart()
                .Width(width)
                .Label($"[green bold underline]{chartLabel}[/]")
                .CenterLabel();

            foreach (var item in items)
            {
                barChart.AddItem(item);
            }

            AnsiConsole.Write(barChart);
        }
        public static BarChart CreateBarChart(IEnumerable<BarChartItem> items, string chartLabel, int width = 500)
        {
            var barChart = new BarChart()
                .Width(width)
                .Label($"[green bold underline]{chartLabel}[/]")
                .CenterLabel();

            foreach (var item in items)
            {
                barChart.AddItem(item);
            }

            return (barChart);
        }
    }
}
