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
	
        public void StartListening()
        {
            ConsoleKeyInfo input;
            int i = 0;
            do
            {
                input = Console.ReadKey();
                if (input.Key == ConsoleKey.A)
                {
                    GameLoop.display.DrawString(" ",GameLoop.display.PlayerX,GameLoop.display.PlayerY);
                    GameLoop.display.PlayerX++;
                    GameLoop.display.DrawCharacter();
                }
            } while (input.Key != ConsoleKey.X);
        }
	
    }
}
