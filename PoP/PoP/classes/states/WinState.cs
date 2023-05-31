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

            Wire.Combat.TurnTitle = $"{Player.Name} won!";
            Wire.Combat.SpaceKeyName = "Leave encounter";

            actionDescription = Style.Color($"slayed {stateMachine.enemy.Name}.", ColorAnsi.RED);
            Wire.Dialogue.ProgressCombat(Player.Name, actionDescription, ColorAnsi.GREEN);
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

        }
    }
}
