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
    class Weapon : Item
    {
        public float Damage { get; set; }

        public Weapon(string name, Slot weaponType, float damage)
        {
            switch (weaponType)
            {
                case Slot.HAND:
                case Slot.RING:
                    slot = weaponType;
                    break;
                default:
                    throw new ArgumentException($"Ez nem egy weapon tipus: {weaponType}");
            }
            Name = name;
            Damage = damage;
        }
        public override void Collect()
        {
            if(Inventory.inventory.Count < Inventory.inventoryLimit)
            {
                Inventory.inventory.Add(this);
                GameLoop.display.DrawString($"INVENTORY({Inventory.inventory.Count})", 212, 1);
            }
        }

        public override void Drop()
        {
            Inventory.inventory.Remove(this);
            GameLoop.display.DrawString($"INVENTORY({Inventory.inventory.Count})", 212, 1);
        }

        public override void Equip()
        {
            base.Equip();
        }

        public override string ToString()
        {
            return $"{Name};{Damage};{slot}";
        }
    }
}
