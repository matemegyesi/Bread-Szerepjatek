using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class PlayerState : State
    {
        private Dictionary<int, int> spellKeys = new Dictionary<int, int>()
        {
            { 81, 0 },
            { 87, 1 },
            { 69, 2 },
            { 82, 3 }
        };
        private bool doneAction;

        public PlayerState(Combat loc) : base(loc)
        {

        }

        public override void Enter()
        {
            doneAction = false;

            stateMachine.CanSkip = true;
            stateMachine.CanContinue = false;
            stateMachine.CanUseWeapon = true;

            Player.RegenerateMana();

            Wire.Combat.ForceUpdate();
        }

        public override void KeyPressed(ConsoleKey key)
        {

            Wire.Combat.ForceUpdate(); //

            if (!doneAction)
            {
                if (key == ConsoleKey.F)
                {
                    doneAction = true;
                }

                if (key == ConsoleKey.T)
                {
                    Player.AttackWithWeapon(stateMachine.enemy);
                    doneAction = true;
                }

                if (key == ConsoleKey.Q || key == ConsoleKey.W || key == ConsoleKey.E || key == ConsoleKey.R)
                {
                    try
                    {
                        int i = spellKeys[(int)key];

                        if (i >= 0 && i < 4 && Inventory.sorcery[i] != null)
                        {
                            Player.AttackWithSpell(stateMachine.enemy, Inventory.sorcery[i]);
                            doneAction = true;
                        }
                    }
                    catch (Exception) { }
                }

                if (doneAction)
                {
                    ResetBooleans(true);
                }
            }
            else
            {
                if (key == ConsoleKey.Spacebar)
                {

                    Wire.Combat.ForceUpdate(); //
                    stateMachine.ChangeCombatState(stateMachine.EnemyState);
                }
            }
        }

        public override void Exit()
        {
            ResetBooleans();
        }
    }
}
