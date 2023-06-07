using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using PoP.classes.states;

namespace PoP.classes
{
    class Combat : Location
    {
        public LoadoutState LoadoutState { get; private set; }
        public PlayerState PlayerState { get; private set; }
        public EnemyState EnemyState { get; private set; }
        public WinState WinState { get; private set; }
        public LoseState LoseState { get; private set; }

        public State CurrentState { get; private set; }

        public Enemy enemy { get; set; }

        // Combat settings
        public bool CanSkip { get; set; }
        public bool CanContinue { get; set; }
        public bool CanUseWeapon { get; set; }
        public bool CanUseSorcery { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Combat"/> class.
        /// </summary>
        /// <param name="id">The ID of the combat encounter.</param>
        /// <param name="x">The X coordinate of the location of the combat encounter.</param>
        /// <param name="y">The Y coordinate of the location of the combat encounter.</param>
        /// <param name="path">The path to the combat encounter.</param>
        public Combat(int id, string path) : base(id)
        {
            this.id = id;
            Path = path;

            string json = File.ReadAllText(path);
            List<Dictionary<string, object>> engagement = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            positionX = int.Parse(engagement[0]["posX"].ToString());
            positionY = int.Parse(engagement[0]["posY"].ToString());

            bool isHidden;
            if (bool.TryParse(engagement[0]["isHidden"].ToString(), out isHidden))
            {
                IsHidden = isHidden;
            }
            else
            {
                IsHidden = false;
            }

            if (!IsHidden)
            {
                Name = engagement[0]["locationName"].ToString();
            }

            enemy = new Enemy(engagement[1], this);

            // State setup
            LoadoutState = new LoadoutState(this);
            PlayerState = new PlayerState(this);
            EnemyState = new EnemyState(this, enemy);
            WinState = new WinState(this);
            LoseState = new LoseState(this);

            CurrentState = LoadoutState;
        }

        /// <summary>
        /// Sets the <see cref="Map"/>.CurrentLocation to this instance and starts the Combat
        /// </summary>
        public override void LoadLocation()
        {
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

            Wire.Disable(Wire.Map);
            Wire.Enable(Wire.Combat);
            Wire.Combat.SetEnemy(enemy);
            Wire.Combat.SetCombat(this);

            CurrentState.Enter();
            CanSkip = true;
        }
        
        /// <summary>
        /// Ends the combat encounter.
        /// </summary>
        public override void End()
        {
            CurrentState.Exit();

            base.End();
            KeyboardInput.KeyPressed -= KeyPressed;

            Wire.Dialogue.ClearDialogue();
            Wire.Disable(Wire.Combat);
            Wire.Combat.SetEnemy(null);
            Wire.Combat.SetCombat(null);
            Wire.Enable(Wire.Map);
            Wire.Gear.UpdateGear();

            isCompleted = true;
            Wire.Map.UpdateLocation(this);
            GameLoop.Phase = GamePhase.ADVENTURE;
        }
        
        public void Flee()
        {
            base.End();
            KeyboardInput.KeyPressed -= KeyPressed;

            Wire.Disable(Wire.Combat);
            Wire.Combat.SetEnemy(null);
            Wire.Combat.SetCombat(null);
            Wire.Enable(Wire.Map);

            GameLoop.playerMovement.Teleport(GameLoop.playerMovement.PrevPlayerX, GameLoop.playerMovement.PrevPlayerY);
            GameLoop.Phase = GamePhase.ADVENTURE;
        }

        /// <summary>
        /// Changes the current combat state to the specified new state.
        /// </summary>
        /// <param name="nextState">The new State of the combat encounter.</param>
        public void ChangeCombatState(State nextState)
        {
            CurrentState.Exit();

            CurrentState = nextState;
            CurrentState.Enter();
        }

        public void Loot()
        {
            Loot loot = new Loot(enemy.Level);

            for (int i = 0; i < loot.ItemLoot.Count; i++)
            {
                string _loot = string.Empty;
                if (i == 0)
                    _loot = "Loot";

                if (loot.ItemLoot[i] is Weapon)
                {
                    Wire.Dialogue.ProgressCombat(_loot, $"{Style.Color(loot.ItemLoot[i].Name, ColorAnsi.LIGHT_BLUE)}, damage: {Style.Color(loot.ItemLoot[i].DamageOrDefense.ToString("0 dmg"), ColorAnsi.LIGHT_RED)}", ColorAnsi.WHEAT);
                }
                else if (loot.ItemLoot[i] is Armor)
                {
                    Wire.Dialogue.ProgressCombat(_loot, $"{Style.Color(loot.ItemLoot[i].Name, ColorAnsi.LIGHT_BLUE)}, defense: {Style.Color(loot.ItemLoot[i].DamageOrDefense.ToString("0 def"), ColorAnsi.AQUA)}", ColorAnsi.WHEAT);
                }
            }

            loot.ItemLoot.ForEach(x => x.Collect());
        }

        public void KeyPressed(ConsoleKey key)
        {
            CurrentState.KeyPressed(key);
        }
    }
}
