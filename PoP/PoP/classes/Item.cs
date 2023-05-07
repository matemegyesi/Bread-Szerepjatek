using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    ///<summary>
    ///The type of an item: Weapon or Armor.
    ///</summary>
    public enum ItemType
    {
        /// <summary>
        /// Can be equipped to Hand or Ring
        /// </summary>
        WEAPON,
        /// <summary>
        /// Can be equipped to Head, Chest or Leg
        /// </summary>
        ARMOR
    }
    abstract class Item : ICollectible, IEquippable
    {
        ///<summary>
        ///The name of the item.
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///The slot where the item can be equipped.
        ///</summary>
        public Slot Slot { get; set; }

        public abstract void Collect();

        public abstract void Drop();

        ///<summary>
        ///Equips the item to the appropriate gear slot.
        ///</summary>
        public virtual void Equip()
        {
            // If the gear slot is empty
            if (Inventory.gear[Slot] == null)
            {
                // Remove the item from the inventory
                Inventory.inventory.Remove(this);

                // Add the item to the gear slot
                Inventory.gear[Slot] = this;
            }
            // If the gear slot is not empty
            else
            {
                // Unequip the current gear in the same slot
                Inventory.gear[Slot].UnequipAuto(Inventory.inventory.IndexOf(this));

                // Add the new item to the gear slot
                Inventory.gear[Slot] = this;
            }

            Wire.Gear.ForceUpdate();
            Wire.Inventory.UpdateItemList(Inventory.inventory);
        }

        ///<summary>
        ///Removes the item from the gear slot and adds it back to the inventory.
        ///</summary>
        /// <param name="inventoryIndex">The index of the item in the inventory list.</param>
        /// <remarks>
        /// This method is used to automatically unequip an item from the gear slot and move it to the inventory list.
        /// The gear slot is set to null, and the item is added to the specified index in the inventory list.
        /// </remarks>
        public void UnequipAuto(int inventoryIndex)
        {
            Inventory.gear[Slot] = null;
            Inventory.inventory[inventoryIndex] = this;

            Wire.Gear.UpdateGear();
            Wire.Inventory.UpdateItemList(Inventory.inventory);
        }

        ///<summary>
        ///Unequips the item and adds it back to the inventory if there is enough space.
        ///</summary>
        ///<returns>
        ///Returns true if the item was successfully unequipped and added to the inventory.
        ///Returns false if the inventory is full and the item could not be unequipped.
        ///</returns>
        public bool Unequip()
        {
            if (Inventory.inventory.Count < Inventory.inventoryLimit)
            {
                Inventory.gear[Slot] = null;
                Inventory.inventory.Add(this);

                Wire.Gear.UpdateGear();

                return true;
            }

            Wire.Gear.UpdateGear();
            Wire.Inventory.UpdateItemList(Inventory.inventory);

            return false;
        }
    }
}
