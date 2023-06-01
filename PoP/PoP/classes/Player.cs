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

        public static double BaseDamage
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
        public static double Damage { get; set; }

        public static double BaseDefence
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
        public static double Defence { get; set; }

        public static double MaxHealth { get; private set; }
        public static double Health { get; private set; }

        public static double MaxMana { get; private set; }
        public static double Mana { get; private set; }

        public static double BaseManaRate { get; private set; }
        public static double ManaRate { get; set; }

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
        public static bool IsStunned { get; set; }
        public static List<bool> PoisonedSpells { get; set; } = new List<bool>(4)
        {
            { false },
            { false },
            { false },
            { false }
        };

        public static void Init()
        {
            Damage = BaseDamage;

            Defence = BaseDefence;

            MaxHealth = 100;
            Health = MaxHealth;

            MaxMana = 75;
            Mana = MaxMana;

            BaseManaRate = 15;
            ManaRate = BaseManaRate;
        }

        public static string AttackWithWeapon(Enemy target)
        {
            string action = string.Empty;

            target.TakeDamage(BaseDamage);
            action += $"dealt {Style.Color(BaseDamage.ToString("0.# dmg"), ColorAnsi.LIGHT_RED)}";

            return action + '.';
        }

        public static string AttackWithSpell(Enemy target, Spell spell)
        {
            string action = string.Empty;

            target.TakeSpell(spell);
            action += $"cast the {Style.Color(spell.Name, ColorAnsi.MAGENTA)} spell";

            if (Mana - spell.ManaCost <= 0)
            {
                Mana = 0;
            }
            else
            {
                Mana -= spell.ManaCost;
            }

            if (spell.Heal != 0)
            {
                if (Health + spell.Heal > MaxHealth)
                {
                    Health = MaxHealth;
                    action += $" that healed them to {Style.Color("maximum health", ColorAnsi.LIGHT_BLUE)}";
                }
                else if (Health + spell.Heal <= 1)
                {
                    Health = 1;
                    action += $" that {Style.Color("critically damaged", ColorAnsi.RED)} them";
                }
                else
                {
                    Health += spell.Heal;
                    if (spell.Heal > 0)
                    {
                        action += $" that healed them (+{Style.Color(spell.Heal.ToString("0.# hp"), ColorAnsi.LIGHT_BLUE)})";
                    }
                    else
                    {
                        action += $" that damaged them ({Style.Color(spell.Heal.ToString("0.# hp"), ColorAnsi.LIGHT_RED)})";
                    }
                }
            }

            if (spell.EffectList.Contains(Effect.Buff))
            {
                EffectDict[Effect.Buff] = 3;
            }

            return action + '.';
        }

        public static void TakeDamage(double damage)
        {
            if (Health - damage <= 0)
            {
                Health = 0;
                (Map.CurrentLocation as Combat).EnemyState.SlayedPlayer = true;
            }
            else
            {
                Health -= damage;
            }
        }

        public static void TakeEffect(Effect effect)
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
        
        public static void RegenerateMana()
        {
            if (Mana + BaseManaRate > MaxMana)
            {
                Mana = MaxMana;
            }
            else
            {
                Mana += BaseManaRate;
            }
        }

        public static void SetPosionedSpells(bool arePoisoned)
        {
            if (!arePoisoned)
            {
                PoisonedSpells[0] = false;
                PoisonedSpells[1] = false;
                PoisonedSpells[2] = false;
                PoisonedSpells[3] = false;
            }
            else
            {
                PoisonedSpells[0] = true;
                PoisonedSpells[1] = true;
                PoisonedSpells[2] = true;
                PoisonedSpells[3] = true;
            }
        }
    }
}
