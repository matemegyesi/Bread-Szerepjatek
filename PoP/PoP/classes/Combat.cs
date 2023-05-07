using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    

namespace PoP.classes
{
    /// <summary>
    /// Represents the different phases of combat.
    /// </summary>
    enum CombatPhase
    {
        /// <summary>
        /// The loadout phase, where the player can customize their spells.
        /// </summary>
        LOADOUT,

        /// <summary>
        /// The player turn phase, where the player can attack with spell or weapon.
        /// </summary>
        PLAYER_TURN,

        /// <summary>
        /// The enemy turn phase, where the enemy takes action.
        /// </summary>
        ENEMY_TURN,

        /// <summary>
        /// The lose phase, where the player loses the game.
        /// </summary>
        LOSE,

        /// <summary>
        /// The win phase, where the player wins the game.
        /// </summary>
        WIN
    }

    class Combat : Location
    {
        public CombatPhase combatPhase;

        Enemy enemy; // kell majd rendes beolvasás

        /// <summary>
        /// Initializes a new instance of the <see cref="Combat"/> class.
        /// </summary>
        /// <param name="id">The ID of the combat encounter.</param>
        /// <param name="x">The X coordinate of the location of the combat encounter.</param>
        /// <param name="y">The Y coordinate of the location of the combat encounter.</param>
        /// <param name="path">The path to the combat encounter.</param>
        public Combat(int id, int x, int y, bool isHidden, string path) : base(id, x, y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            Path = path;

            enemy = new Enemy(path);

            this.isHidden = isHidden;
        }

        /// <summary>
        /// Sets the <see cref="Map"/>.CurrentLocation to this instance and starts the Combat
        /// </summary>
        public override void LoadLocation()
        {
            GameLoop.display.WipeTextBox();
            Map.CurrentLocation = this;
            GameLoop.Phase = GamePhase.COMBAT;

            Start();
        }

        /// <summary>
        /// Starts the combat encounter.
        /// </summary>
        public override void Start()
        {
            base.Start();
            KeyboardInput.KeyPressed += KeyPressed;

            ChangeCombatPhase(CombatPhase.LOADOUT);

        }

        /// <summary>
        /// Ends the combat encounter.
        /// </summary>
        public override void End()
        {
            base.End();
            KeyboardInput.KeyPressed -= KeyPressed;

            isCompleted = true;
            WindowRenderer.Map.UpdateLocation(this);
            GameLoop.Phase = GamePhase.ADVENTURE;
        }

        /// <summary>
        /// Generates a list of strings containing information about the player's current state.
        /// </summary>
        /// <returns>A list of strings containing player information.</returns>
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

        /// <summary>
        /// Generates a list of strings containing information about the enemy's current state.
        /// </summary>
        /// <returns>A list of strings containing enemy information.</returns>
        private List<string> GetEnemyInfo()
        {
            List<string> enemyInfo = new List<string>();

            enemyInfo.Add("Enemy: " + enemy.Name);
            enemyInfo.Add(" ");
            enemyInfo.Add("Level: " + enemy.Level);
            enemyInfo.Add("Health: " + enemy.Health);
            enemyInfo.Add("Damage: " + enemy.Damage);
            enemyInfo.Add("Defence: " + enemy.Defence);
            // playerInfo.Add("Effektek: " + String.Join(", ", enemy.Effects vagy ami a neve lesz));

            return enemyInfo;
        }

        /// <summary>
        /// Changes the current combat phase of the game to the specified new phase.
        /// </summary>
        /// <param name="newPhase">The new CombatPhase enumeration value to change the current phase to.</param>
        public void ChangeCombatPhase(CombatPhase newPhase)
        {
            combatPhase = newPhase;
            GameLoop.display.WipeTextBox();

            switch (combatPhase)
            {
                case CombatPhase.LOADOUT:
                    // Display the loadout selection screen
                    GameLoop.display.DrawString("Loadout selection", 4, 50);
                    GameLoop.display.DrawString("Begin encounter (SPACE)", 4, 52);

                    break;

                case CombatPhase.PLAYER_TURN:
                    // Display the player's turn screen
                    GameLoop.display.DrawString("Player's turn", 4, 50);
                    GameLoop.display.DrawString("Weapon attack (Q)", 4, 52);
                    GameLoop.display.DrawString("Use spell 1 (W)", 4, 53);
                    GameLoop.display.DrawString("Use spell 2 (E)", 4, 54);
                    GameLoop.display.DrawString("Use spell 3 (R)", 4, 55);

                    // Display the player and enemy information
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
                    // Display the enemy's turn screen
                    GameLoop.display.DrawString("Enemy's turn", 4, 50);
                    GameLoop.display.DrawString("Next turn (SPACE)", 4, 52);

                    // Get the enemy's action and display it
                    string enemyAction = enemy.TakeAction();
                    GameLoop.display.DrawString(enemyAction, 40, 50);

                    break;

                case CombatPhase.WIN:
                    // Display the win screen and prompt to end encounter
                    GameLoop.display.DrawString("The player won!", 4, 50);
                    GameLoop.display.DrawString("End encounter (SPACE)", 4, 52);
                    // itt loot-ot kéne kapni

                    break;

                case CombatPhase.LOSE:
                    // Display the lose screen and prompt to end encounter
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
