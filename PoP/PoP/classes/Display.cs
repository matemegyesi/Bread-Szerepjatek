﻿using Ansi;
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
            DrawMap("res\\volcano.txt");
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

        public void DrawCharacter()
        {
            DrawString("H", PlayerX, PlayerY);
            //DrawString($"Playerx : {PlayerX.ToString()}",165,3);
            //DrawString($"Playery : {PlayerY.ToString()}",165,4);
            Render();
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

            foreach (string str in content)
            {
                result += str;
            }
            return result;
        }
    }
}
