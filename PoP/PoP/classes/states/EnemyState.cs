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
            string actionDesc = enemy.TakeAction();
            Wire.Dialogue.ProgressCombat(enemy.Name, actionDesc, ColorAnsi.ORANGE);
        }

        public override void KeyPressed(ConsoleKey key)
        {

        }

        public override void Exit()
        {

        }
    }
}
