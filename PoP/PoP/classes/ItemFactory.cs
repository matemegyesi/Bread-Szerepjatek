﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class ItemFactory
    {
        public static Item CreateItem(ItemType type, string name, float damageOrDefense, Inventory.Slot armorSlot = Inventory.Slot.HEAD)
        {
            Item item = null;

            switch (type)
            {
                case ItemType.Weapon:
                    item = new Weapon { Name = name, Damage = damageOrDefense};
                    break;
                case ItemType.Armor:
                    item = new Armor(name, armorSlot, damageOrDefense);
                    break;
                default:
                    throw new ArgumentException("Invalid item type");
            }

            return item;
        }
    }
}
