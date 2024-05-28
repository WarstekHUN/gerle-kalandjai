using Spectre.Console;
using System.Drawing; // For Color
using SysColor = System.Drawing.Color;

namespace Gerle_Lib.BaseClasses
{
    public static class BeautyWriter
    {
        /// <summary>
        /// Szépen írja ki a belüldött szöveget.
        /// </summary>
        public static void Write(string text) => AnsiConsole.Write(new Markup(text));
        public static void WriteLine(string text) => AnsiConsole.Write(new Markup(text+"\r\n"));

        public static void WriteChart(List<BarChartItem> items, string title="")
        {
            ProgressBarMaker.RenderBarChart(items, title);
        }
    }
}
