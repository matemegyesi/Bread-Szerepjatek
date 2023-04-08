using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    

namespace PoP.classes
{
    enum CombatPhase
    {
        LOADOUT,
        PLAYER_TURN,
        PLAYER_WIN,
        ENEMY_TURN,
        ENEMY_WIN
    }

    class Combat : Location
    {
        CombatPhase combatPhase = CombatPhase.LOADOUT;
        Enemy enemy;

        public Combat(int id, int x, int y, string enemyFile) : base(id, x, y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            
        }

        public override void IncreaseDialogueIndex()
        {
            //not dialogue
        }

        public override void LoadLocation()
        {
            Map.CurrentLocation = this;
            GameLoop.Phase = GamePhase.COMBAT;
            GameLoop.display.DrawString("loc", 10, 10);

            Start();
        }

        public override void Start()
        {

            KeyboardInput.KeyPressed += KeyPressed;

            GameLoop.display.DrawString("Loadout kiválasztása", 4, 50);
            GameLoop.display.DrawString("Csata megkezdése (SPACE)", 4, 51);

        }

        public override void End()
        {
            KeyboardInput.KeyPressed -= KeyPressed;
        }

        public void ChangeCombatPhase(CombatPhase newPhase)
        {
            combatPhase = newPhase;
            GameLoop.display.WipeStringBox(1, 48, 14, Display.WIDTH - 2);

            switch (combatPhase)
            {
                case CombatPhase.LOADOUT:

                    //GameLoop.display.DrawString("Loadout kiválasztása", 4, 50);
                    //GameLoop.display.DrawString("Csata megkezdése (SPACE)", 4, 51);

                    break;

                case CombatPhase.PLAYER_TURN:

                    GameLoop.display.DrawString("Játékos köre", 4, 50);
                    GameLoop.display.DrawString("Fegyverrel támadás (Q)", 4, 51);
                    GameLoop.display.DrawString("Spell 1 használata (W)", 4, 52);
                    GameLoop.display.DrawString("Spell 2 használata (E)", 4, 53);
                    GameLoop.display.DrawString("Spell 3 használata (R)", 4, 54);

                    break;

                case CombatPhase.ENEMY_TURN:

                    //

                    break;

                case CombatPhase.PLAYER_WIN:

                    //

                    End();

                    break;

                case CombatPhase.ENEMY_WIN:

                    //

                    End();

                    break;
            }
        }

        public void KeyPressed(ConsoleKey key)
        {
            
            switch (combatPhase)
            {
                case CombatPhase.LOADOUT:

                    if (key == ConsoleKey.Spacebar)
                    {
                        ChangeCombatPhase(CombatPhase.PLAYER_TURN);
                    }

                    break;

                case CombatPhase.PLAYER_TURN:

                    switch (key)
                    {
                        case ConsoleKey.Q:

                            ChangeCombatPhase(CombatPhase.ENEMY_TURN);

                            break;
                        case ConsoleKey.W:

                            ChangeCombatPhase(CombatPhase.ENEMY_TURN);

                            break;
                        case ConsoleKey.E:

                            ChangeCombatPhase(CombatPhase.ENEMY_TURN);

                            break;
                        case ConsoleKey.R:

                            ChangeCombatPhase(CombatPhase.ENEMY_TURN);

                            break;
                    }

                    break;

                case CombatPhase.ENEMY_TURN:

                    //

                    break;

                case CombatPhase.PLAYER_WIN:

                    //

                    break;

                case CombatPhase.ENEMY_WIN:

                    //

                    break;
            }
            
        }

    }
}
