////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.IO;
////using Bingo.Addons_UI;

////using static Bingo.Addons_UI.Color;
////using static Bingo.Token;
////using System.Text.RegularExpressions;

////namespace Bingo
////{
////    internal class Lexer
////    {

////        public static void ScanLexem(string Code)
////        {
////            List<Token> token = new List<Token>();
////            int Line = 1;
////            bool Error = false;
////            int index = 0;

////            string IntSymbols = "";
////            Console.Title = $"Каретка на {Line} строке";
////            foreach (char symbol in Code)
////            {
////                switch (symbol)
////                {
////                    case '\n':
////                        Line++;
////                        Console.Title = $"Каретка на {Line} строке";
////                        continue;

////                    case '0':
////                    case '1':
////                    case '2':
////                    case '3':
////                    case '4':
////                    case '5':
////                    case '6':
////                    case '7':
////                    case '8':
////                    case '9':
////                    case ' ':
////                        if (symbol == ' ' && IntSymbols != "" && IntSymbols != " ")
////                        {
////                            token.Add(new Token("Int", $"{IntSymbols}", index, Line));
////                            IntSymbols = "";
////                        }
////                        else IntSymbols += symbol;
////                        break;
////                    case '\t':
////                    case '\r':
////                    case '\f':
////                        continue;

////                    case '+': token.Add(new Token("Literal", "+", index, Line)); break;
////                    case '-': token.Add(new Token("Literal", "-", index, Line)); break;
////                    case '*': token.Add(new Token("Literal", "*", index, Line)); break;
////                    case '/': token.Add(new Token("Literal", "/", index, Line)); break;

////                    default:
////                        Console.WriteLine($"{RED}Лексическая ошибка! Не удалось распознать выражение на строке {GREY}{Line}{RED}. Не распознанное значение \"{YELLOW}{symbol}{RED}\"{NORMAL}"); Error = true; break;
////                }
////                index++;

////            }
////            if (Error) { Console.ReadKey(); return; } // зачем так а не просто ретурн в кетче а для того чтобы сразу все ошибки выводились

////            foreach (var item in token)
////            {
////                Console.WriteLine($"Type: {item.Type} Value: {item.Value} Pos: {item.Position} Line: {item.Line}");
////            }
////            Console.ReadKey();


////            //try
////            //{
////            //    result += $"{int.Parse($"{symbol}")}";
////            //}
////            //catch { Console.WriteLine($"{RED}Лексическая ошибка! Не удалось распознать выражение на строке {GREY}{Line}{RED}. Не распознанное значение \"{YELLOW}{symbol}{RED}\"{NORMAL}"); Error = true; }
////        }


////    }
////}


//////void Read(string Code)
//////{
//////    Regex _isNumber = new Regex(@"\d+");
//////    MatchCollection matches = _isNumber.Matches(Code);

//////    if (matches.Count > 0)
//////    {
//////        foreach (Match match in matches)
//////            Console.WriteLine(match.Value);
//////    }
//////    else Console.WriteLine($"{Color.RED}Не найдено ни одного регулярного выражения{Color.NORMAL}");
//////}
/////































////------------------------------------------------------------------------------------------
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
////using static Bingo.Lexer;
//using static Bingo.Addons_UI.Color;

//namespace Bingo
//{
//    public class LexrTest
//    {
//        static ErrorLexer err = new ErrorLexer();
//        static List<Token> token = new List<Token>();


//        public static void StartLexer(string str, int PosLine)
//        {
//            //if (str.ToCharArray()[str.Length - 1] != ';') err.Thesemicolonismissing(str, PosLine);

//            FindeVariables(str, PosLine);
//            Command(str, PosLine);
//        }
//        static void FindeVariables(string str, int PosLine)
//        {
//            bool HaveError = false;

//            Regex regex = new Regex("\\w*\\s[a-zA-Z_]+[a-zA-Z_0-9_]*\\s[=]\\s");
//            MatchCollection matches = regex.Matches(str);
//            if (matches.Count > 0)
//            {
//                foreach (Match match in matches)
//                {
//                    switch (match.Value.Split(' ')[0])
//                    {
//                        case "int": GetDataTypeVariable(str, PosLine, "int"); break;
//                        case "string": GetDataTypeVariable(str, PosLine, "string"); break;
//                        case "char": GetDataTypeVariable(str, PosLine, "char"); break;
//                        case "byte": GetDataTypeVariable(str, PosLine, "byte"); break;
//                        case "bool": GetDataTypeVariable(str, PosLine, "bool"); break;
//                        case "float": GetDataTypeVariable(str, PosLine, "float"); break;
//                        case "var": GetDataTypeVariable(str, PosLine, "var"); break;

//                        default: err.VrongTipeVariable(str, PosLine); HaveError = true; continue;
//                    }
//                    //if (match.Value.Split(' ')[0] != "float") { err.VrongTypeVariable(str, PosLine); HaveError = true; }
//                    //else if (match.Value.Split(' ')[0] != "string") { err.VrongTypeVariable(str, PosLine); HaveError = true; }
//                    //else if (match.Value.Split(' ')[0] != "int") { err.VrongTypeVariable(str, PosLine); HaveError = true; }
//                    //else if (match.Value.Split(' ')[0] != "char") { err.VrongTypeVariable(str, PosLine); HaveError = true; }
//                    //else if (match.Value.Split(' ')[0] != "bool") { err.VrongTypeVariable(str, PosLine); HaveError = true; }
//                    //else if (match.Value.Split(' ')[0] != "byte") { err.VrongTypeVariable(str, PosLine); HaveError = true; }
//                    //else if (match.Value.Split(' ')[0] != "var") { err.VrongTypeVariable(str, PosLine); HaveError = true; }
//                    //Console.WriteLine(match.Value);

//                }

//            }
//            if (HaveError) return;
//        }
//        public static void GetDataTypeVariable(string str, int pos, string type)
//        {
//            string[] splitforchek = str.Split(' ');


//            try
//            {
//                switch (type)
//                {
//                    case "int": token.Add(new Token(type, Convert.ToString(int.Parse(splitforchek[3])), pos)); splitforchek[3] = "removed"; break;
//                    case "string": token.Add(new Token(type, getstring(), pos)); splitforchek[3] = "removed"; break;
//                    case "bool": token.Add(new Token(type, Convert.ToString(bool.Parse(splitforchek[3])), pos)); splitforchek[3] = "removed"; break;
//                    case "float": token.Add(new Token(type, getfloat(), pos)); splitforchek[3] = "removed"; break;
//                    case "byte": token.Add(new Token(type, Convert.ToString(byte.Parse(splitforchek[3])), pos)); splitforchek[3] = "removed"; break;
//                    case "var": token.Add(new Token(type, splitforchek[3], pos)); splitforchek[3] = "removed"; break;
//                    case "char": token.Add(new Token(type, Convert.ToString(char.Parse(splitforchek[3])), pos)); splitforchek[3] = "removed"; break;
//                }
//            }
//            catch { err.VrongDataTipeVariable(str, pos); }

//            string getstring()
//            {
//                Regex regex = new Regex(@"([""'])(?:\\\1|.)*?\1");
//                MatchCollection matches = regex.Matches(str);
//                if (matches.Count > 0)
//                {
//                    return matches[0].Value;
//                }
//                else { err.VrongDataTipeVariable(str, pos); throw new Exception("sd"); }
//            }
//            string getfloat()
//            {
//                Regex regex = new Regex(@"[-+]?\d*\.\d+([eE][-+]?\d+)?");
//                MatchCollection matches = regex.Matches(str);
//                if (matches.Count > 0)
//                {
//                    return matches[0].Value;
//                }
//                else { err.VrongDataTipeVariable(str, pos); throw new Exception("sd"); }
//            }

//            foreach (var tok in token)
//            {
//                Console.Write($"TYPE {tok.Type} VALUE {tok.Value} LINE {tok.Line}");
//            }

//        }

//        public static void Command(string str, int PosLine)
//        {
//            /* print("pepeha");!
//             * input("pepega");!
//             * 
//             * loop(int i = 0; i < 12; i++)
//             * loop(true)
//             * do loop(i > 4)
//             * 
//             * if else elif -> or and
//             * 
//             * switch case
//             * 
//             * try catch
//             * 
//             * return!
//             * 
//             * fn
//             * 
//             * import!
//             * class!
//             */

//            string[] GetCommand = str.Split(' ');

//            for (int i = 0; i < GetCommand.Length; i++)
//            {
//                if (GetCommand[i] == "removed") continue;
//                switch (GetCommand[i])
//                {
//                    case "import": token.Add(new Token("Import", GetCommand[i + 1], PosLine)); break;
//                    case "return;": token.Add(new Token("Return;", GetCommand[i + 1], PosLine)); break;
//                    case "return": token.Add(new Token("Return", null, PosLine)); break;
//                    case "class": token.Add(new Token("class", GetCommand[i + 1], PosLine)); break;
//                }
//                if (str.IndexOf("print") > -1 || str.IndexOf("printl") > -1 || str.IndexOf("input") > -1)
//                {
//                    //Regex regexPrint = new Regex("[p]+[r]+[i]+[n]+[t]+[(]+[\"]+(\\w\\s)*[\"]+[)]");
//                    Regex regexPrint = new Regex("(print\\(\")|(print\\(\\$\")(\\w*\\W*\\s*)[\"][)]|(print\\(\")(\\w*\\W*\\s*)[\"][)]|(print\\(\\))|(printl\\(\\))");
//                    MatchCollection matchesPrint = regexPrint.Matches(str);
//                    if (matchesPrint.Count > 0)
//                    {
//                        foreach (Match match in matchesPrint)
//                            PrintInput(match.Value);
//                    }
//                    Regex regexInput = new Regex("(input\\(\")|(input\\(\\$\")(\\w*\\W*\\s*)+[\"][)]|(input\\(\\))");
//                    MatchCollection matchesInput = regexInput.Matches(str);
//                    if (matchesInput.Count > 0)
//                        foreach (Match match in matchesInput)
//                            PrintInput(match.Value);
//                }
//                else err.VrongNameComand(str, PosLine);
//            }

//            void PrintInput(string command)
//            {
//                string[] getcom = command.Split(new string[] { "(", ")" }, StringSplitOptions.None);

//                if (getcom[0] == "print") SplitPrintInputValue(command, "print");

//                else SplitPrintInputValue(command, "input");
//            }
//            void SplitPrintInputValue(string command, string strinp)
//            {
//                string[] getcom = command.Split(new string[] { "(", ")" }, StringSplitOptions.None);

//                bool Fstr = false;
//                bool writefstr = false;
//                bool Autoacran = false;
//                bool literal = true;
//                string Fvarname = "";
//                foreach (var item in getcom)
//                {
//                    Console.WriteLine(item);
//                }
//                if (getcom[1].Length > 0) { err.IfNotKavuchki(str, PosLine); return; }

//                List<string> stringmap = new List<string> { strinp };
//                if (strinp == "printl") stringmap.Add("END NOT NEW LINE");


//                if (getcom.Length > 1)//!!!!!!!!ЗАглушка для инпута
//                {
//                    char[] charcom = getcom[1].ToCharArray();
//                    for (int i = 0; i < charcom.Length; i++)
//                    {
//                        if (charcom[i] == '$' && i == 0) Fstr = true;
//                        if (charcom[i] == '@' && i == 0 || i == 1) Autoacran = true; // возможден баг что сторока с автоэкранированием но собаки на самом деле нет

//                        if (charcom[i] == '}' && Fstr)
//                        {
//                            writefstr = false;
//                            stringmap.Add(Fvarname);
//                            Fvarname = "";
//                        }
//                        if (charcom[i] == '{' && Fstr) { writefstr = true; continue; }
//                        if (writefstr) Fvarname += charcom[i];


//                        if (charcom[i] == '\\')
//                        {
//                            switch (charcom[i + 1])
//                            {
//                                case '\\': token.Add(new Token("literal", "\\", PosLine)); break;
//                                case 'n': token.Add(new Token("literal", "\\n", PosLine)); break;
//                                case 't': token.Add(new Token("literal", "\\t", PosLine)); break;
//                                default: err.VrongliteralComand(str, PosLine); return;
//                            }
//                        }

//                        // if (Fstr && writefstr && charcom[i] != '}') stringmap[j + 1] += charcom[i];

//                        i++;
//                    }
//                    Fstr = false;
//                    writefstr = false;
//                    Autoacran = false;
//                    literal = true;
//                    Fvarname = "";
//                }

//                void ifNoneParenе()  // если выражение не в скобках
//                {

//                }
//                void ifNotmarks()//если выражение не в кавычках
//                {

//                }
//                void iffigurParence()
//                {

//                }
//            }

//        }
//    }
//}
