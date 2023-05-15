﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Player
    {
        static public double Damage { get; set; }
        static public double Defence { get; set; }

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
