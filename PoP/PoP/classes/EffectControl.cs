using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    public enum Effect
    {
        Burn,
        Freeze,
        Stun,
        Poison,
        Bleed,
        Buff,
        Debuff
    }

    internal static class EffectControl
    {
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

        public static void TakeEffect(Effect effect, Enemy target)
        {
            return;
        }
    }
}
