using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class GearWindow : Window
    {
        public string Title
        {
            get
            {
                return $" ¶○> GEAR & STATS <○¶ ";
            }
        }
        public string HowToUse
        {
            get
            {
                return "USE: after selecting a spell from the inventory, press the key of the desired equipment slot (Q/W/E/R)";
            }
        }

        public bool InUse { get; private set; }

        public GearWindow()
        {
            Height = 46;
            Width = 44;
        }

        protected override List<string> GenerateLines()
        {
            if (CachedLineList.Count != Height)
            {
                LineList.Clear();

                // Gear header
                foreach (string line in GenerateHeader())
                {
                    AddLine(line);
                }

                // Overall info
                foreach (string line in GenerateOverallInfo())
                {
                    AddLine(line);
                }

                // Individual item info
                foreach (var item in Inventory.gear)
                {
                    AddLine(GenerateGearCard(item.Key, item.Value));
                    AddBlankLine();
                }

                // Individual spell info
                string _spellSlot = "<SPELLS>";
                AddLine(Style.GetRemainingSpace(_spellSlot.Length + 1, 14) + _spellSlot);
                AddBlankLine();

                for (int i = 0; i < 4; i++)
                {
                    Spell _spell = Inventory.sorcery.ElementAt(i);

                    foreach (string line in GenerateSpellCard(_spell, i))
                    {
                        AddLine(line);
                    }
                    AddBlankLine();
                }
            }
            else
            {
                LineList.RemoveRange(8, LineList.Count - 8);

                // Overall info
                foreach (string line in GenerateOverallInfo())
                {
                    AddLine(line);
                }

                // Individual item info
                foreach (var item in Inventory.gear)
                {
                    AddLine(GenerateGearCard(item.Key, item.Value));
                    AddBlankLine();
                }

                // Individual spell info
                string _spellSlot = "<SPELLS>";
                AddLine(Style.GetRemainingSpace(_spellSlot.Length + 1, 14) + _spellSlot);
                AddBlankLine();

                for (int i = 0; i < 4; i++)
                {
                    Spell _spell = Inventory.sorcery.ElementAt(i);

                    foreach (string line in GenerateSpellCard(_spell, i))
                    {
                        AddLine(line);
                    }
                    AddBlankLine();
                }
            }

            return LineList;
        }

        private List<string> GenerateHeader()
        {
            List<string> headerLineList = new List<string>();

            AddBlankLineLocal(ref headerLineList);
            AlignedText _header = Style.AlignCenterSpaces(Title, Width);
            AddLineLocal(ref headerLineList, _header.Before + Style.ColorFormat(Title, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + _header.After);
            
            AddBlankLineLocal(ref headerLineList);
            AddLineLocal(ref headerLineList, HowToUse, true, true);

            AddLineLocal(ref headerLineList, Style.Color(Style.GetDashedLine(Width), ColorAnsi.GREY));
            AddBlankLineLocal(ref headerLineList);

            return headerLineList;
        }

        private List<string> GenerateOverallInfo()
        {
            List<string> overallInfo = new List<string>();

            string _hpTitle = "HEALTH: ";
            AddLineLocal(ref overallInfo, Style.GetRemainingSpace(_hpTitle, 27) + Style.Color(_hpTitle, ColorAnsi.WHITE) + Style.ColorFormat(" " + Player.Health.ToString("0.#") + "/" + Player.MaxHealth.ToString("0.# hp") + " ", ColorAnsi.LIGHT_BLUE, FormatAnsi.HIGHLIGHT));

            AddBlankLineLocal(ref overallInfo);

            string _manaTitle = "MAX MANA: ";
            AddLineLocal(ref overallInfo, Style.GetRemainingSpace(_manaTitle, 27) + Style.Color(_manaTitle, ColorAnsi.WHITE) + Style.Color(Player.MaxMana.ToString("0 mana"), ColorAnsi.PURPLE));

            string _manaRegTitle = "REGENERATION RATE: ";
            AddLineLocal(ref overallInfo, Style.GetRemainingSpace(_manaRegTitle, 27) + Style.Color(_manaRegTitle, ColorAnsi.WHITE) + Style.Color(Player.ManaRate.ToString("0 mana/turn"), ColorAnsi.DARK_RED));

            AddBlankLineLocal(ref overallInfo);
            AddLineLocal(ref overallInfo, Style.GetDashedLine(Width));
            AddBlankLineLocal(ref overallInfo);

            string _dmgTitle = " OVERALL DAMAGE: ";
            AddLineLocal(ref overallInfo, Style.GetBlankLine(9) + Style.Color(_dmgTitle, ColorAnsi.WHITE) + Style.GetRemainingSpace(_dmgTitle, 18) + Style.ColorFormat(" " + Player.Damage.ToString("0.#") + " dmg ", ColorAnsi.RUST, FormatAnsi.HIGHLIGHT));

            string _defTitle = " OVERALL DEFENCE: ";
            AddLineLocal(ref overallInfo, Style.GetBlankLine(9) + Style.Color(_defTitle, ColorAnsi.WHITE) + Style.GetRemainingSpace(_defTitle, 18) + Style.ColorFormat(" " + Player.Defence.ToString("0.#") + " def ", ColorAnsi.TEAL, FormatAnsi.HIGHLIGHT));
            
            AddBlankLineLocal(ref overallInfo);

            return overallInfo;
        }

        private string GenerateGearCard(Slot slot, Item item)
        {
            string gearLine = string.Empty;

            string _slot;
            switch (slot)
            {
                case Slot.MainSword:
                    _slot = "Sword";
                    break;
                case Slot.MainCape:
                    _slot = "Cape";
                    break;
                default:
                    _slot = slot.ToString();
                    break;
            }

            if (item != null)
            {
                string _name = item.Name;
                double _value = 0;
                string _unit = string.Empty;
                ColorAnsi _color = ColorAnsi.BLACK;
                if (item is Weapon)
                {
                    _value = (item as Weapon).Damage;
                    _unit = "dmg";
                    _color = ColorAnsi.LIGHT_RED;
                }
                else if (item is Armor)
                {
                    _value = (item as Armor).Defence;
                    _unit = "def";
                    _color = ColorAnsi.AQUA;
                }

                gearLine += Style.GetRemainingSpace(_slot.Length + 2, 14) + _slot.ToUpper() + ": " + Style.ColorFormat(_name, ColorAnsi.LIGHT_BLUE, FormatAnsi.UNDERLINE);
                gearLine += Style.GetRemainingSpace(14 + _name.Length, 35) + Style.Color($"+{_value} {_unit}", _color);
            }
            else
            {
                gearLine += Style.GetRemainingSpace(_slot.Length + 2, 14) + _slot.ToUpper() + ": " + Style.Color("(Empty)", ColorAnsi.RUST);
            }

            return gearLine;
        }

        private List<string> GenerateSpellCard(Spell spell, int valueCount)
        {
            List<string> spellCard = new List<string>();

            string _value = ' ' + Display.spellKeys.ElementAt(valueCount).Value + ' ';
            if (!InUse)
            {
                _value = Style.ColorFormat(_value, ColorAnsi.DARK_GREY, FormatAnsi.HIGHLIGHT);
            }
            else
            {
                _value = Style.ColorFormat(_value, ColorAnsi.MAGENTA, FormatAnsi.HIGHLIGHT);
            }

            if (spell != null)
            {
                string _cost = spell.ManaCost.ToString("0 mana");

                AddLineLocal(ref spellCard, Style.GetBlankLine(9) + _value + "  " + Style.ColorFormat(spell.Name, ColorAnsi.MAGENTA, FormatAnsi.UNDERLINE) + Style.GetRemainingSpace(14 + Style.PurgeAnsi(_value).Length + spell.Name.Length, 38) + Style.Color(_cost, ColorAnsi.PURPLE));

                AddLineLocal(ref spellCard, Style.Color(spell.Effects, ColorAnsi.PINK) + "  ", false);
            }
            else
            {
                AddLineLocal(ref spellCard, Style.GetBlankLine(9) + _value + "  " + Style.Color("(Empty)", ColorAnsi.RUST));
            }

            return spellCard;
        }

        /// <summary>
        /// Sets the InUse boolean, setting the color of the equipment keys next to the spell names.
        /// </summary>
        /// <param name="isInUse">True activates, false deactivates.</param>
        public void SetInUse(bool isInUse)
        {
            InUse = isInUse;
            HasChanged = true;
        }
        
        /// <summary>
        /// Notifies the window for the next render that the gear has changed.
        /// </summary>
        public void UpdateGear()
        {
            HasChanged = true;
        }
    }
}
