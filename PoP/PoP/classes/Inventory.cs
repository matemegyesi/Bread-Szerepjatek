﻿using PoP.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP
{
    ///<summary>
    /// An enumeration representing the different item slots in the inventory.
    /// </summary>
    public enum Slot
    {
        HEAD,
        CHEST,
        LEG,
        HAND,
        RING
    }
    class Inventory
    {
        ///<summary>
        /// The maximum number of items that the inventory can hold.
        /// </summary>
        public const int inventoryLimit = 40;

        ///<summary>
        /// The list of items in the inventory.
        /// </summary>
        public static List<Item> inventory = new List<Item>();

        ///<summary>
        /// A dictionary of gear slots and their corresponding equipped items.
        /// </summary>
        public static Dictionary<Slot, Item> gear = new Dictionary<Slot, Item>() {
            { Slot.HEAD, null},
            { Slot.CHEST, null},
            { Slot.LEG, null},
            { Slot.HAND, null},
            { Slot.RING, null}
        };
    }
}
