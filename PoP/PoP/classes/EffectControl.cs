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

    internal class EffectControl
    {
        public string TakeEffect()
        {
            return "";
        }

        public string TakeEffect(Enemy target)
        {
            return "";
        }
    }
}
