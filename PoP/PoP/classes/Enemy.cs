﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace PoP.classes
{
    class Enemy
    {
        public string Name { get; set; }
        public double Damage { get; set; }
        public double Defence { get; set; }
        public double Health { get; set; }
        public int Level { get; set; }

        public Dictionary<string, string> data { get; set; }
        public Enemy(string path)
        {
            string json = File.ReadAllText(path);
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            Name = data["name"];
            Damage = double.Parse(data["damage"]);
            Defence = double.Parse(data["defence"]);
            Health = double.Parse(data["health"]);
            Level = int.Parse(data["level"]);
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
