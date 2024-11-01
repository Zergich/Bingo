using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pastel;


namespace Bingo
{
    public class ConsoleWork
    {
        class Settings
        {
            public string YP_Version;
            public string Gyurza_VErsion;
        }

        public void Distributeer(string flag)
        {
            switch(flag)
            {
                case "-h": Help(); break;
                case "-d": break;
                default:  StartBuild(flag); break;

            }
        }
        private void StartBuild(string Path)
        {
            if (!File.Exists(Path)) { Console.WriteLine(Color($"Не удалоь найти {Color(Path, "#d35816")}", "#de3419")); return; }

            string FileExtension = Path.Split('.')[Path.Split('.').Length-1];

            if (FileExtension != "bg") { Console.WriteLine(Color($"Расширение файла не соответствует расширению {Color(".bg", "#d35816")}", "#de3419")); return; }

            string FileData = File.ReadAllText(Path);
            LexrTest.StartLexer(FileData);
        }

        private string Color(string str, string colorhex)
        {
            return str.Pastel(colorhex);
        }
        private string ColorFlags(string str)
        {
            return str.Pastel("#367cff");
        }

        private string Path = @"D:\Progects\Bingo\Bingo\config.cfg";
        public void CreatCFH()
        {
            if (!File.Exists(Path) || File.ReadAllText(Path) == "")
            {
                Settings set = new Settings();
                set.YP_Version = "0.1";
                string json = JsonConvert.SerializeObject(set);
                File.WriteAllText(Path, json);
            }
        }

        private void Help()
        {

            string Helpmessage = $"Для компиляции консольного приложения используйте - {Color("bingo", "#a3d226")} {Color("SoursCodeFile.bg", "#30d226")} в качестве файла исходнога кода.\n" +
                $"{ColorFlags("-d")}: Скомпилировать код в библиотеку dll.\n";
            Console.WriteLine(Helpmessage);
        }
        public void BaseInfo()
        {
            Settings set = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path));

            Console.WriteLine($"Bingo version {set.YP_Version.Pastel("#9cd621")}. Actual {"?".Pastel("#d24526")} приделать парсинг на регекс к гитхабу");
            Console.WriteLine($"Gyrza version {set.Gyurza_VErsion.Pastel("#9cd621")}. Actual {"?".Pastel("#d24526")} приделать парсинг на регекс к гитхабу\n");




            string Helpmessage = $"Для компиляции консольного приложения используйте - {Color("bingo", "#a3d226")} {Color("SoursCodeFile.bg", "#30d226")} в качестве файла исходнога кода.\n" +
                $"Больше помощи: {ColorFlags("-h")}\n";
            Console.WriteLine(Helpmessage);
        }
    }
}
