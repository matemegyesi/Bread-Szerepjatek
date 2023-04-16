using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CSInputs;

namespace PoP.classes
{
    enum GamePhase
    {
        ADVENTURE,
        COMBAT,
        DIALOGUE
    }
    class GameLoop
    {
        public static bool Running { get; private set; }
        public static GamePhase Phase { get; set; }

        public static Display display;

        public static KeyboardInput keyboardInput;

        private Movement playerMovement;

        public void Start()
        {
            //Maximalizálja F11-el
            CSInputs.SendInput.Keyboard.Send(CSInputs.Enums.KeyboardKeys.F11);
            
            display = new Display();
            Running = true;
            keyboardInput = new KeyboardInput();
            playerMovement = new Movement();

            Map map1 = new Map("res\\map.txt");
            Map map2 = new Map("res\\map1.txt");
            Map map3 = new Map("res\\volcano.txt");
            Map map4 = new Map("res\\cave.txt");
            map3.AddLocation(1, 2, 2, LocationType.DIALOGUE, "res\\dialogue\\talkwithhighpriest.json");
            map3.AddLocation(2, 2, 12, LocationType.COMBAT, "");
            
            ReadItemFile("res\\itemFile.txt");
            display.DrawInventory();

            Map.maps[2].LoadMap();
            Update();
            Phase = GamePhase.ADVENTURE;
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
