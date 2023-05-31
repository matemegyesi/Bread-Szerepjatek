using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class State
    {
        protected Combat stateMachine { get; private set; }

        public State(Combat loc)
        {
            stateMachine = loc;
        }

        public virtual void Enter()
        {

        }

        public virtual void KeyPressed(ConsoleKey key)
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void ResetBooleans(bool canContinue = false)
        {
            stateMachine.CanSkip = false;
            stateMachine.CanContinue = canContinue;
            stateMachine.CanUseWeapon = false;
        }
    }
}
