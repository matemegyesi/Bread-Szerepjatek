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

            if (false) // Állítsd true-ra ha le akarod tesztelni, hogy működnek-e a színek.
            {
                Console.OutputEncoding = Encoding.Unicode;
                Style.EnableStyling();
                foreach (string line in Style.TestStyle())
                {
                    Console.WriteLine(line);
                }
                Console.ReadKey();
            }

            gameLoop.Start();

        }
    }
}
