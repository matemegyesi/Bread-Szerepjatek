using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PoP.classes
{
    class GameLoop
    {
        /// <summary>
        /// Képernyő frissítés állapota
        /// </summary>
        public bool Running { get; private set; }

        public static Display display;

        public static KeyboardInput kI;

        public void Start()
        {
            //Maximize();
            display = new Display();

            Running = true;

            ReadItemFile("res\\itemFile.txt");

            
            
            int c = 3;
            foreach (Item item in Inventory.inventory)
            {
                display.DrawString($"{item.Name} ({item.slot})", 200, c);
                c++;
            }

            kI = new KeyboardInput();

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
                }
            }
        }

        public void ReadItemFile(string filePath)
        {
            foreach (string str in FileInput.GetAllLinesAsList(filePath))
            {

                Item item = null;
                string[] itemT = str.Split(';');
                switch (itemT[0])
                {
                    case "Armor":
                        switch (itemT[3])
                        {
                            case "Head":
                                item = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Slot.HEAD);
                                break;
                            case "Chest":
                                item = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Slot.CHEST);
                                break;
                            case "Leg":
                                item = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Slot.LEG);
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Weapon":
                        switch (itemT[3])
                        {
                            case "Hand":
                                item = ItemFactory.CreateItem(ItemType.Weapon, itemT[1], float.Parse(itemT[2]), Slot.HAND);
                                break;
                            case "Ring":
                                item = ItemFactory.CreateItem(ItemType.Weapon, itemT[1], float.Parse(itemT[2]), Slot.RING);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                item.Collect();
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }
    }
}
