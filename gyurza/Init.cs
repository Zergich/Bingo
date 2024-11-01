using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyurza
{
    public class Init
    {
        // это менеджер бибилиотек :)
        // а когда это будет одному богу известно :(

        static void Main(string[] args)
        {
            ConsoleWork cw  = new ConsoleWork();
            cw.CreatCFH();
            if (args.Length != 0)
                cw.Distributeer(args[0]);
            else cw.BaseInfo();
        }
        public static void StartGyurza()
        {

        }
    }
}
