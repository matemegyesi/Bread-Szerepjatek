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
        
        public delegate void OnKeyPressed(ConsoleKey key);
        static public OnKeyPressed KeyPressed;

        public void StartListening()
        {
            while (GameLoop.Running)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                KeyPressed?.Invoke(input.Key);
            }
        }
	
    }
}
