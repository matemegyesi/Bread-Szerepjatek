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
        
        public delegate void OnKeyPressed(ConsoleKey key);
        static public OnKeyPressed KeyPressed;

        public void StartListening()
        {
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey();
                #region Movement
                if (GameLoop.Phase == GamePhase.ADVENTURE)
                {
                    if (input.Key == ConsoleKey.D)
                    {
                        if (Display.content[GameLoop.display.PlayerY][GameLoop.display.PlayerX + 1] == ' ')
                        {
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerX++;
                            InputHistory.Add(input.ToString());
                            GameLoop.display.DrawCharacter();
                        }
                    }
                    else if (input.Key == ConsoleKey.A)
                    {
                        if (Display.content[GameLoop.display.PlayerY][GameLoop.display.PlayerX - 1] == ' ')
                        {
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerX--;
                            InputHistory.Add(input.ToString());
                            GameLoop.display.DrawCharacter();
                        }
                    }
                    else if (input.Key == ConsoleKey.W)
                    {
                        if (Display.content[GameLoop.display.PlayerY - 1][GameLoop.display.PlayerX] == ' ')
                        {
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerY--;
                            InputHistory.Add(input.ToString());
                            GameLoop.display.DrawCharacter();
                        }
                    }
                    else if (input.Key == ConsoleKey.S)
                    {
                        if (Display.content[GameLoop.display.PlayerY + 1][GameLoop.display.PlayerX] == ' ')
                        {
                            GameLoop.display.DrawString(" ", GameLoop.display.PlayerX, GameLoop.display.PlayerY);
                            GameLoop.display.PlayerY++;
                            InputHistory.Add(input.ToString());
                            GameLoop.display.DrawCharacter();
                        }
                    }

                    if (Map.CurrentMap.locations.Where(x => x.positionX == GameLoop.display.PlayerX && x.positionY == GameLoop.display.PlayerY && !x.isCompleted).ToList().Count == 1)
                    {
                        Map.CurrentMap.locations.Where(x => x.positionX == GameLoop.display.PlayerX && x.positionY == GameLoop.display.PlayerY && !x.isCompleted).First().LoadLocation();
                    }
                }
                #endregion
                #region Dialogue
                if (GameLoop.Phase == GamePhase.DIALOGUE)
                {
                    if (input.Key == ConsoleKey.Spacebar)
                    {
                        Map.CurrentLocation.IncreaseDialogueIndex();
                    }
                }
                #endregion

                KeyPressed?.Invoke(input.Key);

            } while (input.Key != ConsoleKey.X);
        }
	
    }
}
