using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public List<Effect> RandomEffects { get; private set; } = new List<Effect>();

        public Dictionary<string, string> data { get; set; }
        private Combat combat;

        public Enemy(Dictionary<string, object> data, Combat location)
        {
            Name = data["name"].ToString();
            BaseDamage = double.Parse(data["damage"].ToString());
            BaseDefence = double.Parse(data["defence"].ToString());
            MaxHealth = double.Parse(data["health"].ToString());
            Health = MaxHealth;
            RandomEffects = FileInput.GetEffectList(data["effects"].ToString().Split(';'));
            Level = int.Parse(data["level"].ToString());

            combat = location;
        }

        public string TakeAction()
        {
            string action = string.Empty;

            action += EnemyAttack();

            return action + '.';
        }

        public string EnemyAttack()
        {
            string action = string.Empty;

            // Damage
            Player.TakeDamage(BaseDamage);
            action += $"dealt {Style.Color(BaseDamage.ToString("0.# dmg"), ColorAnsi.LIGHT_RED)}";

            // Effect
            if (new Random().Next(0, 6) == 0)
            {
                int rng = new Random().Next(0, RandomEffects.Count);
                Player.TakeEffect(RandomEffects[rng]);
                action += $", and cast {Style.Color(RandomEffects[rng].ToString(), ColorAnsi.PINK)} effect";
            }

            return action;
        }

        public void TakeDamage(double damage)
        {
            if (Health - damage <= 0)
            {
                Health = 0;
                combat.PlayerState.SlayedEnemy = true;
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
                TakeEffect(effect);
            }
        }

        public void TakeEffect(Effect effect)
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
