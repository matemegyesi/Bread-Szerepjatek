using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CSInputs;
using System.Text.Json;

namespace PoP.classes
{
    /// <summary>
    /// Represents the different phases of the game.
    /// </summary>
    enum GamePhase
    {
        /// <summary>
        /// The adventure phase, where the player explores the game world and interacts with NPCs.
        /// </summary>
        ADVENTURE,

        /// <summary>
        /// The combat phase, where the player engages in battles with enemies.
        /// </summary>
        COMBAT,

        /// <summary>
        /// The dialogue phase, where the player engages in conversations with NPCs progress the story.
        /// </summary>
        DIALOGUE
    }
    class GameLoop
    {
        /// <summary>
        /// Gets or sets a value indicating whether the game is currently running.
        /// </summary>
        /// <value><c>true</c> if the game is running; otherwise, <c>false</c>.</value>
        public static bool Running { get; private set; }
        /// <summary>
        /// Gets or sets the current phase of the game.
        /// </summary>
        public static GamePhase Phase { get; set; }

        public static Display display;

        public static KeyboardInput keyboardInput;

        public static Movement playerMovement;

        public static Inventory inventory;

        private Action action;

        public void Start()
        {
            // Sends the F11 key input using the CSInputs library.
            CSInputs.SendInput.Keyboard.Send(CSInputs.Enums.KeyboardKeys.F11);

            // Initializes the Window Renderer
            Wire.Initialize();

            // Initializes objects for the display, keyboard input, and player movement.
            display = new Display();
            Running = true;
            keyboardInput = new KeyboardInput();
            playerMovement = new Movement(127, 27); // 9, 13 or 127, 27 //
            inventory = new Inventory();
            action = new Action();

            // Initializes maps with file paths and adds locations to map3 using the Map class.
            Map map1 = new Map("res\\maps\\Raudagnupur.txt");
            Map map3 = new Map("res\\maps\\volcano.txt");
            //Map map2 = new Map("res\\maps\\map1.txt");
            //Map map4 = new Map("res\\maps\\cave.txt");
            //Map map5 = new Map("res\\maps\\Capital.txt");

            map1.AddLocation(2, LocationType.DIALOGUE, "res\\dialogue\\blacksmith.json");
            map1.AddLocation(1, LocationType.DIALOGUE, "res\\dialogue\\talkwithhighpriest.json");
            map1.AddLocation(3, LocationType.DIALOGUE, "res\\dialogue\\guiscardtalk.json");
            map1.AddLocation(4, LocationType.COMBAT, "res\\enemies\\enemy.json");
            
            //ReadItemFile("res\\itemFile.txt");

            // Initialize character
            Wire.Map.SetCharacterPosition(127, 27); // 9, 13 or 127, 27 //

            // Loads map2.
            Map.maps[0].LoadMap();
            
            // Starts the Update loop
            Update();

            // Sets the game phase to the adventure phase.
            Phase = GamePhase.ADVENTURE;
        }

        /// <summary>
        /// Updates the game state every frame at a rate of 60 FPS.
        /// </summary>
        private void Update()
        {
            DateTime lastTime = DateTime.Now;
            double frameTime = 16.67; // in milliseconds
            double accumulatedTime = 0.0;

            while (Running)
            {
                DateTime currentTime = DateTime.Now;
                double elapsedTime = (currentTime - lastTime).TotalMilliseconds;
                lastTime = currentTime;

                accumulatedTime += elapsedTime;

                // Update as many frames as needed to catch up with accumulated time
                while (accumulatedTime >= frameTime)
                {
                    display.Render();
                    accumulatedTime -= frameTime;
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
                                item = ItemFactory.CreateItem(ItemType.ARMOR, itemT[1], float.Parse(itemT[2]), Slot.Head);
                                break;
                            case "Chest":
                                item = ItemFactory.CreateItem(ItemType.ARMOR, itemT[1], float.Parse(itemT[2]), Slot.Chest);
                                break;
                            case "Leg":
                                item = ItemFactory.CreateItem(ItemType.ARMOR, itemT[1], float.Parse(itemT[2]), Slot.Leg);
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Weapon":
                        switch (itemT[3])
                        {
                            case "Hand":
                                item = ItemFactory.CreateItem(ItemType.WEAPON, itemT[1], float.Parse(itemT[2]), Slot.Hand);
                                break;
                            case "Ring":
                                item = ItemFactory.CreateItem(ItemType.WEAPON, itemT[1], float.Parse(itemT[2]), Slot.Ring);
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

    }
}
