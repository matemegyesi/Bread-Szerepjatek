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
