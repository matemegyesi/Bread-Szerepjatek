using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class PlayerState : State
    {
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
            stateMachine.CanUseSorcery = true;

            Player.RegenerateMana();

            Wire.Combat.TurnName = Style.Color($"{Player.Name}'s turn", ColorAnsi.GREEN);
            Wire.Combat.ForceUpdate();
        }

        public override void KeyPressed(ConsoleKey key)
        {

            Wire.Combat.ForceUpdate(); //

            if (!doneAction)
            {
                if (key == ConsoleKey.F)
                {
                    if (new Random().Next(0, 10) == 0)
                    {
                        actionDescription = "took a quick nap.";
                    }
                    else
                    {
                        actionDescription = "rested a turn.";
                    }
                    doneAction = true;
                }

                if (key == ConsoleKey.T)
                {
                    actionDescription = Player.AttackWithWeapon(stateMachine.enemy);
                    doneAction = true;
                }

                if (key == ConsoleKey.Q || key == ConsoleKey.W || key == ConsoleKey.E || key == ConsoleKey.R)
                {
                    try
                    {
                        int i = Inventory.spellKeys[(int)key];

                        if (i >= 0 && i < 4 && Inventory.sorcery[i] != null)
                        {
                            actionDescription = Player.AttackWithSpell(stateMachine.enemy, Inventory.sorcery[i]);
                            doneAction = true;
                        }
                    }
                    catch (Exception) { }
                }

                if (doneAction)
                {
                    ResetBooleans(true);

                    Wire.Dialogue.ProgressCombat(Player.Name, actionDescription, ColorAnsi.GREEN);
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
