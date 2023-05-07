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

                string str = Style.GetDashedLine(21) + $"It {Style.ColorFormat("works", ColorAnsi.RED, FormatAnsi.UNDERLINE)}!";
                Border border = new Border();
                Intersection intersection = new Intersection(BorderSide.Right, 1, '$');
                border.IntersectionList.Add(intersection);
                foreach (var item in border.Surround(new List<string>() { str }, 21 + "It works!".Length))
                {
                    Console.WriteLine(item);
                }

                Console.ReadKey();
            }
            gameLoop.Start();

        }
    }
}
