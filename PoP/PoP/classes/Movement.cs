using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Movement
    {
        public Movement() 
        {
            KeyboardInput.KeyPressed += KeyPressed;    
        }
        public void KeyPressed(ConsoleKey key)
        {
            if (GameLoop.Phase == GamePhase.ADVENTURE)
            {
                switch (key)
                {
                    case ConsoleKey.D:
                        if (Display.content[GameLoop.display.PlayerY][GameLoop.display.PlayerX + 1] == ' ')
                        {
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerX++;
                            GameLoop.display.DrawCharacter();
                        }
                        break;
                    case ConsoleKey.A:
                        if (Display.content[GameLoop.display.PlayerY][GameLoop.display.PlayerX - 1] == ' ')
                        {
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerX--;
                            GameLoop.display.DrawCharacter();
                        }
                        break;
                    case ConsoleKey.W:
                        if (Display.content[GameLoop.display.PlayerY - 1][GameLoop.display.PlayerX] == ' ')
                        {
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerY--;
                            GameLoop.display.DrawCharacter();
                        }
                        break;
                    case ConsoleKey.S:
                        if (Display.content[GameLoop.display.PlayerY + 1][GameLoop.display.PlayerX] == ' ')
                        {
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerY++;
                            GameLoop.display.DrawCharacter();
                        }
                        break;
                    default:
                        break;
                }
                if (Map.CurrentMap.locations.Where(x => x.positionX == GameLoop.display.PlayerX && x.positionY == GameLoop.display.PlayerY && !x.isCompleted).ToList().Count == 1)
                {
                    Map.CurrentMap.locations.Where(x => x.positionX == GameLoop.display.PlayerX && x.positionY == GameLoop.display.PlayerY && !x.isCompleted).First().LoadLocation();
                }
            }
        }
    }
}
