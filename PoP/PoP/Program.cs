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

                Console.WriteLine(Style.GetDashedLine(20) + $"It {Style.ColorFormat("works", ColorAnsi.RED, FormatAnsi.UNDERLINE)}!");

                Console.ReadKey();
            }
            gameLoop.Start();

        }
    }
}
