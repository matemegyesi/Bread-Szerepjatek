using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class EnemyState : State
    {
        private Enemy enemy;

        public EnemyState(Combat loc, Enemy enemy) : base(loc)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            actionDescription = enemy.TakeAction();
            Wire.Dialogue.ProgressCombat(enemy.Name, actionDescription, ColorAnsi.ORANGE);
            Wire.Combat.TurnName = Style.Color($" # {enemy.Name}'s turn # ", ColorAnsi.ORANGE);

            stateMachine.CanContinue = true;
        }

        public override void KeyPressed(ConsoleKey key)
        {
            if (key == ConsoleKey.Spacebar)
            {
                Wire.Combat.ForceUpdate(); //
                stateMachine.ChangeCombatState(stateMachine.PlayerState);
            }
        }

        public override void Exit()
        {
            ResetBooleans();
        }
    }
}
