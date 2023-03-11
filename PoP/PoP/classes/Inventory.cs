using PoP.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP
{
    class Inventory
    {
        public enum Slot
        {
            HEAD,
            CHEST,
            LEG,
            HAND,
            RING
        }

        public static List<Item> inventory = new List<Item>(); 

    }
}
