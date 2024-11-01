using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo.Addons_UI
{
    public static  class Color
    {
        public static string NL = Environment.NewLine;
        public static string NORMAL = Console.IsOutputRedirected ? "" : "\u001B[39m";
        public static string RED = Console.IsOutputRedirected ? "" : "\u001B[91m";
        public static string GREEN = Console.IsOutputRedirected ? "" : "\u001B[92m";
        public static string YELLOW = Console.IsOutputRedirected ? "" : "\u001B[93m";
        public static string BLUE = Console.IsOutputRedirected ? "" : "\u001B[94m";
        public static string MAGENTA = Console.IsOutputRedirected ? "" : "\u001B[95m";
        public static string CYAN = Console.IsOutputRedirected ? "" : "\u001B[96m";
        public static string GREY = Console.IsOutputRedirected ? "" : "\u001B[97m";
        public static string BOLD = Console.IsOutputRedirected ? "" : "\u001B[1m";
        public static string NOBOLD = Console.IsOutputRedirected ? "" : "\u001B[22m";
        public static string UNDERLINE = Console.IsOutputRedirected ? "" : "\u001B[4m";
        public static string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\u001B[24m";
        public static string REVERSE = Console.IsOutputRedirected ? "" : "\u001B[7m";
        public static string NOREVERSE = Console.IsOutputRedirected ? "" : "\u001B[27m";
    }
}
