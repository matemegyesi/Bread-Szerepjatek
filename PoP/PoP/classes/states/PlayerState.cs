using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class PlayerState : State
    {
        public PlayerState(Combat loc) : base(loc)
        {

        }

        public override void Enter()
        {
            stateMachine.SetCanContinue(false);
            stateMachine.SetCanUseWeapon(true);
        }

        public override void KeyPressed(ConsoleKey key)
        {

        }

        public override void Exit()
        {

        }
    }
}
