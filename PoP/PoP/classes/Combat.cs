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

        Enemy enemy; // kell majd rendes beolvasás

        public Combat(int id, int x, int y, string path) : base(id, x, y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            Path = path;

            enemy = new Enemy(/* Path majd */);
        }

        public override void LoadLocation()
        {
            GameLoop.display.WipeTextBox();
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

        private List<string> GetPlayerInfo()
        {
            List<string> playerInfo = new List<string>();

            playerInfo.Add("Player's name");
            playerInfo.Add("");
            playerInfo.Add("Health: " + Player.Health);
            playerInfo.Add("Damage: " + Player.Damage);
            playerInfo.Add("Defence: " + Player.Defence);
            playerInfo.Add("Mana: " + Player.Mana);
            // playerInfo.Add("Effektek: " + String.Join(", ", Player.Effects vagy ami a neve lesz));

            return playerInfo;
        }

        private List<string> GetEnemyInfo()
        {
            List<string> enemyInfo = new List<string>();

            enemyInfo.Add("Enemy's name");
            enemyInfo.Add("");
            enemyInfo.Add("Health: " + enemy.Health);
            enemyInfo.Add("Damage: " + enemy.Damage);
            enemyInfo.Add("Defence: " + enemy.Defence);
            // playerInfo.Add("Effektek: " + String.Join(", ", enemy.Effects vagy ami a neve lesz));

            return enemyInfo;
        }

        public void ChangeCombatPhase(CombatPhase newPhase)
        {
            combatPhase = newPhase;
            GameLoop.display.WipeTextBox();

            switch (combatPhase)
            {
                case CombatPhase.LOADOUT:

                    GameLoop.display.DrawString("Loadout selection", 4, 50);
                    GameLoop.display.DrawString("Begin encounter (SPACE)", 4, 52);

                    break;

                case CombatPhase.PLAYER_TURN:

                    GameLoop.display.DrawString("Player's turn", 4, 50);
                    GameLoop.display.DrawString("Weapon attack (Q)", 4, 52);
                    GameLoop.display.DrawString("Use spell 1 (W)", 4, 53);
                    GameLoop.display.DrawString("Use spell 2 (E)", 4, 54);
                    GameLoop.display.DrawString("Use spell 3 (R)", 4, 55);

                    List<string> playerInfo = GetPlayerInfo();
                    for (int i = 0; i < playerInfo.Count; i++)
                    {
                        GameLoop.display.DrawString(playerInfo[i], 40, 50 + i);
                    }

                    List<string> enemyInfo = GetEnemyInfo();
                    for (int i = 0; i < enemyInfo.Count; i++)
                    {
                        GameLoop.display.DrawString(enemyInfo[i], 60, 50 + i);
                    }

                    break;

                case CombatPhase.ENEMY_TURN:

                    GameLoop.display.DrawString("Enemy's turn", 4, 50);
                    GameLoop.display.DrawString("Next turn (SPACE)", 4, 52);

                    string enemyAction = enemy.TakeAction();
                    GameLoop.display.DrawString(enemyAction, 40, 50);

                    break;

                case CombatPhase.WIN:

                    GameLoop.display.DrawString("The player won!", 4, 50);
                    GameLoop.display.DrawString("End encounter (SPACE)", 4, 52);
                    // itt loot-ot kéne kapni

                    break;

                case CombatPhase.LOSE:

                    GameLoop.display.DrawString("The player lost!", 4, 50);
                    GameLoop.display.DrawString("End encounter (SPACE)", 4, 52);
                    // itt meg kéne hóni

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
                            //ChangeCombatPhase(CombatPhase.ENEMY_TURN);
                            ChangeCombatPhase(CombatPhase.LOSE);

                            break;
                    }

                    break;

                case CombatPhase.ENEMY_TURN:

                    if (key == ConsoleKey.Spacebar)
                    {
                        ChangeCombatPhase(CombatPhase.PLAYER_TURN);
                    }

                    break;

                case CombatPhase.WIN:

                    if (key == ConsoleKey.Spacebar)
                    {
                        ChangeCombatPhase(CombatPhase.PLAYER_TURN);

                        End();
                    }

                    break;

                case CombatPhase.LOSE:

                    if (key == ConsoleKey.Spacebar)
                    {
                        ChangeCombatPhase(CombatPhase.PLAYER_TURN);

                        End();
                    }

                    break;
            }
            
        }

    }
}
