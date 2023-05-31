using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class LoseState : State
    {
        public LoseState(Combat loc) : base(loc)
        {

        }

        public override void Enter()
        {
            ResetBooleans(true);

            Wire.Combat.TurnTitle = Style.Color($" # {Player.Name} died! # ", ColorAnsi.RUST);
            Wire.Combat.SpaceKeyName = "Leave encounter";

            actionDescription = Style.Color($"slayed {Player.Name}.", ColorAnsi.RED);
            Wire.Dialogue.ProgressCombat(stateMachine.enemy.Name, actionDescription, ColorAnsi.ORANGE);
        }

        public override void KeyPressed(ConsoleKey key)
        {
            if (key == ConsoleKey.Spacebar)
            {
                Environment.Exit(0);
            }
        }

        public override void Exit()
        {

        }
    }
}
