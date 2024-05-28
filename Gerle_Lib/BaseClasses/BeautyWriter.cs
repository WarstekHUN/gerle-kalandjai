using Spectre.Console;
namespace Gerle_Lib.BaseClasses
{
    public static class BeautyWriter
    {
        /// <summary>
        /// Szépen írja ki a belüldött szöveget.
        /// </summary>
        public static void Write(string text) => AnsiConsole.Write(new Markup(text));
    }
}
