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
        ///<summary>
        /// Allows keyboard input to be listened to in a separate thread.
        ///</summary>
        public KeyboardInput()
        {
            Thread inputThread = new Thread(StartListening);
            inputThread.Start();
        }

        ///<summary>
        ///A delegate for handling a key press event.
        ///</summary>
        public delegate void OnKeyPressed(ConsoleKey key);

        ///<summary>
        ///An event that is triggered when a key is pressed.
        ///</summary>
        static public OnKeyPressed KeyPressed;

        ///<summary>
        ///Listens for keyboard input and triggers the KeyPressed event.
        ///</summary>
        public void StartListening()
        {
            while (GameLoop.Running)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Escape)
                    Environment.Exit(0);

                KeyPressed?.Invoke(input.Key);
            }
        }
	
    }
}
