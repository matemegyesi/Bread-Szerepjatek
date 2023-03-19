using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PoP.classes
{
    class GameLoop
    {
        /// <summary>
        /// Képernyő frissítés állapota
        /// </summary>
        public bool Running { get; private set; }

        Display display;

        public void Start()
        {
            display = new Display();

            Running = true;

            ReadItemFile("res\\itemFile.txt");
            display.DrawString("INVENTORY", 203, 2);
            /*
            string[] itemT = FileInput.GetAllLinesAsList("res\\itemFile.txt")[0].Split(';');
            Item a1 = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Inventory.Slot.HEAD);
            display.DrawString(a1.ToString(), 20, 30);*/

            Update();

        }

        private void Update()
        {
            DateTime lastTime = DateTime.Now;

            while (Running)
            {
                DateTime currentTime = DateTime.Now;
                double elapsedTime = (currentTime - lastTime).TotalMilliseconds;

                if (elapsedTime >= 33.33)
                {
                    lastTime = currentTime;

                    display.Render();
                    int c = 5;
					foreach (Item item in Inventory.inventory)
					{
                        display.DrawString($"{item.Name} ({item.slot})", 200, c);
                        c++;
					}
                    /*Item w1 = ItemFactory.CreateItem(ItemType.Weapon, "Kard", 10);
                    Item w2 = ItemFactory.CreateItem(ItemType.Weapon, "Lándzsa", 20);
                    Item a1 = ItemFactory.CreateItem(ItemType.Armor, "Sisak", 5);

                    w1.Collect();
                    w2.Collect();
                    a1.Collect();

                    display.drawString(w1.ToString().ToCharArray(), 10, 10);*/
                }
            }
        }

        public void ReadItemFile(string filePath)
        {
            foreach (string item in FileInput.GetAllLinesAsList(filePath))
            {

                Item a1 = null;
                string[] itemT = item.Split(';');
                switch (itemT[0])
                {
                    case "Armor":
                        switch (itemT[3])
                        {
                            case "Head":
                                a1 = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Inventory.Slot.HEAD);
                                break;
                            case "Chest":
                                a1 = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Inventory.Slot.CHEST);
                                break;
                            case "Leg":
                                a1 = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Inventory.Slot.LEG);
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Weapon":
                        switch (itemT[3])
                        {
                            case "Hand":
                                a1 = ItemFactory.CreateItem(ItemType.Weapon, itemT[1], float.Parse(itemT[2]), Inventory.Slot.HAND);
                                break;
                            case "Ring":
                                a1 = ItemFactory.CreateItem(ItemType.Weapon, itemT[1], float.Parse(itemT[2]), Inventory.Slot.RING);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                a1.Collect();
            }
        }
    }
}
