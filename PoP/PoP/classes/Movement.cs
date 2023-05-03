using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Movement
    {
        /// <summary>
        /// The Movement class is responsible for handling the movement of the player character in the game.
        /// </summary>
        public Movement()
        {
            // Initializes a new instance of the Movement class and subscribes to the KeyboardInput.KeyPressed event.
            EnableMovement();
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
            int x = GameLoop.display.PlayerX;
            int y = GameLoop.display.PlayerY;

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
            if (Display.content[y][x] != ' ')
            {
                return;
            }

            // Erase the player's current position, move them to the new position, and redraw the player.
            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
            GameLoop.display.PlayerX = x;
            GameLoop.display.PlayerY = y;
            GameLoop.display.DrawCharacter();

            // Check if the player is standing on a location and load it if they are.
            var location = Map.CurrentMap.locations.FirstOrDefault(l => l.positionX == x && l.positionY == y && !l.isCompleted);
            location?.LoadLocation();
        }
    }
}
