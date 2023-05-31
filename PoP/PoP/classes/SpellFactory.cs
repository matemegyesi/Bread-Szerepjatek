using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<Effect> effects = FileInput.GetEffectList(properties["effects"].ToString().Split(';'));
            int level = int.Parse(properties["level"].ToString());
            string description = properties["description"].ToString();

            try
            {
                spell = new Spell(name, mana, damage, heal, effects, level, description);
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
