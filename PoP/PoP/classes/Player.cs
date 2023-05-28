using PoP.classes.effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Player
    {
        public static string Name = "Player";

        public static double Damage
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
        public static double Defence
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

        public static double MaxHealth { get; private set; } = 50;
        public static double Health { get; private set; } = 44;

        public static int MaxMana { get; private set; } = 20;
        public static int Mana { get; private set; } = 9;
        public static int ManaRate { get; private set; } = 5;

        public static Dictionary<Effect, int> EffectDict = new Dictionary<Effect, int>()
        {
            { new Burn(), 0 },
            { new Freeze(), 0 },
            { new Stun(), 0 },
            { new Poison(), 0 },
            { new Bleed(), 0 },
            { new Buff(), 0 },
            { new Debuff(), 0 }
        };

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
