using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    public enum ItemType
    {
        Weapon,
        Armor
    }
    abstract class Item : ICollectible, IEquippable
    {
        public string Name { get; set; }

        public Slot slot { get; set; }

        public abstract void Collect();
        public abstract void Drop();

        /// <summary>
        /// Áthelyezi az item-et az inventory-ból a gear-be, automatikusan kicserélve a jelenlegi gear-t.
        /// </summary>
        public virtual void Equip()
        {
            if (Inventory.gear[slot] == null)
            {
                Inventory.inventory.Remove(this);
                Inventory.gear[slot] = this;
            }
            else
            {
                Inventory.gear[slot].UnequipAuto(Inventory.inventory.IndexOf(this));
                Inventory.gear[slot] = this;
            }

            // TODO: statok és inventory frissítése
        }
        
        /// <summary>
        /// Áthelyezi az item-et a gear-ből az inventory meghatározott indexére.
        /// </summary>
        /// <param name="inventoryIndex">Cél index.</param>
        public void UnequipAuto(int inventoryIndex)
        {
            Inventory.gear[slot] = null;
            Inventory.inventory[inventoryIndex] = this;

            // TODO: statok és inventory frissítése
        }

        /// <summary>
        /// Áthelyezi az item-et a gear-ből az inventory legvégére és igazat ad vissza, ha még nem telt meg.
        /// </summary>
        public bool Unequip()
        {
            if (Inventory.inventory.Count < Inventory.inventoryLimit)
            {
                Inventory.gear[slot] = null;
                Inventory.inventory.Add(this);

                return true;
            }

            return false;

            // TODO: statok és inventory frissítése
        }
    }
}
