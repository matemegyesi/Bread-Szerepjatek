using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class LoadoutState : State
    {
        public LoadoutState(Combat loc) : base(loc)
        {

        }

        public override void Enter()
        {
            Wire.Combat.SpaceKeyName = "Begin";
            stateMachine.SetCanContinue(true);
        }

        public override void KeyPressed(ConsoleKey key)
        {
            // Flee
            if (key == ConsoleKey.F)
            {
                stateMachine.Flee();
            }

            // Begin combat
            if (key == ConsoleKey.Spacebar)
            {
                stateMachine.ChangeCombatPhase(stateMachine.PlayerState);
            }
        }

        public override void Exit()
        {
            stateMachine.SetCanFlee(false);

            Wire.Combat.SpaceKeyName = "Continue";
            Wire.Combat.ForceUpdate();
        }
    }
}
