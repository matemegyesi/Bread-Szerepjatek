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

        }

        public override void KeyPressed(ConsoleKey key)
        {
            // Flee
            if (key == ConsoleKey.F)
            {
                stateMachine.Flee();
            }
        }

        public override void Exit()
        {

        }
    }
}
