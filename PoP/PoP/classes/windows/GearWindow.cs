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
                return $" ¶○> GEAR <○¶ ";
            }
        }

        public bool InUse { get; private set; }

        public GearWindow()
        {
            Height = 36;
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
                LineList.RemoveRange(5, LineList.Count - 5);

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
            AddLineLocal(ref headerLineList, Style.Color(Style.GetDashedLine(Width), ColorAnsi.GREY));
            AddBlankLineLocal(ref headerLineList);

            return headerLineList;
        }

        private List<string> GenerateOverallInfo()
        {
            List<string> overallInfoLineList = new List<string>();
            
            string _dmgTitle = " OVERALL DAMAGE: ";
            AddLineLocal(ref overallInfoLineList, Style.GetBlankLine(9) + _dmgTitle + Style.GetRemainingSpace(_dmgTitle, 18) + Style.ColorFormat(" " + Player.Damage.ToString("0.#") + " dmg ", ColorAnsi.RUST, FormatAnsi.HIGHLIGHT));

            string _defTitle = " OVERALL DEFENCE: ";
            AddLineLocal(ref overallInfoLineList, Style.GetBlankLine(9) + _defTitle + Style.GetRemainingSpace(_defTitle, 18) + Style.ColorFormat(" " + Player.Defence.ToString("0.#") + " def ", ColorAnsi.TEAL, FormatAnsi.HIGHLIGHT));
            
            AddBlankLineLocal(ref overallInfoLineList);

            return overallInfoLineList;
        }

        private string GenerateGearCard(Slot slot, Item item)
        {
            string gearLine = string.Empty;

            string _slot = slot.ToString();
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
