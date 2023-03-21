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
            Inventory.inventory.Add(this);
        }

        public override void Drop()
        {
            Inventory.inventory.Remove(this);
        }

        public override void Equip(Slot slot)
        {
            //todo
        }

        public override string ToString()
        {
            return $"{Name};{Damage};{slot}";
        }
    }
}
