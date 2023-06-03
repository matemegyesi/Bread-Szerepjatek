using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class WinState : State
    {
        public WinState(Combat loc) : base(loc)
        {

        }

        public override void Enter()
        {
            ResetBooleans(true);

            Wire.Combat.TurnTitle = Style.Color($" # {Player.Name} won! # ", ColorAnsi.LIGHT_GREEN);
            Wire.Combat.SpaceKeyName = "Leave encounter";

            // Victory text
            Wire.Dialogue.ClearDialogue();
            actionDescription = Style.Color($"slayed {stateMachine.enemy.Name}.", ColorAnsi.RED);
            Wire.Dialogue.ProgressCombat(Player.Name, actionDescription, ColorAnsi.GREEN);
            Wire.Dialogue.ProgressBlank();

            // Loot + Boost description
            Wire.Dialogue.ProgressCombat("Player", $"{Style.Color((Math.Round(stateMachine.enemy.MaxHealth / 100) * 10).ToString("+0 max hp"), ColorAnsi.LIGHT_BLUE)}", ColorAnsi.GREEN);
            Wire.Dialogue.ProgressCombat("", $"{Style.Color(10.ToString("+0 max mana"), ColorAnsi.PURPLE)}");
            Wire.Dialogue.ProgressCombat("", $"{Style.Color(2.5.ToString("+0.# mana rate"), ColorAnsi.DARK_RED)}");
            Wire.Dialogue.ProgressBlank();
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
            Player.MaxHealth += Math.Round(stateMachine.enemy.MaxHealth / 100) * 10;
            Player.Health = Player.MaxHealth;

            Player.MaxMana += 10;
            Player.Mana = Player.MaxMana;

            Player.BaseManaRate += 2.5;
            Player.ManaRate = Player.BaseManaRate;

            foreach (Effect effect in Player.EffectDict.Select(x => x.Key))
            {
                Player.EffectDict[effect] = 0;
            }

            stateMachine.Loot();
        }
    }
}
