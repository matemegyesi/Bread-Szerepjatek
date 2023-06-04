﻿using System;
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

            // Main menu & tutorial
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;

            if (true)
            {
                List<string> _menuLines = new List<string>();

                string[] _logo = FileInput.GetAllLines("res\\pop_logo.txt");
                for (int i = 0; i < _logo.Length; i++)
                {
                    _logo[i] = Style.GetBlankLine(77) + Style.Color(_logo[i], ColorAnsi.RED);
                }

                _menuLines.AddRange(new List<string>() { string.Empty, string.Empty, string.Empty });

                string[] _tutorial = FileInput.GetAllLines("res\\tutorial.txt");
                for (int i = 0; i < _tutorial.Length; i++)
                {
                    if (_tutorial[i].Contains('━'))
                    {
                        string[] _info = _tutorial[i].Split(' ');

                        _tutorial[i] = Style.GetBlankLine(90) + Style.Color(_info[0], ColorAnsi.CORAL) + ' ' + Style.ColorFormat(_info[1], ColorAnsi.CORAL, FormatAnsi.UNDERLINE) + ' ' + Style.Color(_info[2], ColorAnsi.CORAL);
                    }
                    else if (_tutorial[i].Contains(':'))
                    {
                        string[] _info = _tutorial[i].Split(':');

                        _tutorial[i] = Style.GetBlankLine(90) + Style.Color(_info[0] + ':', ColorAnsi.LIGHT_BLUE) + Style.Color(_info[1], ColorAnsi.WHITE);
                    }
                    else if (_tutorial[i].Contains('└'))
                    {
                        _tutorial[i] = Style.GetBlankLine(90) + Style.Color('└', ColorAnsi.LIGHT_BLUE) + Style.Color(_tutorial[i].Substring(1), ColorAnsi.WHITE);
                    }
                    else
                    {
                        _tutorial[i] = _tutorial[i];
                    }
                    
                }

                _menuLines.AddRange(_logo);
                _menuLines.AddRange(new List<string>() { string.Empty, string.Empty, string.Empty } );
                _menuLines.AddRange(_tutorial);

                _menuLines.AddRange(new List<string>() { string.Empty, string.Empty, string.Empty });
                string _continue = "Press any key to continue";
                _menuLines.Add(Style.GetRemainingSpace(_continue.Length / 2, Wire.Width / 2) + _continue);

                Console.WriteLine(String.Join("\n", _menuLines));
            }

            Console.ReadKey();
            Console.Clear();

            // Initializes objects for the display, keyboard input, and player movement.
            display = new Display();
            Running = true;
            keyboardInput = new KeyboardInput();
            playerMovement = new Movement(127, 27); // 9, 13 or 127, 27 //
            inventory = new Inventory();
            action = new Action();

            // Initializes maps with file paths and adds locations to map3 using the Map class.
            Map map1 = new Map("res\\maps\\raudagnupur_map.json", 0);
            Map map3 = new Map("res\\maps\\ermir_map.json", 1);
            //Map map2 = new Map("res\\maps\\map1.txt");
            //Map map4 = new Map("res\\maps\\cave.txt");
            //Map map5 = new Map("res\\maps\\Capital.txt");

            //ReadItemFile("res\\itemFile.txt");

            // Initialize character
            Player.Init();
            Wire.Map.SetCharacterPosition(127, 27); // 9, 13 or 127, 27 //

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
