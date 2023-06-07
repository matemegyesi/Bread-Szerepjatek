using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    internal class Menu
    {
        public Menu()
        {
            KeyboardInput.KeyPressed += OnKeyPressed;
        }

        public void OnKeyPressed(ConsoleKey key)
        {
            ExitMenu();
        }

        private void ExitMenu()
        {
            KeyboardInput.KeyPressed -= OnKeyPressed;

            Wire.BasicWindowSetup();
        }
    }
}
