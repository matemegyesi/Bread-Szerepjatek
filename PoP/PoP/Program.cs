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

                Wire.Initialize();
                //

                Dialogue dialogue1 = new Dialogue(2, 53, 24, false, "res\\dialogue\\blacksmith.json");
                Wire.Map.LocationList.Add(dialogue1);
                Dialogue dialogue2 = new Dialogue(1, 75, 8, false, "res\\dialogue\\talkwithhighpriest.json");
                Wire.Map.LocationList.Add(dialogue2);
                Dialogue dialogue3 = new Dialogue(3, 127, 30, false, "res\\dialogue\\guiscardtalk.json");
                Wire.Map.LocationList.Add(dialogue3);
                Combat combat1 = new Combat(4, 127, 30, true, "res\\enemies\\enemy.json");
                combat1.isHidden = true;
                Wire.Map.LocationList.Add(combat1);

                Wire.Map.SetCharacterPosition(16, 16);

                List<Item> itemList = new List<Item>();
                for (int i = 0; i < 20; i++)
                {
                    itemList.Add(new Armor("asd" + i + "asd" + i, Slot.Leg, i * 4.5));
                    itemList.Add(new Weapon("qweqwe" + i + i, Slot.Hand, i * 0.8));

                    switch (i)
                    {
                        case 1:
                            Inventory.gear[Slot.Chest] = new Armor("Mellkasvédő", Slot.Leg, i * 4.5);
                            break;
                        case 2:
                            Inventory.gear[Slot.Ring] = new Armor("Egy Gyűrű", Slot.Leg, i * 8.5);
                            break;
                        case 3:
                            Inventory.gear[Slot.Hand] = new Weapon("Vipera", Slot.Hand, i * 0.5);
                            break;
                    }
                }
                Wire.Inventory.UpdateItemList(itemList);
                Wire.Inventory.PreviousPage();
                Wire.Inventory.ToggleInUse();

                Wire.Map.ImportMap("res\\maps\\Raudagnupur.txt");

                Wire.Dialogue.ProgressDialogue("Name", "Lorem ipsum dolor amet.", ColorAnsi.CORAL);
                Wire.Dialogue.ClearDialogue();
                Wire.Dialogue.ProgressDialogue("Player", "Hi, I'm the player. I wanna learn something fun today!", ColorAnsi.CORAL);
                Wire.Dialogue.ProgressDialogue("...", "Lorentz transformations are a six-parameter family of linear transformations from a coordinate frame in spacetime to another frame that moves at a constant velocity relative to the former. The respective inverse transformation is then parameterized by the negative of this velocity. The transformations are named after the Dutch physicist Hendrik Lorentz.");
                Wire.Dialogue.ProgressDialogue("Player", "God help.", ColorAnsi.CORAL);

                //
                List<string> strList = Wire.Get();
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
