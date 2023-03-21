using Ansi;
using System;
using System.Collections.Generic;
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

            DrawString($"INVENTORY({Inventory.inventory.Count})", 212, 1);
            DrawString("STATISTICS", 167, 1);
            DrawString("MAP", 65, 1);

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
        public void DrawString(string e, int x, int y)
        {
            char[] characters = content[y].ToCharArray();
            for (int h = 0; h < e.Length; h++)
            {
                if (y >= 0 && y < content.Count() && x + h < content[y].Length && x + h > 0)
                {
                    characters[x + h] = e[h];
                    content[y] = new string(characters);
                }
            }

            drawStringCalled = true;
        }

        private string GetContent()
        {
            string result = "";

            foreach (string str in content)
            {
                result += str;
            }
            return result;
        }
    }
}
