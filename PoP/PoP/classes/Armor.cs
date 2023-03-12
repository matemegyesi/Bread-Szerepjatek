using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Armor : Item, ICollectible, IEquippable
    {
        public float Defense { get; set; }

        public string Name { get; set; }

        public Inventory.Slot slot { get; set; }


        public Armor(Inventory.Slot armorType, string name)
        {
            switch (armorType)
            {
                case Inventory.Slot.HEAD:
                case Inventory.Slot.CHEST:
                case Inventory.Slot.LEG:
                    this.slot = armorType;
                    break;
                default:
                    throw new ArgumentException($"Ez nem egy armor tipus: {armorType}");
            }
            this.Name = name;
        }

        public void Collect()
        {
            Inventory.inventory.Add(this);
        }

        public void Drop()
        {
            Inventory.inventory.Remove(this);
        }

        /// <summary>
        /// Felveszi a játékosnak a szettjét
        /// </summary>
        public void Equip()
        {
            Inventory.armorList.Add(this);
        }
    }
}
