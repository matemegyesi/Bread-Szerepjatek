using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    /// <summary>
    /// HAND slotra berakható itemek
    /// </summary>
    class Weapon : Item, ICollectible, IEquippable
    {
        public void Collect()
        {
            Inventory.inventory.Add(this);
        }

        public void Drop()
        {
            Inventory.inventory.Remove(this);
        }

        public void Equip(Inventory.Slot slot)
        {
            //todo
        }
    }
}
