using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoP.classes
{
    class KeyboardInput
    {

        public KeyboardInput()
        {
            Thread inputThread = new Thread(StartListening);
            inputThread.Start();
        }
	
        private static List<string> InputHistory = new List<string>();

        public void StartListening()
        {
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey();
                if (input.Key == ConsoleKey.D)
                {
                    if(Display.content[GameLoop.display.PlayerY][GameLoop.display.PlayerX+1] == ' ')
                    {
                        GameLoop.display.DrawString(" ",GameLoop.display.PlayerX,GameLoop.display.PlayerY);
                        GameLoop.display.PlayerX++;
                        InputHistory.Add(input.ToString());
                        GameLoop.display.DrawCharacter();
                    }
                }
                else if (input.Key == ConsoleKey.A)
                {
                    if (Display.content[GameLoop.display.PlayerY][GameLoop.display.PlayerX - 1] == ' ')
                    {
                        GameLoop.display.DrawString(" ",GameLoop.display.PlayerX,GameLoop.display.PlayerY);
                        GameLoop.display.PlayerX--;
                        InputHistory.Add(input.ToString());
                        GameLoop.display.DrawCharacter();
                    }
                }
                else if (input.Key == ConsoleKey.W)
                {
                    if (Display.content[GameLoop.display.PlayerY - 1][GameLoop.display.PlayerX] == ' ')
                    {
                        GameLoop.display.DrawString(" ",GameLoop.display.PlayerX,GameLoop.display.PlayerY);
                        GameLoop.display.PlayerY--;
                        InputHistory.Add(input.ToString());
                        GameLoop.display.DrawCharacter();
                    }
                }
                else if (input.Key == ConsoleKey.S)
                {
                    if (Display.content[GameLoop.display.PlayerY + 1][GameLoop.display.PlayerX] == ' ')
                    {
                        GameLoop.display.DrawString(" ",GameLoop.display.PlayerX,GameLoop.display.PlayerY);
                        GameLoop.display.PlayerY++;
                        InputHistory.Add(input.ToString());
                        GameLoop.display.DrawCharacter();
                    }
                }
            } while (input.Key != ConsoleKey.X);
        }
	
    }
}
