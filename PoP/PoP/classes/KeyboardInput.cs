using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class KeyboardInput
    {

        public KeyboardInput()
        {

            ConsoleKeyInfo input;
            input = Console.ReadKey();
            do
            {
            if (input.Key == ConsoleKey.R)
            {
                GameLoop.display.DrawString("alma",13,13) ;
            }

            } while (input.Key != ConsoleKey.X);

        }
	

	
    }
}
