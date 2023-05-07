using PoP.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP
{
    class Program
    {
        static void Main(string[] args)
        {

            GameLoop gameLoop = new GameLoop();

            if (false) // Testing new rendering system
            {
                Console.OutputEncoding = Encoding.Unicode;
                Style.EnableStyling();

                WindowRenderer.Initialize();

                List<string> strList = WindowRenderer.Get();
                string str = string.Empty;
                foreach (var item in strList)
                {
                    str += item;
                }

                Console.Write(str);
                Console.SetCursorPosition(0, 0);

                Console.ReadKey();
            }
            gameLoop.Start();

        }
    }
}
