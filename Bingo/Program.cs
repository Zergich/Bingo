using Bingo.Addons_UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using static Bingo.Lexer;
using static Bingo.Addons_UI.Color;
namespace Bingo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleWork cw = new ConsoleWork();

            //cw.CreatCFH();
            //if (args.Length != 0)
            //    cw.Distributeer(args[0]);// крмпилция
            //else cw.BaseInfo();

            Console.WriteLine("Главная проблема то что сплиты работают по концретным индексам а в коде принт может стоять на 3 позиции например и все перестает работаь");
            //string FileData = "string pepega = \"da\"";

            string FileData = File.ReadAllText(@"D:\Progects\Bingo\Bingo\1.bg");

            LexrTest.StartLexer(FileData); // сделать нумерацию без массивов в параметре str

            Console.ReadKey();
        }
    }
}

