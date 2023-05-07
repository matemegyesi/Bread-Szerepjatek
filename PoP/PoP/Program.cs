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
                //

                Dialogue dialogue1 = new Dialogue(2, 53, 24, "res\\dialogue\\blacksmith.json");
                WindowRenderer.Map.LocationList.Add(dialogue1);
                Dialogue dialogue2 = new Dialogue(1, 75, 8, "res\\dialogue\\talkwithhighpriest.json");
                WindowRenderer.Map.LocationList.Add(dialogue2);
                Dialogue dialogue3 = new Dialogue(3, 127, 30, "res\\dialogue\\guiscardtalk.json");
                WindowRenderer.Map.LocationList.Add(dialogue3);
                Combat combat1 = new Combat(4, 127, 30, "res\\enemies\\enemy.json");
                combat1.isHidden = true;
                WindowRenderer.Map.LocationList.Add(combat1);

                WindowRenderer.Map.MoveCharacterMarker(16, 16);
                


                WindowRenderer.Map.ImportMap("res\\maps\\Raudagnupur.txt");


                //
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
