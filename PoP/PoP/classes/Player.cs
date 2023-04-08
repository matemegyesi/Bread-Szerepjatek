using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Player
    {
        public double Damage { get; set; }
        public double Defence { get; set; }
        public int Health { get; private set; }
        public int Mana { get; private set; }

        static public void AttackWithWeapon(Enemy target)
        {
            // target TakeDamage() meghívása, sebzése a Inventory.gear[Slot.HAND]
        }

        static public void AttackWithSpell(Enemy target)
        {
            // target TakeDamage() meghívása
        }

        static public void TakeDamage(int damage)
        {
            // ha Health az <= 0, akkor az ellenség nyer: (Map.CurrentLocation as Combat).ChangeCombatPhase(CombatPhase.ENEMY_WIN);
        }
    }
}
