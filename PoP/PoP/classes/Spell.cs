using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Spell
    {
        // The name of the spell
        public string Name { get; }

        // The mana cost of the spell
        public double ManaCost { get; }

        public double Damage { get; }
        public double Heal { get; }
        public string Effects { get; }

        public int LvL { get; }
        public string Description { get; }

        public Spell(string name, double mana, double damage, double heal, string effects, int level, string description = "")
        {
            Name = name;
            ManaCost = mana;
            Damage = damage;
            Heal = heal;
            Effects = effects;
            LvL = level;
            Description = description;
        }
        
        public void Collect()
        {
            if (Inventory.spellList.Count < Inventory.sorceryLimit)
            {
                Inventory.spellList.Add(this);

                Wire.Sorcery.UpdateSpellList(Inventory.spellList);
            }
        }

        public void Equip()
        {
            Inventory.spellList.Remove(this);
            Wire.Gear.ForceUpdate();
        }

        public void EquipSpecific(SpellSlot spellSlot)
        {

        }
    }
}
