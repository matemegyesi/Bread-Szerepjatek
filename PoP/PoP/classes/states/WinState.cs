using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class WinState : State
    {
        private double healthBoost;
        private double manaBoost;
        private double manaRateBoost;

        public WinState(Combat loc) : base(loc)
        {
            healthBoost = Math.Round(stateMachine.enemy.MaxHealth / 100, 1) * 10;
            manaBoost = stateMachine.enemy.Level * 5;
            manaRateBoost = stateMachine.enemy.Level * 2.5;
        }

        public override void Enter()
        {
            ResetBooleans(true);

            // Boost
            Player.MaxHealth += healthBoost;
            Player.Health = Player.MaxHealth;

            Player.MaxMana += manaBoost;
            Player.Mana = Player.MaxMana;

            Player.BaseManaRate += manaRateBoost;
            Player.ManaRate = Player.BaseManaRate;

            Wire.Combat.TurnTitle = Style.Color($" # {Player.Name} won! # ", ColorAnsi.LIGHT_GREEN);
            Wire.Combat.SpaceKeyName = "Leave encounter";

            // Victory text
            Wire.Dialogue.ClearDialogue();
            actionDescription = Style.Color($"slayed {stateMachine.enemy.Name}.", ColorAnsi.RED);
            Wire.Dialogue.ProgressCombat(Player.Name, actionDescription, ColorAnsi.GREEN);
            Wire.Dialogue.ProgressBlank();

            // Boost description
            Wire.Dialogue.ProgressCombat("Boost", $"{Style.Color(healthBoost.ToString("+0 max hp"), ColorAnsi.LIGHT_BLUE)}", ColorAnsi.TEAL);
            Wire.Dialogue.ProgressCombat("", $"{Style.Color(manaBoost.ToString("+0 max mana"), ColorAnsi.PURPLE)}");
            Wire.Dialogue.ProgressCombat("", $"{Style.Color(manaRateBoost.ToString("+0.# mana rate"), ColorAnsi.DARK_RED)}");
            Wire.Dialogue.ProgressBlank();

            // Loot description
            stateMachine.Loot();
            Wire.Dialogue.ProgressBlank();

            Wire.Inventory.UpdateItemList(Inventory.inventory);
            Wire.Gear.UpdateGear();
            Wire.Combat.ForceUpdate();
        }

        public override void KeyPressed(ConsoleKey key)
        {
            if (key == ConsoleKey.Spacebar)
            {
                stateMachine.End();
            }
        }

        public override void Exit()
        {
            foreach (Effect effect in Player.EffectDict.Select(x => x.Key))
            {
                Player.EffectDict[effect] = 0;
            }
        }
    }
}
