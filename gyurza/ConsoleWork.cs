using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime;
using Pastel;
using System.Reflection.Emit;

namespace gyurza
{
    internal class ConsoleWork
    {
        class Settings
        {
            public string Gyurza_VErsion;
            public string YP_Version;
        }
        public void Distributeer(string flag)
        {
            switch (flag)
            {
                case "-h": Help(); break;
                case "-l": List(); break;

            }
        }


        private string Color(string str, string colorhex)
        {
            return str.Pastel(colorhex);
        }
        private string ColorFlags(string str)
        {
            return str.Pastel("#367cff");
        }

        private string PathConfig = @"D:\Progects\Bingo\Bingo\config.cfg";
        private string LibConfig = @"NON";
        public void CreatCFH()
        {
            if (!File.Exists(PathConfig) || File.ReadAllText(PathConfig) == "")
            {
                Settings set = new Settings();
                set.Gyurza_VErsion = "0.1";
                string json = JsonConvert.SerializeObject(set);
                File.WriteAllText(PathConfig, json);
            }
        }

        private void Help()
        {
            Console.WriteLine($"{ColorFlags("-l")} - Выводит список установленных библиотек.");
        }
        public void BaseInfo()
        {
            Settings set = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(PathConfig));

            Console.WriteLine($"Bingo version {set.YP_Version.Pastel("#9cd621")}. Actual {"?".Pastel("#d24526")} приделать парсинг на регекс к гитхабу");
            Console.WriteLine($"Gyrza version {set.Gyurza_VErsion.Pastel("#9cd621")}. Actual {"?".Pastel("#d24526")} приделать парсинг на регекс к гитхабу\n");




            string Helpmessage = $"Для уставнки библиотеки - {Color("gyurza", "#a3d226")} {Color("Libriry", "#30d226")} желаемая библиотека.\n" +
                $"Больше помощи: {ColorFlags("-h")}\n";
            Console.WriteLine(Helpmessage);
        }
        public void List()
        {
            Dictionary<string, string> getfullstringlib = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(LibConfig));
            int colvo = 0;
            int colvoneedupdate = 0;
            Console.WriteLine("Libraries\tVersion");
            Console.WriteLine("---------\t-------");

            string getActualVersion = "Берет версию с сервера";
            List<string> ListUpdate = new List<string>();
            foreach (var i in getfullstringlib)
            {
                Console.Write($"{Color(i.Key, "#47b61a")}");
                if (int.Parse(i.Value) < int.Parse(getActualVersion))
                {
                    Console.WriteLine($"{Color(i.Value, "#e52717")}");
                    ListUpdate.Add(i.Key);
                }
                else Console.WriteLine($"{Color(i.Value, "#bae517")}");


                colvo++;
                colvoneedupdate++;
            }
            Console.WriteLine($"Всего установленно библиотек: {Color($"{colvo}", "#bae517")}. Требуют обновлений: {Color($"{colvoneedupdate}", "#e52717")}");
            if (colvoneedupdate > 0)
            {
                poshelnaxyiwhile:
                Console.WriteLine("Обновить библиотеки y/n?");
                switch (Console.ReadLine().ToLower())
                {
                    case "y": break;
                    case "n": break;
                    case "д": break;
                    case "н": break;

                    default: goto poshelnaxyiwhile;
                }
            }
        }
    }
}
