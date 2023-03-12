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

        private bool drawStringCalled = false;

        public Display()
        {
            Console.CursorVisible = false;

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
        public void drawString(char[] e, int x, int y)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (i == y && j == x)
                    {
                        for (int h = 0; h < e.Length; h++)
                        {
                            content[i, j+h] = e.ElementAt(h);
                        }
                    }
                }
            }

            drawStringCalled = true;
        }

        private string GetContent()
        {
            string result = "";

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    result += content[i, j];
                }
                result+= "\n";
            }

            return result;
        }
    }
}
