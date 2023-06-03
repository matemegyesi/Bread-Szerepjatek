using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class EnemyState : State
    {
        public bool SlayedPlayer { get; set; }
        private Enemy enemy;

        public EnemyState(Combat loc, Enemy enemy) : base(loc)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            // Set normal values
            enemy.Damage = enemy.BaseDamage;
            enemy.Defence = enemy.BaseDefence;
            enemy.CanHeal = true;
            enemy.CanCastEffect = true;

            // Take effect
            foreach (Effect effect in stateMachine.enemy.EffectDict.Where(x => x.Value > 0).Select(x => x.Key))
            {
                EffectControl.TakeEffect(effect, stateMachine.enemy);
            }

            // Enemy action
            actionDescription = enemy.TakeAction();
            Wire.Dialogue.ProgressCombat(enemy.Name, actionDescription, ColorAnsi.DARK_RED);
            Wire.Combat.TurnTitle = Style.Color($" # {enemy.Name}'s turn # ", ColorAnsi.DARK_RED);

            stateMachine.CanContinue = true;
        }

        public override void KeyPressed(ConsoleKey key)
        {
            if (key == ConsoleKey.Spacebar)
            {
                if (!SlayedPlayer)
                {
                    Wire.Combat.ForceUpdate(); //
                    stateMachine.ChangeCombatState(stateMachine.PlayerState);
                }
                else
                {
                    Wire.Combat.ForceUpdate(); //
                    stateMachine.ChangeCombatState(stateMachine.LoseState);
                }
            }
        }

        public override void Exit()
        {
            // Effect reduction
            foreach (Effect effect in stateMachine.enemy.EffectDict.Where(x => x.Value > 0).Select(x => x.Key))
            {
                enemy.EffectDict[effect]--;
            }

            if (enemy.IsStunned)
                enemy.IsStunned = false;

            ResetBooleans();
        }
    }
}
