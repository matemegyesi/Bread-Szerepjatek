using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Armor : Item
    {
        /// <summary>
        /// The Defense property represents a float value that represents how many percent of the damage it can negate.
        /// </summary>
        public double Defense { get; set; }

        /// <summary>
        /// Creates a new armor with the specified name, type, and defense.
        /// </summary>
        /// <param name="name">The name of the armor.</param>
        /// <param name="armorType">The type of armor (head, chest, or leg).</param>
        /// <param name="defense">The amount of defense the armor provides.</param>
        /// <param name="main">Only needed for the MainCape</param>
        /// <exception cref="ArgumentException">Thrown if an invalid armor type is specified.</exception>
        public Armor(string name,Slot armorType, double defense, bool main=false)
        {
            if (main)
            {
                Name = name;
                Slot = Slot.MAINCAPE;
                Defense = defense;
            }
            else
            {
                switch (armorType)
                {
                    case Slot.HEAD:
                    case Slot.CHEST:
                    case Slot.LEG:
                        Slot = armorType;
                        break;
                    default:
                        throw new ArgumentException($"Ez nem egy armor tipus: {armorType}");
                }
                Name = name;
                Defense = defense;
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
                GameLoop.display.DrawString($"INVENTORY({Inventory.inventory.Count})", 212, 1);
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
            GameLoop.display.DrawString($"INVENTORY({Inventory.inventory.Count})", 212, 1);
        }

        public override void Equip()
        {
            base.Equip();
        }
        public override string ToString()
        {
            return $"{Name};{Defense};{Slot}";
        }
    }
}
