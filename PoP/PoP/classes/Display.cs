using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Display
    {
        public const int WIDTH = 100;
        public const int HEIGHT = 100;

        public string[,] content = new string[WIDTH,HEIGHT];

        public Display()
        {
            Console.CursorVisible = false;
        }

        private void render()
        {
            Console.SetCursorPosition(0, 0);

            Console.Write(GetContent());
        }
        private void drawString(string e, int x, int y)
        {
            int count = 0;
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (i == y && j == x)
                    {
                        content[i, j] = e[++count].ToString();
                    }
                }
            }
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
