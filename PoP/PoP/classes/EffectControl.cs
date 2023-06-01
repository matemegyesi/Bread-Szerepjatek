using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    /// <summary>
    /// EFFECTs influence stats and create difficult choices in combat.
    /// </summary>
    public enum Effect
    {
        /// <summary>
        /// BURN EFFECT halves the defence for the Player.
        /// </summary>
        Burn,
        /// <summary>
        /// FREEZE EFFECT halves the mana regeneration rate for the Player.
        /// </summary>
        Freeze,
        /// <summary>
        /// STUN EFFECT takes away the control from the Player.
        /// </summary>
        Stun,
        /// <summary>
        /// POISON EFFECT disables a spell of the Player.
        /// The disablement depends on the amount of spells.
        /// There's a 10% chance that every spell gets disabled.
        /// </summary>
        Poison,
        /// <summary>
        /// BLEED EFFECT takes away 25% from the health of the Player.
        /// </summary>
        Bleed,
        /// <summary>
        /// BUFF EFFECT raises the damage by 25% for the Player.
        /// </summary>
        Buff,
        /// <summary>
        /// DEBUFF EFFECT reduces the damage by 25% for the Player.
        /// </summary>
        Debuff
    }

    internal static class EffectControl
    {
        /// <summary>
        /// Activates the changes of the Effect for the Player.
        /// </summary>
        /// <param name="effect">The Effect enum that has to take effect.</param>
        public static void TakeEffect(Effect effect)
        {
            switch (effect)
            {
                case Effect.Burn:
                    Player.Defence = Player.BaseDefence / 2;
                    break;

                case Effect.Freeze:
                    Player.ManaRate = Player.BaseManaRate / 2;
                    break;

                case Effect.Stun:
                    Player.IsStunned = true;
                    break;

                case Effect.Poison:
                    int _randomNumber = new Random().Next(0, 10);
                    if (Inventory.sorcery.Count(x => x != null) > 0)
                    {
                        if (_randomNumber != 0)
                        {
                            int _randomIndex = new Random().Next(0, 4);
                            if (Inventory.sorcery[_randomIndex] != null)
                            {
                                Player.PoisonedSpells[_randomIndex] = true;
                                Wire.Dialogue.ProgressCombat(effect.ToString(), $"effect disabled the {Style.Color(Inventory.sorcery[_randomIndex].Name, ColorAnsi.MAGENTA)} spell.", ColorAnsi.PINK);
                            }
                        }
                        else
                        {
                            Wire.Dialogue.ProgressCombat(effect.ToString(), $"effect temporarily damaged the {Style.Color(Player.Name + "'s", ColorAnsi.GREEN)} neocortex, disabling {Style.Color("every spell", ColorAnsi.MAGENTA)}.", ColorAnsi.PINK);
                            Player.SetPosionedSpells(true);
                        }
                    }
                    break;

                case Effect.Bleed:
                    double _damage = Player.Health * .1;
                    Player.TakeDamage(_damage);
                    Wire.Dialogue.ProgressCombat(effect.ToString(), $"effect caused {Style.Color(_damage.ToString("-0.# hp"), ColorAnsi.LIGHT_RED)}.", ColorAnsi.PINK);
                    break;

                case Effect.Buff:
                    Player.Damage += Player.BaseDamage * .25;
                    break;

                case Effect.Debuff:
                    Player.Damage -= Player.BaseDamage * .25;
                    break;
            }
        }

        /// <summary>
        /// Activates the changes of the Effect for the specified enemy.
        /// </summary>
        /// <param name="effect">The Effect enum that has to take effect.</param>
        /// <param name="target">The Enemy that got the effect.</param>
        public static void TakeEffect(Effect effect, Enemy target)
        {
            return;
        }
    }
}
