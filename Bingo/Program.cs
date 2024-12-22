using Bingo.Addons_UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using static Bingo.Lexer;
using static Bingo.Addons_UI.Color;
using static System.Net.Mime.MediaTypeNames;
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

            //Console.WriteLine(IsValid2(FileData));

            if (!IsBalanced(FileData)) { err.IfNotFigSkobk($"{FileData[FileData.Length - 1]}", posline); Environment.Exit(1); }


            LexrTest.StartLexer(FileData); // сделать нумерацию без массивов в параметре str

            Console.ReadKey();
            //Console.ReadKey();
        }
        private static readonly Dictionary<char, char> Pairs = new Dictionary<char, char>()
        {
            {'{', '}'},
        };
        static int posline = 1;
        static ErrorLexer err = new ErrorLexer();

        private static bool IsBalanced(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            var stack = new Stack<char>();
            foreach (var symbol in text)
            {
                if (symbol == '\n') posline++;
                if (Pairs.ContainsKey(symbol))
                {
                    stack.Push(symbol);
                }
                else if (!Pairs.ContainsValue(symbol)) continue;
                else if (stack.Count == 0)
                {
                    //err.IfNotFigSkobk($"{text[text.Length-1]}", posline);
                    return false;
                }
                else if (Pairs[stack.Pop()] != symbol)
                {
                    //err.IfNotFigSkobk($"{text[text.Length - 1]}", posline);
                    return false;
                }
            }
            return stack.Count == 0;
        }
    }
}

