﻿//VariableNotFound - !+

/*
 * VariableNotFound - !+ это обозначение определяющие отлов ошибок с переменными на этапе атс с картой переменных
 */



using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
//using static Bingo.Lexer;
using static Bingo.Addons_UI.Color;

namespace Bingo
{
    public class LexrTest
    {
        static ErrorLexer err = new ErrorLexer();
        static List<Token> token = new List<Token>();

        struct VarNameValue
        {
            public string GlobalOrLocal;
            public string Value;
        }
        //static Dictionary<VarNameValue, string /*Name*/> VariableMap = new Dictionary<VarNameValue, string>();!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        static bool ASMBlock = false;

        static bool LoopBody = false;
        static bool DoLoopBody = false;

        static bool IF = false;
        static bool ElIF = false;
        static bool Else = false;

        static bool Try = false;
        static bool Catch = false;

        // для ловли ошибок с фигурными скобками
        static int countzakr = 0;
        static int countotkr = 0;
        public static void StartLexer(string fullcode)
        {
            //if (str.ToCharArray()[str.Length - 1] != ';') err.Thesemicolonismissing(str, PosLine);
            string[] str = fullcode.Replace("\r", string.Empty).Trim().Split('\n');


            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].ToCharArray()[0] == '/' && str[i].ToCharArray()[1] == '/') continue;
                if (str[i] == "") continue;
                FindeVariables(str[i].Trim('\r'), i + 1);
                Command(str[i].Trim('\r'), i + 1, "None", str);
            }
            ReturnTokens();
        }
        static void ReturnTokens()
        {
            int i = 1;
            foreach (var showtoken in token)
            {
                Console.WriteLine($"{i}) {CYAN}Type {MAGENTA}{showtoken.Type} {GREEN}Value {MAGENTA}{showtoken.Value} {YELLOW}Line {MAGENTA}{showtoken.Line}{NORMAL}");
                //foreach (var i in token) Console.WriteLine($"Value {i.Value}, Type {i.Type}, Line {i.Line}");
                i++;
            }
        }
        static bool HaveVariabel = false; // чтоб не выдаало ошибку что нет такого ключегого слова
        static void FindeVariables(string str, int PosLine)
        {
            bool HaveError = false;

            Regex regex = new Regex("\\w*\\s[a-zA-Z_]+[a-zA-Z_0-9_]*\\s[=]\\s");
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    switch (match.Value.Split(' ')[0])
                    {
                        case "int": GetDataTypeVariable(str, PosLine, "int_Variable"); HaveVariabel = true; break;
                        case "string": GetDataTypeVariable(str, PosLine, "string_Variable"); HaveVariabel = true; break;
                        case "char": GetDataTypeVariable(str, PosLine, "char_Variable"); HaveVariabel = true; break;
                        case "byte": GetDataTypeVariable(str, PosLine, "byte_Variable"); HaveVariabel = true; break;
                        case "bool": GetDataTypeVariable(str, PosLine, "bool_Variable"); HaveVariabel = true; break;
                        case "float": GetDataTypeVariable(str, PosLine, "float_Variable"); HaveVariabel = true; break;
                        case "var": GetDataTypeVariable(str, PosLine, "var_Variable"); HaveVariabel = true; break;

                        default: err.VrongTipeVariable(str, PosLine); HaveError = true; continue;
                    }
                }

            }
            if (HaveError) return;
        }
        public static void GetDataTypeVariable(string str, int pos, string type)
        {
            string[] splitforchek = str.Split(' ');
            int needIndex = 0;

            Regex regeex = new Regex("\\w*\\s[a-zA-Z_]+[a-zA-Z_0-9_]*\\s[=]");
            MatchCollection matchees = regeex.Matches(str);
            for (int i = 0; i < splitforchek.Length; i++)
            {
                if (matchees.Count > 0) { /*Console.WriteLine("pepega");*/ }
                
            }
            foreach(var d in matchees) Console.WriteLine(d);
            //Console.WriteLine(splitforchek[needIndex]);
            try
            {
                switch (type)
                {
                    case "int_Variable": token.Add(new Token(type, Convert.ToString(int.Parse(splitforchek[needIndex])), "var", pos)); splitforchek[needIndex] = "removed";  break;
                    case "string_Variable": token.Add(new Token(type, getstring(), "var", pos)); splitforchek[needIndex] = "removed";  break;
                    case "bool_Variable": token.Add(new Token(type, Convert.ToString(bool.Parse(splitforchek[needIndex])), "var", pos)); splitforchek[needIndex] = "removed"; break;
                    case "float_Variable": token.Add(new Token(type, getfloat(), "var", pos)); splitforchek[needIndex] = "removed";  break;
                    case "byte_Variable": token.Add(new Token(type, Convert.ToString(byte.Parse(splitforchek[needIndex])), "var", pos)); splitforchek[needIndex] = "removed"; /*HaveVariabel = false;*/ break;
                    case "var_Variable": token.Add(new Token(type, splitforchek[needIndex], "var", pos)); splitforchek[needIndex] = "removed"; /*HaveVariabel = false;*/ break;
                    case "char_Variable": token.Add(new Token(type, Convert.ToString(char.Parse(splitforchek[needIndex])), "var", pos)); splitforchek[needIndex] = "removed"; /*HaveVariabel = false;*/ break;
                }
            }
            catch { err.VrongDataTipeVariable(str, pos); }

            string getstring()
            {
                Regex regex = new Regex(@"([""'])(?:\\\1|.)*?\1");
                MatchCollection matches = regex.Matches(str);
                if (matches.Count > 0)
                {
                    return matches[0].Value;
                }
                else { err.VrongDataTipeVariable(str, pos); throw new Exception("sd"); }
            }
            string getfloat()
            {
                Regex regex = new Regex(@"[-+]?\d*\.\d+([eE][-+]?\d+)?");
                MatchCollection matches = regex.Matches(str);
                if (matches.Count > 0)
                {
                    return matches[0].Value;
                }
                else { err.VrongDataTipeVariable(str, pos); throw new Exception("sd"); }
            }

            //foreach (var tok in token)
            //{
            //    Console.Write($"TYPE {tok.Type} VALUE {tok.Value} LINE {tok.Line}");
            //}

        }

        static bool HaveCommandWord = false;

        public static void Command(string str, int PosLine, string vlogenie, string[] Fullstr)// что вложенно и глубина if_1
        {

            string[] GetCommand = str.Split(' ');

            if (ASMBlock) ASM(str, PosLine);


            for (int i = 0; i < GetCommand.Length; i++)
            {
                if (ASMBlock) break;
                //if (GetCommand[i] == "removed") continue;
                switch (GetCommand[i])
                {
                    case "import": token.Add(new Token("Import", GetCommand[i + 1], "none", PosLine)); break;
                    case "return;": token.Add(new Token("Return;", null, "none", PosLine)); break;
                    case "return": token.Add(new Token("Return", GetCommand[i + 1], "none", PosLine)); break;
                    case "continue": token.Add(new Token("Continue", null, "none", PosLine)); break;
                    case "break": token.Add(new Token("Break", null, "none", PosLine)); break;
                    case "class": token.Add(new Token("class", GetCommand[i + 1], "none", PosLine)); break;
                    case "lable": token.Add(new Token("Label_Goto", GetCommand[i+1], "none", PosLine)); break;

                    case "fn": break;


                    case "else": Branches(GetCommand[1], PosLine, str, "else", vlogenie); HaveCommandWord = true;
                        str = "";
                        for (int j = 1; j < GetCommand.Length; j++)
                        {
                            str += GetCommand[j]+" ";
                        }
                        char[] chars = str.ToCharArray();
                        chars[0] = ' ';
                        chars[chars.Length - 2] = ' ';

                        str = new string(chars);
                        break;
                    case "asm": ASMBlock = true; HaveCommandWord = true; break;



                        //default: err.VrongNameComand(GetCommand[i], PosLine); wronggramar = true; Console.WriteLine("asdasdfasfdad"); return;

                }
            }
            int glubina = 0;
            if (IF) { vlogenie = $"IF_{glubina}"; glubina++; }
            if (Else) { vlogenie = $"ELSE_{glubina}"; glubina++; }
            if (ElIF) { vlogenie = $"ELIF_{glubina}"; glubina++; }


            //command
            string[] GetCommandArgs = str.Split(new string[] { "(", ")" }, StringSplitOptions.None);

            for (int i = 0; i < GetCommandArgs.Length; i++)
            {
                if (ASMBlock) break;
                if (GetCommandArgs[i] == "") continue;

                try
                {
                    if (GetCommandArgs[i].ToCharArray()[1] == '{') //if(2 > 3) {print("asdasd") printl("asdasd453345345") print("asd")} для таких случаев чтоб удалять первую {
                    {
                        char[] deletFigSkobk = GetCommandArgs[i].ToCharArray();
                        //Console.WriteLine(deletFigSkobk[1]);
                        deletFigSkobk[1] = ' ';
                        GetCommandArgs[i] = new string(deletFigSkobk);
                    }
                }
                catch { }
                //if (GetCommand[i] == "removed") continue;
                //Console.WriteLine(GetCommandArgs[i].ToCharArray()[1]);
                //Console.WriteLine(GetCommandArgs[i]);
                switch (GetCommandArgs[i].Trim('\t').Trim())
                {
                    case "try": HaveCommandWord = true; break;//-
                    case "catch": HaveCommandWord = true; break;//добавить что после закрывающей скобки обязательно должен идти катч и между ничего быть не может
                    case "panic": Panic(GetCommandArgs, PosLine, vlogenie); HaveCommandWord = true; break;


                    case "print":
                    case "input":
                    case "printl": printbuilding(str, false, GetCommandArgs[i]); Print(GetCommandArgs, PosLine, i, GetCommandArgs[i + 1]); HaveCommandWord = true; break;


                    case "shell": HaveCommandWord = true; Shell(GetCommandArgs, PosLine, vlogenie); break; // как system в C++ 


                    case "switch": HaveCommandWord = true; break;//-

                    case "if": Branches(GetCommandArgs[1], PosLine, str, "if", vlogenie); HaveCommandWord = true; break;
                    case "elif": /*NotFigur();*/  Branches(GetCommandArgs[1], PosLine, str, "elif", vlogenie); HaveCommandWord = true; break;


                    case "dolp": DoloopIn++; DoLoop(GetCommandArgs[1], PosLine, str, vlogenie); HaveCommandWord = true; break; // do loop
                    case "loop": Loop(GetCommandArgs[1], PosLine, str, vlogenie); HaveCommandWord = true; break;

                    case "goto": HaveCommandWord = true; Goto(GetCommandArgs, PosLine, vlogenie); break;

                    default: if (!HaveCommandWord && !HaveVariabel) err.VrongNameComand(GetCommandArgs[0], PosLine); break;
                }
            } // HaveVariabel переменная всегда равна тру !!!!!!
            HaveVariabel = false;

            void printbuilding(string valuestring, bool tchk, string printl)// тчк точка с запаятой для однострочного if
            {
                char[] ChekErrorPrint = new char[] { };
                if (printl == "printl")
                    ChekErrorPrint = valuestring.ToCharArray().Skip(6).ToArray();
                else
                    ChekErrorPrint = valuestring.ToCharArray().Skip(5).ToArray();

                bool kavuchki = false;
                int otkr = 0;
                int zakr = 0;
                if (!tchk) // print 
                {
                    for (int i = 0; i < ChekErrorPrint.Length; i++)
                    {
                        //Console.WriteLine(ChekErrorPrint[i]);
                        if (i == ChekErrorPrint.Length - 2 && ChekErrorPrint[i] != '"') err.WrongParametrs(str, PosLine);


                        if (ChekErrorPrint[i] == '"')
                        {
                            if (i != 0 && ChekErrorPrint[i - 1] == '\\' && ChekErrorPrint[i] == '"')
                            {
                                continue; // для \"
                            }
                        }

                        if (ChekErrorPrint[i] == '"')
                        {
                            if (i != 0 && ChekErrorPrint[i - 1] != '\\' && ChekErrorPrint[i] == '"' && kavuchki)
                                kavuchki = false;
                        }

                        if (ChekErrorPrint[i] == '"' && !kavuchki && i == 1 || i == 2)
                        {
                            //Console.WriteLine(ChekErrorPrint[i - 1]);
                            kavuchki = true;
                            //if (ChekErrorPrint[i - 1] == '$') addliteral = "$";
                            //else if (ChekErrorPrint[i - 1] == '@') addliteral = "@";
                            //else if (ChekErrorPrint[i - 1] == '$' && ChekErrorPrint[i - 2] == '@') addliteral = "$@";
                            //else if (ChekErrorPrint[i - 1] == '@' && ChekErrorPrint[i - 2] == '$') addliteral = "$@";
                            //else if (ChekErrorPrint[i - 1] != '(') err.VrongliteralComand(str, PosLine);
                        }

                        if (!kavuchki)
                        {
                            if (ChekErrorPrint[i] == '(') otkr++;
                            if (ChekErrorPrint[i] == ')') zakr++;
                        }
                    }
                }
                if (otkr != zakr)
                {
                    err.WrongParametrs(str, PosLine);
                    Console.WriteLine($"{RED}Или выражение не в кавычках{NORMAL}");
                }
            }
            //void NotFigur()
            //{
            //    char[] linechar = str.Trim(' ').ToCharArray(); // posline -1 потому что в инициализации i+1 для понятного отображения строк  
            //    bool jh = str.ToCharArray()[0] != '{' && linechar[linechar.Length - 2] != ')';

            //    Regex regex = new Regex(@"{[^}]+\{?}*[^{]+\}");
            //    MatchCollection matches = regex.Matches(str.Trim('\r'));

            //    bool regexfalse = true;
            //    bool arrayfalseinline = true; //if() {
            //    bool arrayfaulenextline = true; // if()
            //                                    //{


            //    bool UslNextSkobk = Fullstr[PosLine - 1].Trim(' ').ToCharArray()[Fullstr[PosLine - 1].ToCharArray().Length - 2] == ')'; //if() скобка на следуйщей строке
            //                                                                                                                            //{

            //    //Console.WriteLine(Fullstr[PosLine - 1].Trim(' ').ToCharArray()[Fullstr[PosLine - 1].ToCharArray().Length - 3]);        
            //    //Console.WriteLine(linechar[linechar.Length - 3]);
            //    //Console.WriteLine(linechar[linechar.Length - 1]);

            //    if (matches.Count == 0) regexfalse = false;
            //    try
            //    {// ошибка в индексах                                                                               // так надо смысл в индексе 3
            //        if ((UslNextSkobk || Fullstr[PosLine - 1].Trim(' ').ToCharArray()[Fullstr[PosLine - 1].ToCharArray().Length - 3] == ')') && Fullstr[PosLine] == "{\r") { } else arrayfaulenextline = false;
            //        if (linechar[linechar.Length - 3] == ')' && linechar[linechar.Length - 1] == '{') { } else arrayfalseinline = false;
            //    }
            //    catch (IndexOutOfRangeException) { }

            //    if (!regexfalse && !arrayfalseinline && !arrayfaulenextline) { err.IfNotFigSkobk(str, PosLine); Environment.Exit(1); }

            //    //Console.WriteLine($"{regexfalse} {arrayfalseinline} {arrayfaulenextline}");
            //}
        }
        static void Branches(string str, int Posline, string Fullstr, string IF, string vlogenie)
        {
            string branch = "";
            if (IF == "if") branch = "IF";
            else if (IF == "elif") branch = "ELIF";
            else if (IF == "else") branch = "ELSE";

            string[] line = str.Split(' ');
            string[] getfigurskobk = Fullstr.Split(' ');

            //if (Fullstr == "{" || getfigurskobk[getfigurskobk.Length - 1] == "{")
            //{
            //    LoopBody = false;
            //}
            //else if (getfigurskobk[0] != "}" && getfigurskobk[getfigurskobk.Length - 1] != "}") { err.IfNotFigSkobk(Fullstr, Posline); return; }

            string[] Zaglushka = new string[] { "0000" };
            string[] OneArray = Zaglushka.Concat(line).ToArray();

            try
            {
                if (IF != "else")
                { 
                    bool ValidVar = false; // лексически правильный вид переменной
                    bool stringVarValidate = false; // чтоб не возникало ошибки у int Parse
                    for (int i = 1; i < OneArray.Length; i++) //!!!!!!!!!!!!!!!!!! возможна ошибка +1
                    {
                        if (OneArray[i] == "true" || OneArray[i] == "false") { token.Add(new Token($"{branch}_True__or__False", OneArray[i], vlogenie, Posline)); continue; }
                        if (OneArray[i] == "or" || OneArray[i] == "and") { token.Add(new Token($"{branch}_Or__or__AND", OneArray[i], vlogenie, Posline)); continue; }

                        Regex regex = new Regex("^(_|)+[a-zA-Z_]+[a-zA-Z_0-9_]*");
                        MatchCollection matches = regex.Matches(OneArray[i]);

                        if (i % 4 == 1 && matches.Count == 0)
                        {
                            char[] chars = new char[] { };
                            Array.Clear(chars, 0, chars.Length);
                            chars = OneArray[i].ToCharArray();

                            //foreach(char c in chars) Console.WriteLine(c);
                            if ((chars[0] == '"' && chars[chars.Length - 1] == '"') || (chars[0] == '\'' && chars[chars.Length - 1] == '\'')) { ValidVar = true; stringVarValidate = true; }// проверка на строку
                            if (!stringVarValidate)
                            {
                                try { int.Parse(OneArray[i]); ValidVar = true; }
                                catch { err.VrongTipeVariable(Fullstr, Posline); return; }
                                stringVarValidate = false;
                            }
                        }

                        if (matches.Count == 0 && !ValidVar && i % 4 == 1) { err.VrongTipeVariable(Fullstr, Posline); return; }
                        if (i % 4 == 1) token.Add(new Token($"{branch}_VAR", OneArray[i], vlogenie, Posline));
                        else
                        { // i % 4 и тд это позиции на которых стоят условия 
                            if (i % 2 == 0)
                            {
                                switch (OneArray[i])
                                {
                                    case "<": token.Add(new Token($"{branch}_LogicOperation_<", "<", vlogenie, Posline)); break;
                                    case ">": token.Add(new Token($"{branch}_LogicOperation_>", ">", vlogenie, Posline)); break;
                                    case "==": token.Add(new Token($"{branch}_LogicOperation_==", "==", vlogenie, Posline)); break;
                                    case ">=": token.Add(new Token($"{branch}_LogicOperation_>=", ">=", vlogenie, Posline)); break;
                                    case "<=": token.Add(new Token($"{branch}_LogicOperation_<=", "<=", vlogenie, Posline)); break;
                                    case "!=": token.Add(new Token($"{branch}_LogicOperation_!=", "Poshel Nahui", vlogenie, Posline)); break;
                                    default: err.WrongBoolOperator(Fullstr, Posline); break;
                                }
                                token.Add(new Token($"{branch}_Value", OneArray[i + 1], vlogenie, Posline)); // остаток от 3 работает только до 11, по этому +1 в свиче

                            }

                        }

                    }
                    //foreach (var i in token) Console.WriteLine($"Value {i.Value}, Type {i.Type}, Line {i.Line}");

                    //string[] GetCommandStartEnd = Fullstr.Split(new string[] { "{", "}" }, StringSplitOptions.None);
                    //if (GetCommandStartEnd.Length <= 3) { }
                    //else
                    //{
                    //    for (int i = 0; i < GetCommandStartEnd.Length; i++)
                    //    {
                    //        if (GetCommandStartEnd[i] == "\n") Posline++;
                    //        FindeVariables(str, Posline);
                    //        Command(str, Posline, $"{IF}_{IFIn}"); // идет дальше сканировать тело с пометкой о глубине и то в каком операторе находится
                    //    }
                    //}
                }
                else token.Add(new Token("ELSE", "Non", vlogenie, Posline));// и добавить тело 
            }
            catch { }
            HaveCommandWord = false;
            //BranchBody = false;

        }
        static int DoloopIn = 0;  // вложенных циклов
        static void DoLoop(string str, int Posline, string Fullstr, string Vlogennost) // на стадии атс добавить распределение на 1 итерацию или как логика скажет
        {
            string[] line = str.Split(' ');//.Skip<string>(1).ToArray<string>();
            string[] getfigurskobk = Fullstr.Split(' ');

            //if (Fullstr == "{" || getfigurskobk[getfigurskobk.Length - 1] == "{")
            //{
            //    LoopBody = false;
            //}
            //else if (getfigurskobk[0] != "}" && getfigurskobk[getfigurskobk.Length - 1] != "}") { err.IfNotFigSkobk(Fullstr, Posline); }

            string[] Zaglushka = new string[] { "0000" };
            string[] OneArray = Zaglushka.Concat(line).ToArray();

            // НАСЛЕДИЕ КОДА 
            //int CVIPUC = 1; // ColVoIteraciyProveganiyUsloviyCicla кол-во итераций пробегания условия цикла |

            try
            {
                Regex regexF = new Regex("^(_|)+[a-zA-Z_]+[a-zA-Z_0-9_]*");
                MatchCollection matchesF = regexF.Matches(line[1]);

                for (int i = 1; i < OneArray.Length; i++) //!!!!!!!!!!!!!!!!!! возможна ошибка +1
                {
                    if (OneArray[i] == "true" || OneArray[i] == "false") { token.Add(new Token("DoWhile_True__or__False", OneArray[i], Vlogennost, Posline)); continue; }
                    if (OneArray[i] == "or" || OneArray[i] == "and") { token.Add(new Token("DoWhile_Or__or__AND", OneArray[i], Vlogennost, Posline)); continue; }

                    Regex regex = new Regex("^(_|)+[a-zA-Z_]+[a-zA-Z_0-9_]*");
                    MatchCollection matches = regex.Matches(OneArray[i]);

                    if (matches.Count == 0 && i % 4 == 1) err.VrongTipeVariable(Fullstr, Posline);
                    if (i % 4 == 1) token.Add(new Token("DoWHILE_VAR", OneArray[i], Vlogennost, Posline));
                    else
                    {
                        if (i % 2 == 0)
                        {
                            switch (OneArray[i])
                            {
                                case "<": token.Add(new Token("DoWHILE_LogicOperation_<", "<", Vlogennost, Posline)); break;
                                case ">": token.Add(new Token("DoWHILE_LogicOperation_>", ">", Vlogennost, Posline)); break;
                                case "==": token.Add(new Token("DoWHILE_LogicOperation_==", "==", Vlogennost, Posline)); break;
                                case ">=": token.Add(new Token("DoWHILE_LogicOperation_>=", ">=", Vlogennost, Posline)); break;
                                case "<=": token.Add(new Token("DoWHILE_LogicOperation_<=", "<=", Vlogennost, Posline)); break;
                                case "!=": token.Add(new Token("DoWHILE_LogicOperation_!=", "Poshel Nahui", Vlogennost, Posline)); break;
                                default: err.WrongBoolOperator(Fullstr, Posline); break;
                            }
                            token.Add(new Token("DoWHILE_Value", OneArray[i + 1], Vlogennost, Posline)); // остаток от 3 работает только до 11, по этому +1 в свиче

                        }

                    }

                }
                //foreach (var i in token) Console.WriteLine($"Value {i.Value}, Type {i.Type}, Line {i.Line}");

            }
            catch { }
            HaveCommandWord = false;
            DoloopIn = 0;
        }


        static int WhileLoopIn = 0;
        static int ForLoopIn = 0;


        static void Loop(string str, int Posline, string Fullstr, string vlogenie)
        {
            string[] line = str.Split(' ');//.Skip<string>(1).ToArray<string>();
            string[] getfigurskobk = Fullstr.Split(' ');

            //if (Fullstr == "{" || getfigurskobk[getfigurskobk.Length - 1] == "{")
            //{
            //    LoopBody = false;
            //}
            //else if (getfigurskobk[0] != "}" && getfigurskobk[getfigurskobk.Length - 1] != "}") { err.IfNotFigSkobk(Fullstr, Posline); }

            string IJValue = "";
            string VarName = "";

            bool IFNotTupe = false;
            bool ISStartLoopRealization = true; // чтоб не надо было стартовать функцию поиска вайла в сроке после фора
            WhileRealization();

            void WhileRealization()
            {

                ISStartLoopRealization = true;
                string[] Zaglushka = new string[] { "0000" };
                string[] OneArray = Zaglushka.Concat(line).ToArray(); // чтоб массив стартовал с 1 а не с 0. это для остатка от делегия 

                // НАСЛЕДИЕ КОДА 
                //int CVIPUC = 1; // ColVoIteraciyProveganiyUsloviyCicla кол-во итераций пробегания условия цикла |

                try
                {
                    Regex regexF = new Regex("^(_|)+[a-zA-Z_]+[a-zA-Z_0-9_]*");
                    MatchCollection matchesF = regexF.Matches(line[1]);
                    if (line[0] == "int" && matchesF.Count != 0) ForRealization();
                    WhileLoopIn++;

                    if (ISStartLoopRealization)
                    {
                        for (int i = 1; i < OneArray.Length; i++) //!!!!!!!!!!!!!!!!!! возможна ошибка +1
                        {
                            if (OneArray[i] == "true" || OneArray[i] == "false") { token.Add(new Token("While_True__or__False", OneArray[i], vlogenie, Posline)); continue; }
                            if (OneArray[i] == "or" || OneArray[i] == "and") { token.Add(new Token("While_Or__or__AND", OneArray[i], vlogenie, Posline)); continue; }

                            Console.WriteLine($"{i}-{OneArray[i]}");
                            Regex regex = new Regex("^(_|)+[a-zA-Z_]+[a-zA-Z_0-9_]*");
                            MatchCollection matches = regex.Matches(OneArray[i]);

                            if (matches.Count == 0 && i % 4 == 1) err.VrongTipeVariable(Fullstr, Posline);
                            if (i % 4 == 1) token.Add(new Token("WHILE_VAR", OneArray[i], vlogenie, Posline));
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    switch (OneArray[i])
                                    {
                                        case "<": token.Add(new Token("WHILE_LogicOperation_<", "<", vlogenie, Posline)); break;
                                        case ">": token.Add(new Token("WHILE_LogicOperation_>", ">", vlogenie, Posline)); break;
                                        case "==": token.Add(new Token("WHILE_LogicOperation_==", "==", vlogenie, Posline)); break;
                                        case ">=": token.Add(new Token("WHILE_LogicOperation_>=", ">=", vlogenie, Posline)); break;
                                        case "<=": token.Add(new Token("WHILE_LogicOperation_<=", "<=", vlogenie, Posline)); break;
                                        case "!=": token.Add(new Token("WHILE_LogicOperation_!=", "Poshel Nahui", vlogenie, Posline)); break;
                                        default: err.WrongBoolOperator(Fullstr, Posline); break;
                                    }
                                    token.Add(new Token("WHILE_Value", OneArray[i + 1], vlogenie, Posline)); // остаток от 3 работает только до 11, по этому +1 в свиче

                                }

                            }
                        }
                        //foreach(var i in token) Console.WriteLine($"Value {i.Value}, Type {i.Type}, Line {i.Line}");
                    }
                }
                // проверить находится переменная(ные) в карте или числа же это числа и так же отлавить здесь несколько ошибьок чтоб не делать жто на стадии атс
                catch { }
                HaveCommandWord = false;
            }
            void ForRealization()
            {
                //int i = 0;
                ForLoopIn++;
                try
                {
                    if (line[0] == "int")
                    {
                        Regex regexInt = new Regex("\\W?");
                        MatchCollection matchesInt = regexInt.Matches(line[2]);
                        if (matchesInt.Count == 0) err.IfNotINT(Fullstr, Posline);

                        Regex regex = new Regex("^(_|)+[a-zA-Z_]+[a-zA-Z_0-9_]*");
                        MatchCollection matches = regex.Matches(line[1]);
                        if (matches.Count == 0) err.VrongTipeVariable(Fullstr, Posline);
                        else VarName = line[2];
                        IJValue = Convert.ToString(int.Parse(line[3]));  //VariableNotFound - !+

                    }
                    else
                    {
                        //VariableNotFound - !+

                        Regex regex = new Regex("^(_|)+[a-zA-Z_]+[a-zA-Z_0-9_]*");
                        MatchCollection matches = regex.Matches(line[0]);
                        if (matches.Count == 0) err.VrongTipeVariable(Fullstr, Posline);
                        else VarName = line[1];
                        IJValue = Convert.ToString(int.Parse(line[2])); //VariableNotFound - !+
                        IFNotTupe = true;
                    }
                }
                catch { err.WrongINTValueInLoop(Fullstr, Posline); }

                token.Add(new Token("FOR_LOOP_VARIABLE_Local", IJValue, vlogenie, Posline));
                //VarNameValue varmap = new VarNameValue { GlobalOrLocal = "Local_ForVar", Value = IJValue };!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //VariableMap.Add(varmap, $"{(IFNotTupe ? line[0] : line[1])}");


                // i < 10;
                int RangeValueINT = 0;
                try
                {
                    if (line.Length == 8) RangeValueINT = int.Parse(line[6]);
                    else RangeValueINT = int.Parse(line[5]);

                }
                catch { err.WrongINTValueInLoop(Fullstr, Posline); }

                // Console.WriteLine(IFNotTupe ? line[4] : line[5]  + " asdfasdfadsfsdf");
                switch (IFNotTupe ? line[4] : line[5])
                {
                    case "<": token.Add(new Token("LogicOperation_<", "<", vlogenie, Posline)); break;
                    case ">": token.Add(new Token("LogicOperation_>", ">", vlogenie, Posline)); break;
                    case "==": token.Add(new Token("LogicOperation_==", "==", vlogenie, Posline)); break;
                    case ">=": token.Add(new Token("LogicOperation_>=", ">=", vlogenie, Posline)); break;
                    case "<=": token.Add(new Token("LogicOperation_<=", "<=", vlogenie, Posline)); break;
                    case "!=": token.Add(new Token("WHILE_LogicOperation_!=", "Poshel Nahui", vlogenie, Posline)); break;
                    default: err.WrongBoolOperator(Fullstr, Posline); return;
                }
                //Uslovie += $"{(IFNotTupe ? getfigskobk[7] : getfigskobk[8])}";

                //i++
                char[] incremendDI = IFNotTupe ? line[6].ToCharArray() : line[7].ToCharArray();
                string PlusOrMinus = incremendDI[incremendDI.Length - 2].ToString() + incremendDI[incremendDI.Length - 1].ToString();

                if (PlusOrMinus == "++" || PlusOrMinus == "--") token.Add(new Token("Increment&Dicrement", PlusOrMinus, vlogenie, Posline));
                else err.WrongIncremebtOperator(Fullstr, Posline);
                //for (int i = 0; i < incremendDI.Length; i++)
                //{
                //    //VariableNotFound - !+
                //    if (incremendDI[incremendDI.Length -2] != VarName)
                //}

                IFNotTupe = false;
                HaveCommandWord = false;
                ISStartLoopRealization = false;
            }
            WhileLoopIn = 0;
            ForLoopIn = 0;
        }
        static void ASM(string str, int Posline) // по идеир не срабатывает по тому ччто есть только одна строка нужно болеше строк для обработки тела и ошмбок++
        {
            string asmcommand = "";
            char[] getfigskobk = str.ToCharArray();

            if (str == "}" || getfigskobk[getfigskobk.Length - 1] == '}') // возможна проблема с фигурными скобками
            {
                token.Add(new Token("ASM", asmcommand, "ASM_Block", Posline));
                ASMBlock = false;
            }
            if (getfigskobk[0] != '{' && getfigskobk[getfigskobk.Length - 1] != '}') { err.IfNotFigSkobk(str, Posline); }

            if (ASMBlock) asmcommand += str;
            HaveCommandWord = false;

        }

        public static void Panic(string[] MsgToPanic, int Posline, string vlogenie)
        {
            token.Add(new Token("PANIC", MsgToPanic[1], vlogenie, Posline));
        }
        public static void Shell(string[] MsgToPanic, int Posline, string vlogenie) => token.Add(new Token("SHELL", MsgToPanic[1], vlogenie, Posline));

        public static void Goto(string[] MsgToPanic, int Posline, string vlogenie) => token.Add(new Token("Goto", MsgToPanic[1], vlogenie, Posline));

        public static void Print(string[] str, int Position, int Index, string fuullstr) // index для имени токена в случиях if(2 > 3) {print("asdasd") printl("asdasd453345345") print("asd")}
        {
            List<string> returntokens = new List<string>();
            returntokens.Add(str[Index]);

            char[] ChekErrorPrint = fuullstr.ToCharArray();
            char[] getchars = str[1].ToCharArray();
            int countkavychki = 0;
            for (int i = 0; i < ChekErrorPrint.Length; i++)
            {
                //if (ChekErrorPrint[i] == '"' && !kavuchki) kavuchki = true;
                //else if (ChekErrorPrint[i] == '"' && kavuchki) kavuchki = false;


                if (ChekErrorPrint[i] == '"') if (i != 0 && ChekErrorPrint[i - 1] != '\\' && ChekErrorPrint[i] == '"') countkavychki++;// ошибка на начаое чтения
                if (ChekErrorPrint[i] == '"' && i == 0)  countkavychki++;  // специально для начала чтения ВСЕ СЛУЖИТ ДЛЯ ПРОПУСКА ЭКРАНИРОВАННОЙ \"
            }
            if (countkavychki % 2 != 0 || countkavychki == 0) err.IfNotKavuchki(fuullstr, Position);

            string returnstring = "";
            bool Fstr = false;

            string varnamefstr = "";
            bool wrirtefstr = false;

            //Console.WriteLine(getchars.Count);
            //for (int i = 0; i < getchars.Count; i++)
            //{
            //    Console.WriteLine(getchars[i]);
            //}


            if (getchars.Length == 0) { goto recognize; }
            else if (getchars[0] == '$') { Fstr = true; goto recognize; }
            else if (getchars[0] == '@') { goto recognize; }
            else if (getchars[0] != '"') { err.DontWount(str[1], Position); return; }

        recognize:
            try
            {
                for (int i = 0; i < getchars.Length-1; i++)
                {
                    if (getchars[i] == '}' && Fstr) { wrirtefstr = false; returntokens.Add(returnstring); returntokens.Add("VARIABLE(" + varnamefstr + ")"); returnstring = ""; continue; }
                    if (getchars[i] == '{' && Fstr) { wrirtefstr = true; continue; } // добавить ошибку если есть ф строка и неправильное расположение скобок
                    if (wrirtefstr) { varnamefstr += getchars[i]; }
                    else { returnstring += getchars[i]; continue; }


                    if (getchars[0] == '"' && i == 0) continue;
                    if (getchars[getchars.Length - 1] == '"' && i == getchars.Length - 1) continue;

                    if (getchars[i] == '"' && getchars[i + 1] == '"') { returnstring += getchars[i]; continue; }
                    else if (getchars[i] == '\\' && getchars[i + 1] == '"') { returnstring += getchars[i]; continue; }

                    else if (getchars[i] == '\\' && getchars[i + 1] == '\\') { returnstring += getchars[i]; continue; }

                    else if (getchars[i] == '\\' && getchars[i + 1] == 'n' ) { returntokens.Add(returnstring); returntokens.Add("NEW_LINE"); returnstring = ""; continue; }
                    else if (getchars[i] == '\\' && getchars[i + 1] == 't' /*|| getchars[i - 1] == '\\' && getchars[i] == 't'*/) { returntokens.Add(returnstring); returntokens.Add("TAB"); returnstring = ""; continue; }

                    else if (char.IsLetter(getchars[i])) returnstring += getchars[i];

                    else { err.VrongliteralComand(str[1], Position); return; }
                }
            }
            catch (IndexOutOfRangeException e) { Console.WriteLine(e.Message); }
            returntokens.Add(returnstring);

            //string[] s = returnstring.Split(' ');
            //for (int i = 0; i < s.Length; i++)
            //{
            //    if (s[i] == "\\n") Console.WriteLine("PE{E{EPEPEPEPEPEP");
            //    Console.WriteLine(i + " " + s[i]);
            //}

            //for (int i = 0; i < getchars.Length; i++)
            //{
            //    Console.WriteLine(i + " " + getchars[i]);
            //}

            string Fullvalue = ""; // для данных в классе токена
            for (int i = 1; i < returntokens.ToArray().Length; i++) Fullvalue += returntokens[i];
            token.Add(new Token(returntokens.ToArray()[0].Trim('\t').Trim(), Fullvalue, "Потом добавить для всех слов фортат \"0x101\"", Position)); // модет быть в будующим баги которых не видно из за это го

            //foreach (var token in returntokens) Console.WriteLine(token);


            //token.Add(new Token(str[0], ));

            returnstring = "";
            Fstr = false;
            HaveCommandWord = false;

        }
    }
}
