using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using PoP.classes.effects;

namespace PoP.classes
{
    class Enemy
    {
        public string Name { get; set; }
        public double Damage { get; set; }
        public double Defence { get; set; }

        public double MaxHealth { get; set; }
        public double Health { get; set; }

        public int MaxMana { get; set; }
        public int Mana { get; set; }

        public int Level { get; set; }

        public Dictionary<Effect, int> EffectDict = new Dictionary<Effect, int>()
        {
            { new Burn(), 0 },
            { new Freeze(), 0 },
            { new Stun(), 0 },
            { new Poison(), 0 },
            { new Bleed(), 0 },
            { new Buff(), 0 },
            { new Debuff(), 0 }
        };


        public Dictionary<string, string> data { get; set; }
        public Enemy(Dictionary<string, object> data)
        {
            Name = data["name"].ToString();
            Damage = double.Parse(data["damage"].ToString());
            Defence = double.Parse(data["defence"].ToString());
            MaxHealth = double.Parse(data["health"].ToString());
            Health = MaxHealth;
            Level = int.Parse(data["level"].ToString());
        }

        public string TakeAction()
        {
            string action = $"The enemy dealt {Damage} damage.";

            // itt lesz az AI-a majd az enemy-nek, egyelőre csak az EnemyAttack()-ot kell meghívni

            return action;
        }

        public void EnemyAttack()
        {
            // Player TakeDamage()-ét kell meghívni, aminek a paramétere Damage
        }

        public void TakeDamage(int damage)
        {
            // ha Health <= 0, akkor a játékos nyer: (Map.CurrentLocation as Combat).ChangeCombatPhase(CombatPhase.PLAYER_WIN);
        }
    }
}
