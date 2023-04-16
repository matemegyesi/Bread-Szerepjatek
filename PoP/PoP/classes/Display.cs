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
        public const int HEIGHT = 63;
        public const int WIDTH = 237;

        public const int MAPWIDTH = 148;
        public const int MAPHEIGHT = 46;

        public int PlayerX = 10;   
        public int PlayerY = 10;


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

        public Display()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Style.EnableStyling();

            DrawString("INVENTORY(0)", 212, 1);
            DrawString("STATISTICS", 167, 1);
            DrawString("MAP", 65, 1);
            //DrawMap("res\\volcano.txt");
            DrawCharacter();
            
            Console.Write(GetContent());
        }

        public void Render()
        {
            Console.SetCursorPosition(0, 0);

            if (drawStringCalled)
            {
                Console.Write(GetContent());
                drawStringCalled = false;
            }
        }

        public void WipeStringBox(int x, int y, int height, int width, char fill = ' ')
        {
            string line = string.Empty;
            for (int i = 0; i < width; i++)
            {
                line += fill;
            }

            for (int yCurrent = y; yCurrent < y + height; yCurrent++)
            {
                GameLoop.display.DrawString(line, x, yCurrent);
            }
        }

        public void WipeTextBox()
        {
            string line = string.Empty;
            for (int i = 0; i < WIDTH-2; i++)
            {
                line += ' ';
            }
            for (int i = 48; i < 62; i++)
            {
                GameLoop.display.DrawString(line, 1, i);
            }
        }

        public void DrawString(string e, int x, int y)
        {
            char[] characters = content[y].ToCharArray();
            for (int i = 0; i < e.Length; i++)
            {
                if (y >= 0 && y < content.Count() && x + i < content[y].Length && x + i > 0)
                {
                    characters[x + i] = e[i];
                    content[y] = new string(characters);
                }
            }

            drawStringCalled = true;
        }

        public void DrawConversation(string e, int x, int y)
        {
            for (int i = 0; i < e.Length; i++)
            {
                if(x+i >= content[y].Length-1)
                {
                    y += 1;
                    x -= i+1;
                }
                else
                {
                    char[] chars = content[y].ToCharArray();
                    chars[x + i] = e[i];
                    content[y] = new string(chars);
                }
            }
            drawStringCalled = true;
        }
        public void DrawCharacter()
        {
            DrawString("H", PlayerX, PlayerY);
        }

        public void DrawMap(string file)
        {
            string[] map = FileInput.GetAllLines(file);

            int mapC = 0;

            for (int i = 1; i < MAPHEIGHT+1; i++)
            {
                DrawString(map[mapC], 1, i);
                mapC++;
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
