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
            stateMachine.CanContinue = true;

            // Set normal values
            Player.Damage = Player.BaseDamage;
            Player.Defence = Player.BaseDefence;
            Player.ManaRate = Player.BaseManaRate;
            stateMachine.enemy.Damage = stateMachine.enemy.BaseDamage;
            stateMachine.enemy.Defence = stateMachine.enemy.BaseDefence;
            stateMachine.enemy.CanHeal = true;
            stateMachine.enemy.CanCastEffect = true;

            // Visuals
            Wire.Combat.TurnTitle = Style.Color(" # Loadout inspection # ", ColorAnsi.WHITE);
            Wire.Combat.FKeyName = "Flee";
            Wire.Combat.SpaceKeyName = "Begin";
            Wire.Combat.ForceUpdate();
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
                stateMachine.ChangeCombatState(stateMachine.PlayerState);
            }
        }

        public override void Exit()
        {
            stateMachine.CanSkip = false;

            Wire.Combat.FKeyName = "Skip";
            Wire.Combat.SpaceKeyName = "Continue";
            Wire.Combat.ForceUpdate();
        }
    }
}
