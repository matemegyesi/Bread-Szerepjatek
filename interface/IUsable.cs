using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfacek
{
    interface IUsable
    {
        int Cooldown { get; set; }
        void Use();
    }
}
