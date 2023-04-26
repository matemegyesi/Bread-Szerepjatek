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
            KeyboardInput.KeyPressed += KeyPressed;    
        }
        public void KeyPressed(ConsoleKey key)
        {
            // Check if the game is currently in the adventure phase
            if (GameLoop.Phase == GamePhase.ADVENTURE)
            {
                //Wipe text box and display options
                GameLoop.display.WipeTextBox();
                GameLoop.display.DrawString("Open Inventory: I", 5, 50);
                // Check which key was pressed and move the player accordingly
                switch (key)
                {
                    case ConsoleKey.D:
                        // Check if the tile to the right of the player is empty
                        if (Display.content[GameLoop.display.PlayerY][GameLoop.display.PlayerX + 1] == ' ')
                        {
                            // Erase the player's current position, move them one tile to the right, and redraw the player
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerX++;
                            GameLoop.display.DrawCharacter();
                        }
                        break;
                    case ConsoleKey.A:
                        // Check if the tile to the left of the player is empty
                        if (Display.content[GameLoop.display.PlayerY][GameLoop.display.PlayerX - 1] == ' ')
                        {
                            // Erase the player's current position, move them one tile to the left, and redraw the player
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerX--;
                            GameLoop.display.DrawCharacter();
                        }
                        break;
                    case ConsoleKey.W:
                        // Check if the tile above the player is empty
                        if (Display.content[GameLoop.display.PlayerY - 1][GameLoop.display.PlayerX] == ' ')
                        {
                            // Erase the player's current position, move them one tile up, and redraw the player
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerY--;
                            GameLoop.display.DrawCharacter();
                        }
                        break;
                    case ConsoleKey.S:
                        // Check if the tile below the player is empty
                        if (Display.content[GameLoop.display.PlayerY + 1][GameLoop.display.PlayerX] == ' ')
                        {
                            // Erase the player's current position, move them one tile down, and redraw the player
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerY++;
                            GameLoop.display.DrawCharacter();
                        }
                        break;
                    default:
                        break;
                }

                // Check if the player is standing on a location and load it if they are
                if (Map.CurrentMap.locations.Where(x => x.positionX == GameLoop.display.PlayerX && x.positionY == GameLoop.display.PlayerY && !x.isCompleted).ToList().Count == 1)
                {
                    Map.CurrentMap.locations.Where(x => x.positionX == GameLoop.display.PlayerX && x.positionY == GameLoop.display.PlayerY && !x.isCompleted).First().LoadLocation();
                }
            }
        }
    }
}
