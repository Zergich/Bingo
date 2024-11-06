using System;
using System.Linq;
using static Bingo.Addons_UI.Color;

namespace Bingo
{
    public class ErrorLexer // Ретурны в конце это плохо
    {
        public void VrongTipeVariable(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:");

            Console.Write($"{RED}{UNDERLINE}{str.Split(' ')[0]}{NOUNDERLINE}{NORMAL}");
            foreach(string word in str.Split(' ').Skip(1)) { Console.Write(" " + word); }
            Console.WriteLine();

            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.WriteLine("^");
            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.Write("└─");
            Console.WriteLine("─ Не распознан тип или имя переменной.");
            Console.WriteLine($"\n{RED}Error: VrongTipeVariable{NORMAL}\n");
            //if (str.Split(' ')[3].Length > 1) Console.WriteLine($"{RED}Тип двнных \"char\" может хранитьь только 1 символ!{NORMAL}");

            return;
        }
        public void VrongDataTipeVariable(string str, int pos)
        {
            string[] SplitVar = str.Split(' ');

            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");

            int GetDataIntoSplit = 0;
            for (int i = 0; i < SplitVar.Length; i++)
            {
                GetDataIntoSplit = i;
                if (SplitVar[i] == "=") break;
                Console.Write(SplitVar[i] + " ");
            }
            Console.Write($"= {RED}{UNDERLINE}{SplitVar[GetDataIntoSplit+1]}{NOUNDERLINE}{NORMAL}");
            Console.WriteLine();

            int movearrow = 0;
            int last = 0;
            for (int i = 0; i < SplitVar.Length; i++)
            {
                movearrow += SplitVar[i].Length;
                last = SplitVar[i].Length;
            }

            Console.SetCursorPosition(last + movearrow /2, Console.CursorTop++);
            Console.WriteLine("^");
            Console.SetCursorPosition(last + movearrow / 2, Console.CursorTop++);
            Console.Write("└─");

            if (SplitVar[0] == "char")
                Console.WriteLine($"─ CHAR - Длина строки должна составлять один знак");
            else
                Console.WriteLine($"─ Не верный тип данных переменной.\n{GREEN}Доступные типы данных: \"string\", \"int\", \"byte\", \"char\", \"float\", \"bool\", \"var\".\n{RED}Error: VronDataType{NORMAL}\n");
            return;

        }

        public void VrongNameComand(string str, int pos)
        {
            string[] command = str.Split(new string[] { "(", ")" }, StringSplitOptions.None);

            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:");
            Console.Write($"{RED}{UNDERLINE}{command[0]}{NOUNDERLINE}{NORMAL}(");
            foreach(var i in command.Skip(1)) Console.Write(i);
            Console.WriteLine(")");


            Console.SetCursorPosition(command[0].Length / 2, Console.CursorTop++);
            Console.WriteLine("^");
            Console.SetCursorPosition(command[0].Length / 2, Console.CursorTop++);
            Console.Write("└─");
            Console.WriteLine("─ Не распознано ключевое слово.");

            Console.WriteLine($"\n{RED}Error: VrongNameComand{NORMAL}\n");
            return;

        }

        public void VrongGramarComand(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:");
            Console.WriteLine($"{MAGENTA}{UNDERLINE}{str}{NOUNDERLINE}{NORMAL} ─ Неверная граматика ключевого слова.");


            Console.WriteLine($"\n{RED}Error: VrongGramarComand{NORMAL}\n");
            return;
        }
        public void VrongliteralComand(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:");
            Console.WriteLine($"{MAGENTA}{UNDERLINE}{str}{NOUNDERLINE}{NORMAL} ─ Не распознанный или не экранированный литерал {BLUE}\"\\\"{NORMAL}");


            Console.WriteLine($"\n{RED}Error: VrongLiteralComand{NORMAL}\n");
            return;

        }

        public void Thesemicolonismissing(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");
            
            var strchar = str.ToCharArray();
            for(int i = 0; i < str.Length; i ++)
            {
                if (strchar.Length - 1 == i)
                {
                    Console.WriteLine($"{RED}{UNDERLINE}{strchar[i]}{NOUNDERLINE}{NORMAL}");
                    break;
                }
                Console.Write(strchar[i]);
            }
            Console.SetCursorPosition(strchar.Length, Console.CursorTop++);
            Console.WriteLine("^");
            Console.SetCursorPosition(strchar.Length, Console.CursorTop++);
            Console.Write("└─");
            Console.WriteLine($"─ Отсутвует символ окончания строки - {BLUE}\";\"{NORMAL}");


            Console.WriteLine($"\n{RED}Error: TheSemicolonIsMissing{NORMAL}\n");
            return;

        }

        public void IfNotKavuchki(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");
            Console.WriteLine($"{RED}{UNDERLINE}{str.Split(' ')[0]}{NOUNDERLINE}{NORMAL}");


            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.WriteLine("^");
            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.Write("└─");
            Console.WriteLine("─ Выражение не в кавычках.");

            Console.WriteLine($"\n{RED}Error: IfNotKavuchki{NORMAL}\n");
            return;


        }

        public void DontWount(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");

            var strchar = str.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0)
                {
                    Console.Write($"{RED}{UNDERLINE}{strchar[i]}{NOUNDERLINE}{NORMAL}");
                    continue;
                }

                Console.Write(strchar[i]);
            }
            Console.WriteLine();
            Console.SetCursorPosition(0, Console.CursorTop++);
            Console.WriteLine("^");
            Console.SetCursorPosition(0, Console.CursorTop++);
            Console.Write("└─");
            Console.WriteLine($"─ Ожидался другой символ{NORMAL}");

            Console.WriteLine($"\n{RED}Error: DontWount{NORMAL}\n");
            return;

        }

        //если выражение не в фигурных скобках
        public void IfNotFigSkobk(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");
            Console.WriteLine($"{RED}{UNDERLINE}{str.Split(' ')[0]}{NOUNDERLINE}{NORMAL}");


            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.WriteLine("^");
            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.Write("└─");
            Console.WriteLine("─ Отсутствуют фигурные скобки лиюо стоит впритык.");

            Console.WriteLine($"\n{RED}Error: IfNotFigSkobk{NORMAL}\n");
            return;

        }
        public void IfNotINT(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");
            Console.WriteLine($"{RED}{UNDERLINE}{str.Split(' ')[0]}{NOUNDERLINE}{NORMAL}");


            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.WriteLine("^");
            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.Write("└─");
            Console.WriteLine($"─ В цикле разрешен только тип данных {GREEN}int{NORMAL}.");

            Console.WriteLine($"\n{RED}Error: IfNotINT{NORMAL}\n");
            return;

        }
        public void WrongINTValueInLoop(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");
            Console.WriteLine($"{RED}{UNDERLINE}{str.Split(' ')[0]}{NOUNDERLINE}{NORMAL}");


            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.WriteLine("^");
            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.Write("└─");
            Console.WriteLine($"─ Не удалось преобразовать значение в условие цикла к типу {GREEN}int{NORMAL}.");

            Console.WriteLine($"\n{RED}Error: WrongINTValueInLoop{NORMAL}\n");
            return;

        }
        public void WrongParametrs(string str, int pos)//!!!!
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");
            Console.WriteLine($"{RED}{UNDERLINE}{str.Split(' ')[0]}{NOUNDERLINE}{NORMAL}");


            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.WriteLine("^");
            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.Write("└─");
            Console.WriteLine($"─ Не распознанно условие ключевого слова.");

            Console.WriteLine($"\n{RED}Error: WrongParametrs{NORMAL}\n");
            return;

        }
        public void WrongBoolOperator(string str, int pos)//!!!!
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");
            Console.WriteLine($"{RED}{UNDERLINE}{str.Split(' ')[0]}{NOUNDERLINE}{NORMAL}");

            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.WriteLine("^");
            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.Write("└─");
            Console.WriteLine($"─ Не распознанный булевой оператор.");

            Console.WriteLine($"\n{RED}Error: WrongBoolOperator{NORMAL}\n");
            return;

        }
        public void WrongIncremebtOperator(string str, int pos)
        {
            Console.WriteLine($"{YELLOW}На {pos} строке обнаружена ошибка:{NORMAL}");
            Console.WriteLine($"{RED}{UNDERLINE}{str.Split(' ')[0]}{NOUNDERLINE}{NORMAL}");

            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.WriteLine("^");
            Console.CursorLeft = str.Split(' ')[0].Length / 2;
            Console.Write("└─");
            Console.WriteLine($"─ Ошибка обозначения инкремента.");

            Console.WriteLine($"\n{RED}Error: WrongIncremebtOperator{NORMAL}\n");
            return;

        }
    }
}
