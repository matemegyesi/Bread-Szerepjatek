using Ansi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Display
    {
        // Constants used for defining the size of the game display and map.
        public const int HEIGHT = 63;
        public const int WIDTH = 237;

        public const int MAPWIDTH = 148;
        public const int MAPHEIGHT = 46;


        public static List<string> content = new List<string>() {
            $"╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╤════════════════════════════════════════════╤═════════════════════════════════════════╗",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    ├━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┤                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║                                                                                                                                                    │                                            │                                         ║",
            $"║━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╧━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╧━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"║                                                                                                                                                                                                                                           ║",
            $"╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝",
        };

        public static Dictionary<int, string> keys = new Dictionary<int, string>() {
            {65,"A"},
            {66,"B"},
            {67,"C"},
            {68,"D"},
            {69,"E"},
            {70,"F"},
            {71,"G"},
            {72,"H"},
            {73,"I"},
            {74,"J"},
            {75,"K"},
            {76,"L"},
            {77,"M"},
            {78,"N"},
            {79,"O"},
            {80,"P"},
            {81,"Q"},
            {82,"R"},
            {83,"S"},
            {84,"T"},
            {85,"U"},
            {86,"V"},
            {87,"W"},
            {88,"X"},
            {89,"Y"},
            {90,"Z"},
            {49,"1"},
            {50,"2"},
            {51,"3"},
            {52,"4"},
            {53,"5"},
            {54,"6"},
            {55,"7"},
            {56,"8"},
            {57,"9"},
            {112,"F1"},
            {113,"F2"},
            {114,"F3"},
            {115,"F4"},
            {116,"F5"}
        };
        public static Dictionary<int, string> spellKeys = new Dictionary<int, string>()
        {
            { 81, "Q" },
            { 87, "W" },
            { 69, "E" },
            { 82, "R" }
        };

        /// <summary>
        /// Initializes a new instance of the Display class, setting up the console window and displaying the initial game interface.
        /// </summary>
        public Display()
        {
            // Set the console output encoding and hide the cursor
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;

            // Enabling the recognition of ANSI escape codes
            Style.EnableStyling();

            // Display the initial game content
            //Console.Write(GetContent());
        }

        /// <summary>
        /// Renders the game interface to the console window.
        /// </summary>
        public void Render()
        {
            // Set the console cursor position to the top-left corner of the window
            Console.SetCursorPosition(0, 0);

            // If the DrawString method was called since the last rendering
            if (Wire.NeedUpdate)
            {
                // Refresh the game interface by printing the current game content
                Console.Write(GetContent());
            }
        }

        /// <summary>
        /// Clears a rectangular area of the console window by filling it with a specified character.
        /// </summary>
        /// <param name="x">The x-coordinate of the area.</param>
        /// <param name="y">The y-coordinate of the area.</param>
        /// <param name="height">The height of the rectangular area.</param>
        /// <param name="width">The width of the rectangular area.</param>
        /// <param name="fill">The character to fill the rectangular area with.</param>
        public void WipeStringBox(int x, int y, int height, int width, char fill = ' ')
        {
            // Create a string of the specified fill character with the same width as the rectangular area
            string line = new(fill, width);

            // Draw the fill string at each y-coordinate in the rectangular area
            for (int yCurrent = y; yCurrent < y + height; yCurrent++)
            {
                //GameLoop.display.DrawString(line, x, yCurrent);
            }
        }

        /// <summary>
        /// Clears the dialogue text box by filling it with spaces.
        /// </summary>
        public void WipeTextBox()
        {
            // Create a string of spaces with a width of WIDTH - 2 (to account for the textbox borders)
            string line = new(' ', WIDTH-2);

            // Fill the dialogue text box with the space string, except for the borders
            for (int i = 48; i < 62; i++)
            {
                //GameLoop.display.DrawString(line, 1, i);
            }
        }

        /// <summary>
        /// Replaces the characters at the specified position in the display buffer with the characters of the given string.
        /// </summary>
        /// <param name="e">The string to draw.</param>
        /// <param name="x">The x-coordinate of the starting position.</param>
        /// <param name="y">The y-coordinate of the starting position.</param>
        public void DrawString(string e, int x, int y)
        {
            // Convert the string at the specified y-coordinate to a character array
            char[] characters = content[y].ToCharArray();

            // Replace the characters in the array with the characters from the given string
            for (int i = 0; i < e.Length; i++)
            {

                // Check if the current coordinates are within the bounds of the display buffer
                if (y >= 0 && y < content.Count() && x + i < content[y].Length && x + i > 0)
                {
                    characters[x + i] = e[i];
                    content[y] = new string(characters);
                }
            }
        }

        /// <summary>
        /// Draws a conversation between two speakers, with the name of the second speaker.
        /// </summary>
        /// <param name="e">The conversation text to display.</param>
        /// <param name="x">The x-coordinate of the starting position for the conversation text.</param>
        /// <param name="y">The y-coordinate of the starting position for the conversation text.</param>
        /// <param name="name">The name of the second speaker.</param>
        /// <param name="person">An integer value indicating the current speaker (0 for player, 1 for other speaker).</param>
        public void DrawConversation(string e, int x, int y, string name)
        {
            // Draw speaker name above conversation text
            //DrawString(name, x, y - 2);

            // Add "(Next: SPACE)" to the end of the conversation text
            e += " (Next: SPACE)";

            // Iterate through each character in the conversation text
            for (int i = 0; i < e.Length; i++)
            {
                // If the x-coordinate is beyond the width of the screen, move to the next line
                if (x + i >= content[y].Length - 1)
                {
                    y += 1;
                    x -= i + 1;
                }
                // Otherwise, add the current character to the string at the current position
                else
                {
                    char[] chars = content[y].ToCharArray();
                    chars[x + i] = e[i];
                    content[y] = new string(chars);
                }
            }
        }

        /// <summary>
        /// Draws/Updates the character's position on the display
        /// </summary>
        public void DrawCharacter()
        {
            //DrawString("H", PlayerX, PlayerY);
        }

        ///<summary>
        ///Draws the map from the given file onto the display
        ///</summary>
        ///<param name="file">The file containing the map data</param>
        public void DrawMap(string file)
        {

            // Read the file and store each line as a string in an array
            string[] map = FileInput.GetAllLines(file);

            int mapC = 0;

            // Draw each line of the map onto the display
            for (int i = 1; i < MAPHEIGHT + 1; i++)
            {
                //DrawString(map[mapC], 1, i);
                mapC++;
            }
        }

        ///<summary>
        ///Draws the player's inventory onto the display
        ///</summary>
        public void DrawInventory()
        {
            //GameLoop.display.DrawString("Use: F12 then value next to item", 200, 2);
            //Reset inventory box
            //WipeStringBox(WIDTH-42, 3, MAPHEIGHT-2, 40);

            //Update inventory count
            //DrawString($"INVENTORY({Inventory.inventory.Count})", 212, 1);

            int y = 3;
            int counter = 0;
            // Iterate through each item in the inventory and draw its name and slot onto the display
            foreach (Item item in Inventory.inventory)
            {
                //GameLoop.display.DrawString($"{item.Name} ({item.Slot}) - {keys.ElementAt(counter).Value}", 200, y);
                y += 1;
                counter++;
            }
        }

        /// <summary>
        /// Draws/Updates the player's gear onto the display
        /// </summary>
        public void DrawGear()
        {
            //WipeStringBox(150, 27, 20, 44);
            int yStart = 27;
            foreach(KeyValuePair<Slot, Item> kv in Inventory.gear)
            {
                if (kv.Value != null)
                {
                    if (kv.Value.GetType().Name == "Armor")
                    {
                        Armor item = kv.Value as Armor;
                        //DrawString($"{kv.Key}: {item.Name}", 153, yStart);
                        //DrawString($"Defense: {item.Defense}", 180, yStart);
                    }
                    else
                    {
                        Weapon item = kv.Value as Weapon;
                        //DrawString($"{kv.Key}: {kv.Value.Name}", 153, yStart);
                        //DrawString($"Damage: {item.Damage}", 180, yStart);
                    }

                }
                else
                    //DrawString($"{kv.Key}: Empty", 153, yStart);

                yStart++;
            }
        }

        private string GetContent()
        {
            string result = "";

            try
            {
                foreach (string line in Wire.Get())
                {
                    result += line;
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
