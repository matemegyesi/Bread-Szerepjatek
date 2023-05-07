using PoP.classes.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Movement
    {
        // Player's current position in the game world.
        public int PlayerX { get; private set; }
        public int PlayerY { get; private set; }

        /// <summary>
        /// The Movement class is responsible for handling the movement of the player character in the game.
        /// </summary>
        public Movement(int startPositionX, int startPositionY)
        {
            // Initializes a new instance of the Movement class and subscribes to the KeyboardInput.KeyPressed event.
            EnableMovement();

            PlayerX = startPositionX;
            PlayerY = startPositionY;
        }
        public void DisableMovement()
        {
            KeyboardInput.KeyPressed -= KeyPressed;
        }
        public void EnableMovement()
        {
            KeyboardInput.KeyPressed += KeyPressed;
        }
        public void KeyPressed(ConsoleKey key)
        {
            // Get the player's current position.
            int x = PlayerX;
            int y = PlayerY;

            // Check which key was pressed and move the player accordingly.
            switch (key)
            {
                case ConsoleKey.D:
                    x++;
                    break;
                case ConsoleKey.A:
                    x--;
                    break;
                case ConsoleKey.W:
                    y--;
                    break;
                case ConsoleKey.S:
                    y++;
                    break;
                default:
                    return;
            }

            // Check if the new position is empty.
            if (MapWindow.Map[y, x].Char != ' ')
            {
                return;
            }

            // Erase the player's current position, move them to the new position, and redraw the player.
            PlayerX = x;
            PlayerY = y;
            WindowRenderer.Map.SetCharacterPosition(x, y);

            // Check if the player is standing on a location and load it if they are.
            var location = Map.CurrentMap.locations.FirstOrDefault(l => l.positionX == x && l.positionY == y && !l.isCompleted);
            location?.LoadLocation();
        }
    }
}
