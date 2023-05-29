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
        public HashSet<Effect> EffectList { get; } = new HashSet<Effect>();

        public int LvL { get; }
        public string Description { get; }

        public Spell(string name, double mana, double damage, double heal, List<Effect> effectList, int level, string description = "")
        {
            Name = name;
            ManaCost = mana;
            Damage = damage;
            Heal = heal;
            EffectList = effectList.ToHashSet();
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

        public void Equip(int index)
        {
            Inventory.spellList.Remove(this);

            if (Inventory.sorcery[index] == null)
            {
                Inventory.spellList.Remove(this);
                Inventory.sorcery[index] = this;
            }
            else
            {
                Inventory.sorcery[index].Unequip(index);

                Inventory.spellList.Remove(this);
                Inventory.sorcery[index] = this;
            }

            Wire.Gear.ForceUpdate();
        }

        public void Unequip(int index)
        {
            Inventory.sorcery[index] = null;
            Inventory.spellList.Add(this);
        }
    }
}
