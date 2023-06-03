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
        public bool IsStunned { get; set; }
        public bool CanHeal { get; set; }
        public bool CanCastEffect { get; set; }

        private Combat combat;
        private Random rng = new Random();

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

            if (!IsStunned)
            {
                if (rng.Next(0, 3) == 0 && Health < MaxHealth / 4 && CanHeal)
                {
                    action += EnemyHeal();
                }
                else
                {
                    action += EnemyAttack();
                }
            }
            else
            {
                action += $"was knocked out";
            }

            return action + '.';
        }

        public string EnemyAttack()
        {
            string action = string.Empty;

            // Damage
            double potentialDamage = Damage;
            double randomDouble = rng.NextDouble() * (1.2 - .8) + .8;
            if (Damage > Player.Defence)
            {
                potentialDamage -= Math.Sqrt(Damage - Player.Defence);
            }
            potentialDamage *= randomDouble;

            Player.TakeDamage(potentialDamage);
            action += $"dealt {Style.Color(potentialDamage.ToString("0.# dmg"), ColorAnsi.LIGHT_RED)}";

            // Effect
            if (CanCastEffect)
            {
                if (rng.Next(0, 5) == 0)
                {
                    int randomInt = rng.Next(0, RandomEffects.Count);

                    if (RandomEffects[randomInt] != Effect.Buff)
                    {
                        Player.TakeEffect(RandomEffects[randomInt]);
                        action += $", and cast {Style.Color(RandomEffects[randomInt].ToString(), ColorAnsi.PINK)} effect";
                    }
                    else
                    {
                        EffectDict[Effect.Buff] = 3;
                        action += $", and cast {Style.Color(RandomEffects[randomInt].ToString(), ColorAnsi.PINK)} effect";
                    }
                }
            }

            return action;
        }

        public string EnemyHeal()
        {
            string action = string.Empty;

            double _heal = Player.BaseDamage / 3 * (rng.NextDouble() * (3 - 1.5) + 1.5);
            if (Health + _heal > MaxHealth)
            {
                Health = MaxHealth;
                action += $"fully {Style.Color("healed", ColorAnsi.LIGHT_BLUE)} back";
            }
            else
            {
                Health += _heal;
                action += $"healed back {Style.Color(_heal.ToString("0.# hp"), ColorAnsi.LIGHT_BLUE)}";
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
            else if (effect != Effect.Buff)
            {
                EffectDict[effect] = 3;
            }
        }
    }
}
