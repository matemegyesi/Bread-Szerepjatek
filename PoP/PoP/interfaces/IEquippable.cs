using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP
{
    interface IEquippable
    {
        void Equip();
        void UnequipAuto(int inventoryIndex);
        void Unequip();
    }
}
