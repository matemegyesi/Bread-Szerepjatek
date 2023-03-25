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

            if (true) // Equip tesztelés (később törlöm)
            {
                gameLoop.ReadItemFile("res\\itemFile.txt");

                Console.WriteLine("(1)");
                foreach (var item in Inventory.inventory)
                {
                    Console.WriteLine($">> {item}");
                }

                Inventory.inventory[0].Equip();
                Inventory.inventory[2].Equip();

                Console.WriteLine("\n(2)");
                foreach (var item in Inventory.inventory)
                {
                    Console.WriteLine($">> {item}");
                }

                Console.WriteLine();
                foreach (var gear in Inventory.gear)
                {
                    Console.WriteLine($"-- {gear}");
                }

                Console.ReadKey();
            }

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
