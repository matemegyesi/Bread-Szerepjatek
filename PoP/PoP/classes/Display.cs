using Ansi;
using System;
using System.Collections.Generic;
using System.Data;
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

        // Player's current position in the game world.
        public int PlayerX = 9;
        public int PlayerY = 13;


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

        public bool drawStringCalled = false;

        /// <summary>
        /// Initializes a new instance of the Display class, setting up the console window and displaying the initial game interface.
        /// </summary>
        public Display()
        {
            // Set the console output encoding and hide the cursor
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Style.EnableStyling();

            // Display the game interface elements
            DrawString("INVENTORY(0)", 212, 1);
            DrawString("STATISTICS", 167, 1);
            DrawString("MAP", 65, 1);
            DrawString("GEAR", 170, 25);

            // Display the initial game content
            Console.Write(GetContent());
        }

        /// <summary>
        /// Renders the game interface to the console window.
        /// </summary>
        public void Render()
        {
            // Set the console cursor position to the top-left corner of the window
            Console.SetCursorPosition(0, 0);

            // If the DrawString method was called since the last rendering
            if (drawStringCalled)
            {
                // Refresh the game interface by printing the current game content
                Console.Write(GetContent());
                drawStringCalled = false;
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
            string line = string.Empty;
            for (int i = 0; i < width; i++)
            {
                line += fill;
            }

            // Draw the fill string at each y-coordinate in the rectangular area
            for (int yCurrent = y; yCurrent < y + height; yCurrent++)
            {
                GameLoop.display.DrawString(line, x, yCurrent);
            }
        }

        /// <summary>
        /// Clears the dialogue text box by filling it with spaces.
        /// </summary>
        public void WipeTextBox()
        {
            // Create a string of spaces with a width of WIDTH - 2 (to account for the textbox borders)
            string line = string.Empty;
            for (int i = 0; i < WIDTH - 2; i++)
            {
                line += ' ';
            }

            // Fill the dialogue text box with the space string, except for the borders
            for (int i = 48; i < 62; i++)
            {
                GameLoop.display.DrawString(line, 1, i);
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

            // Set a flag to indicate that a draw string operation has been called
            drawStringCalled = true;
        }

        /// <summary>
        /// Draws a conversation between two speakers, with the name of the second speaker.
        /// </summary>
        /// <param name="e">The conversation text to display.</param>
        /// <param name="x">The x-coordinate of the starting position for the conversation text.</param>
        /// <param name="y">The y-coordinate of the starting position for the conversation text.</param>
        /// <param name="name">The name of the second speaker.</param>
        /// <param name="person">An integer value indicating the current speaker (0 for player, 1 for other speaker).</param>
        public void DrawConversation(string e, int x, int y, string name, int person)
        {
            // Draw speaker name above conversation text
            if (person++ % 2 == 0)
                GameLoop.display.DrawString("Player", x, y - 2);
            else
                GameLoop.display.DrawString(name, x, y - 2);

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

            // Set the drawStringCalled flag to true
            drawStringCalled = true;
        }

        /// <summary>
        /// Draws/Updates the character's position on the display
        /// </summary>
        public void DrawCharacter()
        {
            DrawString("H", PlayerX, PlayerY);
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
                DrawString(map[mapC], 1, i);
                mapC++;
            }
        }

        ///<summary>
        ///Draws the player's inventory onto the display
        ///</summary>
        public void DrawInventory()
        {
            int i = 3;

            // Iterate through each item in the inventory and draw its name and slot onto the display
            foreach (Item item in Inventory.inventory)
            {
                GameLoop.display.DrawString($"{item.Name} ({item.Slot})", 200, i);
                i += 1;
            }
        }

        /// <summary>
        /// Draws/Updates the player's gear onto the display
        /// </summary>
        public void DrawGear()
        {
            int yStart = 27;
            foreach(KeyValuePair<Slot, Item> kv in Inventory.gear)
            {
                if (kv.Value != null)
                    DrawString($"{kv.Key}: {kv.Value.Name}", 165, yStart);
                else
                    DrawString($"{kv.Key}: Empty", 165, yStart);

                yStart++;
            }
        }

        private string GetContent()
        {
            string result = "";

            try
            {
                foreach (string str in content)
                {
                    result += str;
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
