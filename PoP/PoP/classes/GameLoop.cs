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
        public static Menu menu;

        private Action action;

        public void Start()
        {

            // Sends the F11 key input using the CSInputs library.
            CSInputs.SendInput.Keyboard.Send(CSInputs.Enums.KeyboardKeys.F11);

            // Initializes the Window Renderer & the Display
            Wire.Initialize();
            display = new Display();

            // Main menu & tutorial
            if (true)
            {
                Wire.Menu.HasChanged = true;
            }

            // Initializes objects for the keyboard input, the player movement, and the inventory.
            Running = true;
            keyboardInput = new KeyboardInput();
            playerMovement = new Movement(9, 13); // 9, 13 or 127, 27 //
            inventory = new Inventory();
            action = new Action();
            menu = new Menu();

            // Initializes maps with file paths and adds locations to map3 using the Map class.
            Map map1 = new Map("res\\maps\\raudagnupur_map.json", 0);
            Map map3 = new Map("res\\maps\\ermir_map.json", 1);
            //Map map2 = new Map("res\\maps\\map1.txt");
            //Map map4 = new Map("res\\maps\\cave.txt");
            //Map map5 = new Map("res\\maps\\Capital.txt");

            //ReadItemFile("res\\itemFile.txt");

            // Initialize character
            Player.Init();
            Wire.Map.SetCharacterPosition(9, 13); // 9, 13 or 127, 27 //

            Inventory.spellList[8].Equip(2);
            Inventory.spellList[1].Equip(1);
            Inventory.spellList[0].Equip(0);
            Wire.Sorcery.UpdateSpellList(Inventory.spellList);

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
    }
}
