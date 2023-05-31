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
        //public static double DamageActual { get; private set; }

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
        //public static double DefenceActual { get; private set; }

        public static double MaxHealth { get; private set; }
        public static double Health { get; private set; }

        public static double MaxMana { get; private set; }
        public static double Mana { get; private set; }

        public static double BaseManaRate { get; private set; }
        public static double ManaRate { get; private set; }

        public static Dictionary<Effect, int> EffectDict { get; set; } = new Dictionary<Effect, int>()
        {
            { Effect.Burn, 0 },
            { Effect.Freeze, 0 },
            { Effect.Stun, 0 },
            { Effect.Poison, 0 },
            { Effect.Bleed, 0 },
            { Effect.Buff, 0 },
            { Effect.Debuff, 0 }
        };

        public static void Init()
        {
            MaxHealth = 100;
            Health = MaxHealth;

            MaxMana = 75;
            Mana = MaxMana;

            BaseManaRate = 15;
        }

        public static void AttackWithWeapon(Enemy target)
        {
            target.TakeDamage(Damage);
        }

        public static void AttackWithSpell(Enemy target, Spell spell)
        {
            target.TakeSpell(spell);

            if (spell.Heal != 0)
            {
                if (Health + spell.Heal > MaxHealth)
                {
                    Health = MaxHealth;
                }
                else if (Health + spell.Heal <= 0)
                {
                    Health = 1;
                }
                else
                {
                    Health += spell.Heal;
                }
            }
        }

        public static void TakeDamage(double damage)
        {
            if (Health - damage <= 0)
            {
                Health = 0;
                (Map.CurrentLocation as Combat).ChangeCombatState((Map.CurrentLocation as Combat).LoseState);
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

        public static void RegenerateMana()
        {
            if (Mana + ManaRate > MaxMana)
            {
                Mana = MaxMana;
            }
            else
            {
                Mana += ManaRate;
            }
        }
    }
}
