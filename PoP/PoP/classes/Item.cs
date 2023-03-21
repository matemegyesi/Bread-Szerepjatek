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
        public abstract void Equip(Slot slot);
    }
}
