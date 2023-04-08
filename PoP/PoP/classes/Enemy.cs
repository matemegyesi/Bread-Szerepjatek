using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Enemy
    {
        public string Name { get; set; }
        public double Damage { get; set; }
        public double Defence { get; set; }
        public double Health { get; set; }
    
        public void EnemyAttack()
        {

        }

        public void TakeDamage(int damage)
        {
            // ha Health <= 0, akkor a játékos nyer: (Map.CurrentLocation as Combat).ChangeCombatPhase(CombatPhase.PLAYER_WIN);
        }
    }
}
