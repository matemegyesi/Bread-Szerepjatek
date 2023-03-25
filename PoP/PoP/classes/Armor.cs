using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Armor : Item
    {
        public float Defense { get; set; }

        public Armor(string name,Slot armorType, float defense)
        {
            switch (armorType)
            {
                case Slot.HEAD:
                case Slot.CHEST:
                case Slot.LEG:
                    slot = armorType;
                    break;
                default:
                    throw new ArgumentException($"Ez nem egy armor tipus: {armorType}");
            }
            Name = name;
            Defense = defense;
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
            return $"{Name};{Defense};{slot}";
        }
    }
}
