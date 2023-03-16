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
        public const int WIDTH = 50;
        public const int HEIGHT = 20;

        private char[,] content = new char[HEIGHT,WIDTH];

        public const int contentHeight = 63;
        public const int contentWidth = 237;
        public static List<string> contentList = new List<string>() {
            $"╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╤══════════════════════════════════════════════════════════╗",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║                                                                                                                                                                                │                                                          ║",
            $"║━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╧━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━║",
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

        private bool drawStringCalled = false;

        public Display()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Style.EnableStyling();

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    content[i,j] = '-';
                }
            }

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
        public void DrawString(char[] e, int x, int y)
        {

            for (int h = 0; h < e.Length; h++)
            {
                content[y, x + h] = e.ElementAt(h);
            }


            drawStringCalled = true;
        }

        private string GetContent()
        {
            string result = "";

            foreach (string str in contentList)
            {
                result += str;
            }
            /*for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    result += content[i, j];
                }
                result+= "\n";
            }
            */
            return result;
        }
    }
}
