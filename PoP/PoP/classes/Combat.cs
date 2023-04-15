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
        ENEMY_TURN,
        LOSE,
        WIN
    }

    class Combat : Location
    {
        public CombatPhase combatPhase;

        Enemy enemy = new Enemy(); // kell majd rendes beolvasás

        public Combat(int id, int x, int y, string path) : base(id, x, y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            Path = path;
        }

        public override void LoadLocation()
        {
            Map.CurrentLocation = this;
            GameLoop.Phase = GamePhase.COMBAT;

            Start();
        }

        public override void Start()
        {

            KeyboardInput.KeyPressed += KeyPressed;

            ChangeCombatPhase(CombatPhase.LOADOUT);

        }

        public override void End()
        {
            KeyboardInput.KeyPressed -= KeyPressed;

            isCompleted = true;
            GameLoop.Phase = GamePhase.ADVENTURE;
        }

        public void ChangeCombatPhase(CombatPhase newPhase)
        {
            combatPhase = newPhase;
            GameLoop.display.WipeTextBox();

            switch (combatPhase)
            {
                case CombatPhase.LOADOUT:

                    GameLoop.display.DrawString("Loadout kiválasztása", 4, 50);
                    GameLoop.display.DrawString("Csata megkezdése (SPACE)", 4, 51);

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

                case CombatPhase.WIN:

                    GameLoop.display.DrawString("Játékos megnyerte!", 4, 50);

                    End();

                    break;

                case CombatPhase.LOSE:

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

                            Player.AttackWithWeapon(enemy);
                            //ChangeCombatPhase(CombatPhase.ENEMY_TURN);
                            ChangeCombatPhase(CombatPhase.WIN);

                            break;
                        case ConsoleKey.W:

                            Player.AttackWithSpell(enemy);
                            ChangeCombatPhase(CombatPhase.ENEMY_TURN);

                            break;
                        case ConsoleKey.E:

                            Player.AttackWithSpell(enemy);
                            ChangeCombatPhase(CombatPhase.ENEMY_TURN);

                            break;
                        case ConsoleKey.R:

                            Player.AttackWithSpell(enemy);
                            ChangeCombatPhase(CombatPhase.ENEMY_TURN);

                            break;
                    }

                    break;

                case CombatPhase.ENEMY_TURN:

                    //

                    break;

                case CombatPhase.WIN:

                    //

                    break;

                case CombatPhase.LOSE:

                    //

                    break;
            }
            
        }

    }
}
