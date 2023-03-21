using PoP.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP
{
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

        public const int inventoryLimit = 40;

        public static List<Item> inventory = new List<Item>();

        public static Dictionary<Slot, Item> gear = new Dictionary<Slot, Item>() {
            { Slot.HEAD, null},
            { Slot.CHEST, null},
            { Slot.LEG, null},
            { Slot.HAND, null},
            { Slot.RING, null}
        };
    }
}
