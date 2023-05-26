using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Player
    {
        static public double Damage
        {
            get
            {
                double damage = 0;
                if (Inventory.gear[Slot.Hand] != null)
                    damage += (Inventory.gear[Slot.Hand] as Weapon).Damage;
                if (Inventory.gear[Slot.Ring] != null)
                    damage += (Inventory.gear[Slot.Ring] as Weapon).Damage;
                if (Inventory.gear[Slot.MainSword] != null)
                    damage += (Inventory.gear[Slot.MainSword] as Weapon).Damage;
                return damage;
            }
        }
        static public double Defence
        {
            get
            {
                double defence = 0;
                if (Inventory.gear[Slot.Head] != null)
                    defence += (Inventory.gear[Slot.Head] as Armor).Defence;
                
                if (Inventory.gear[Slot.Chest] != null)
                    defence += (Inventory.gear[Slot.Chest] as Armor).Defence;


                if (Inventory.gear[Slot.Leg] != null)
                    defence += (Inventory.gear[Slot.Leg] as Armor).Defence;


                if (Inventory.gear[Slot.MainCape] != null)
                    defence += (Inventory.gear[Slot.MainCape] as Armor).Defence;

                return defence;
            }
        }

        static public double MaxHealth { get; private set; } = 50;
        static public double Health { get; private set; } = 44;

        static public int MaxMana { get; private set; } = 20;
        static public int Mana { get; private set; } = 9;

        public Player()
        {
            
        }

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
