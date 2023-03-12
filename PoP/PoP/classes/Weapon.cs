﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    /// <summary>
    /// HAND slotra berakható itemek
    /// </summary>
    class Weapon : Item
    {
        public float Damage { get; set; }

        public override void Collect()
        {
            Inventory.inventory.Add(this);
        }

        public override void Drop()
        {
            Inventory.inventory.Remove(this);
        }

        public override void Equip(Inventory.Slot slot)
        {
            //todo
        }

        public override string ToString()
        {
            return $"{Name};{Damage}";
        }
    }
}
