using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{

    class Weapon : Item
    {
        /// <summary>
        /// The damage the weapon adds to the player's statistics
        /// </summary>
        public override double DamageOrDefense { get; set; }

        /// <summary>
        /// Constructor for a Weapon object, taking a name, weapon type, and damage value as parameters.
        /// </summary>
        /// <param name="name">The name of the weapon.</param>
        /// <param name="weaponType">The type of slot that the weapon can be equipped to.</param>
        /// <param name="damage">The amount of damage the weapon can inflict.</param>
        /// <param name="main">Only needed for the MainSword</param>
        public Weapon(string name, Slot weaponType, double damage, bool main=false)
        {
            if (main)
            {
                Name = name;
                DamageOrDefense = damage;
                Slot = Slot.MainSword;
            }
            else
            {
                switch (weaponType)
                {
                    // Check if the weapon can be equipped to the HAND or RING slot
                    case Slot.Hand:
                    case Slot.Ring:
                    case Slot.MainSword:
                        Slot = weaponType;
                        break;

                    // Throw an ArgumentException if the weaponType is HAND or RING
                    default:
                        throw new ArgumentException($"Ez nem egy weapon tipus: {weaponType}");
                }
                Name = name;
                DamageOrDefense = damage;
            }
        }

        /// <summary>
        /// Adds the item to the player's inventory if there is space available.
        /// </summary>
        public override void Collect()
        {
            // Check if the inventory has space for the item.
            if (Inventory.inventory.Count < Inventory.inventoryLimit)
            {
                // Add the item to the inventory.
                Inventory.inventory.Add(this);

                // Update the display to show the current inventory count.
                Wire.Inventory.UpdateItemList(Inventory.inventory);

            }
        }

        /// <summary>
        /// Removes the item from the player's inventory.
        /// </summary>
        public override void Drop()
        {
            // Remove the item from the inventory.
            Inventory.inventory.Remove(this);

            // Update the display to show the current inventory count.
            Wire.Inventory.UpdateItemList(Inventory.inventory);
        }

        public override void Equip()
        {
            base.Equip();
        }

        public override string ToString()
        {
            return $"{Name} / {DamageOrDefense} / {Slot}";
        }
    }
}
