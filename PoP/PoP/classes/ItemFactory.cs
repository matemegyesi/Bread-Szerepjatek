using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class ItemFactory
    {
        ///<summary>
        ///Creates a new item of the specified type with the given properties.
        ///</summary>
        ///<param name="type">The type of item to create (Weapon or Armor).</param>
        ///<param name="name">The name of the item.</param>
        ///<param name="damageOrDefense">The damage (if the item is a weapon) or defense (if the item is an armor) value of the item.</param>
        ///<param name="slot">The slot where the item can be equipped.</param>
        ///<returns>A new item of the specified type with the given properties.</returns>
        public static Item CreateItem(ItemType type, string name, float damageOrDefense, Slot slot)
        {
            Item item = null;

            switch (type)
            {
                case ItemType.WEAPON:
                    item = new Weapon(name, slot, damageOrDefense);
                    break;
                case ItemType.ARMOR:
                    item = new Armor(name, slot, damageOrDefense);
                    break;
                default:
                    throw new ArgumentException("Invalid item type");
            }

            return item;
        }
    }
}
