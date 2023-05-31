using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace PoP.classes
{
    class Enemy
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public double BaseDamage { get; set; }
        public double Damage { get; set; }

        public double BaseDefence { get; set; }
        public double Defence { get; set; }

        public double MaxHealth { get; set; }
        public double Health { get; set; }

        public int MaxMana { get; set; }
        public int Mana { get; set; }

        public Dictionary<Effect, int> EffectDict = new Dictionary<Effect, int>()
        {
            { Effect.Burn, 0 },
            { Effect.Freeze, 0 },
            { Effect.Stun, 0 },
            { Effect.Poison, 0 },
            { Effect.Bleed, 0 },
            { Effect.Buff, 0 },
            { Effect.Debuff, 0 }
        };

        public Dictionary<string, string> data { get; set; }
        private Combat combat;

        public Enemy(Dictionary<string, object> data, Combat location)
        {
            Name = data["name"].ToString();
            BaseDamage = double.Parse(data["damage"].ToString());
            BaseDefence = double.Parse(data["defence"].ToString());
            MaxHealth = double.Parse(data["health"].ToString());
            Health = MaxHealth;
            Level = int.Parse(data["level"].ToString());

            combat = location;
        }

        public string TakeAction()
        {
            string action = $"The enemy dealt {Style.Color(BaseDamage.ToString("0.# dmg"), ColorAnsi.LIGHT_RED)}.";

            EnemyAttack();

            return action;
        }

        public void EnemyAttack()
        {
            Player.TakeDamage(BaseDamage);
        }

        public void TakeDamage(double damage)
        {
            if (Health - damage <= 0)
            {
                Health = 0;
                combat.ChangeCombatState(combat.WinState);
            }
            else
            {
                Health -= damage;
            }
        }

        public void TakeSpell(Spell spell)
        {
            TakeDamage(spell.Damage);

            foreach (Effect effect in spell.EffectList)
            {
                if (effect == Effect.Stun)
                {
                    EffectDict[effect] = 1;
                }
                else if (effect == Effect.Poison)
                {
                    EffectDict[effect] = 2;
                }
                else
                {
                    EffectDict[effect] = 3;
                }
            }
        }
    }
}
