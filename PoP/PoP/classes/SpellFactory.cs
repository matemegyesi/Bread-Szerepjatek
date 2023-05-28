using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoP.classes.effects;

namespace PoP.classes
{
    internal class SpellFactory
    {
        public static Spell CreateSpell(Dictionary<string, object> properties)
        {
            Spell spell = null;

            string name = properties["name"].ToString();
            double mana = double.Parse(properties["mana"].ToString());
            double damage = double.Parse(properties["damage"].ToString());
            double heal = double.Parse(properties["heal"].ToString());
            string[] effects = properties["effects"].ToString().Split(';');
            List<Effect> effectList = new List<Effect>();
            foreach (string effect in effects)
            {
                switch (effect)
                {
                    case "Burn":
                        effectList.Add(new Burn());
                        break;

                    case "Freeze":
                        effectList.Add(new Freeze());
                        break;

                    case "Stun":
                        effectList.Add(new Stun());
                        break;

                    case "Poison":
                        effectList.Add(new Poison());
                        break;

                    case "Bleed":
                        effectList.Add(new Bleed());
                        break;

                    case "Buff":
                        effectList.Add(new Buff());
                        break;


                    case "Debuff":
                        effectList.Add(new Debuff());
                        break;
                }
            }
            int level = int.Parse(properties["level"].ToString());
            string description = properties["description"].ToString();

            try
            {
                spell = new Spell(name, mana, damage, heal, effectList, level, description);
            }
            catch { }

            return spell;
        }

        public static List<Spell> CreateSpellRange(List<Dictionary<string, object>> propertiesAll)
        {
            List<Spell> spellRange = new List<Spell>();
            foreach (Dictionary<string, object> properties in propertiesAll)
            {
                spellRange.Add(CreateSpell(properties));
            }

            return spellRange;
        }
    }
}
