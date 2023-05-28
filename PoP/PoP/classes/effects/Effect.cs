using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.effects
{
    internal abstract class Effect
    {
        public abstract string TakeEffect();

        public abstract string TakeEffect(Enemy target);

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
