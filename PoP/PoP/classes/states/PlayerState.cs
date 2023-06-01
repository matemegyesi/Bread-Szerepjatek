using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.states
{
    internal class PlayerState : State
    {
        public bool SlayedEnemy { get; set; }
        private bool doneAction;

        public PlayerState(Combat loc) : base(loc)
        {

        }

        public override void Enter()
        {
            // Set normal values
            Player.Damage = Player.BaseDamage;
            Player.Defence = Player.BaseDefence;
            Player.ManaRate = Player.BaseManaRate;
            Player.SetPosionedSpells(false);

            // Take effect
            foreach (Effect effect in Player.EffectDict.Where(x => x.Value > 0).Select(x => x.Key))
            {
                EffectControl.TakeEffect(effect);
                Player.EffectDict[effect]--;
            }

            // Stun check
            if (!Player.IsStunned)
            {
                doneAction = false;

                stateMachine.CanSkip = true;
                stateMachine.CanContinue = false;
                stateMachine.CanUseWeapon = true;
                stateMachine.CanUseSorcery = true;
            }
            else
            {
                doneAction = true;

                actionDescription = "pulled themself together.";
                ResetBooleans();
                stateMachine.CanSkip = true;
                Wire.Combat.FKeyName = "Wake up";
            }
            
            Player.RegenerateMana();

            // Visuals
            Wire.Combat.TurnTitle = Style.Color($" # {Player.Name}'s turn # ", ColorAnsi.GREEN);
            Wire.Combat.ForceUpdate();
        }

        public override void KeyPressed(ConsoleKey key)
        {

            Wire.Combat.ForceUpdate(); //

            if (!doneAction && !Player.IsStunned)
            {
                // Skip
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

                // Weapon attack
                if (key == ConsoleKey.T)
                {
                    actionDescription = Player.AttackWithWeapon(stateMachine.enemy);
                    doneAction = true;
                }

                // Cast spell
                if (key == ConsoleKey.Q || key == ConsoleKey.W || key == ConsoleKey.E || key == ConsoleKey.R)
                {
                    try
                    {
                        int i = Inventory.spellKeys[(int)key];

                        if (i >= 0 && i < 4 && Inventory.sorcery[i] != null)
                        {
                            if (Inventory.sorcery[i].ManaCost <= Player.Mana && Player.PoisonedSpells[i] == false)
                            {
                                actionDescription = Player.AttackWithSpell(stateMachine.enemy, Inventory.sorcery[i]);
                                doneAction = true;
                            }
                            else if (Player.PoisonedSpells[i] == true)
                            {
                                Wire.Dialogue.ProgressCombat("!!!", $"{Style.Color(Inventory.sorcery[i].Name, ColorAnsi.MAGENTA)} is disabled by the {Style.Color(Effect.Poison.ToString(), ColorAnsi.PINK)} effect.", ColorAnsi.RUST);
                            }
                            else if (Inventory.sorcery[i].ManaCost > Player.Mana)
                            {
                                Wire.Dialogue.ProgressCombat("!!!", $"Not enough {Style.Color("mana", ColorAnsi.PURPLE)}.", ColorAnsi.RUST);
                            }
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
                if (key == ConsoleKey.Spacebar && !Player.IsStunned)
                {
                    if (!SlayedEnemy)
                    {
                        Wire.Combat.ForceUpdate(); //
                        stateMachine.ChangeCombatState(stateMachine.EnemyState);
                    }
                    else
                    {
                        Wire.Combat.ForceUpdate(); //
                        stateMachine.ChangeCombatState(stateMachine.WinState);
                    }
                }
                
                if (key == ConsoleKey.F && Player.IsStunned)
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
