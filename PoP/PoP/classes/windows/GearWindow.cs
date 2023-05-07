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

        public GearWindow()
        {
            Height = 22;
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

                // Individual info
                foreach (var item in Inventory.gear)
                {
                    AddLine(GenerateGearCard(item.Key, item.Value));
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

                // Individual info
                foreach (var item in Inventory.gear)
                {
                    AddLine(GenerateGearCard(item.Key, item.Value));
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

            double _dmgSum = 0;
            double _defSum = 0;

            foreach (var item in Inventory.gear)
            {
                if (item.Value is Weapon)
                {
                    _dmgSum += (item.Value as Weapon).Damage;
                }
                else if (item.Value is Armor)
                {
                    _defSum += (item.Value as Armor).Defense;
                }
            }

            string _dmgTitle = " OVERALL DAMAGE: ";
            AddLineLocal(ref overallInfoLineList, Style.GetBlankLine(9) + _dmgTitle + Style.GetRemainingSpace(_dmgTitle, 18) + Style.ColorFormat(_dmgSum + " hp", ColorAnsi.RUST, FormatAnsi.HIGHLIGHT));

            string _defTitle = " OVERALL DEFENCE: ";
            AddLineLocal(ref overallInfoLineList, Style.GetBlankLine(9) + _defTitle + Style.GetRemainingSpace(_defTitle, 18) + Style.ColorFormat(_defSum + " def", ColorAnsi.TEAL, FormatAnsi.HIGHLIGHT));
            
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
                    _value = (item as Armor).Defense;
                    _unit = "def";
                    _color = ColorAnsi.AQUA;
                }

                gearLine += Style.GetRemainingSpace(_slot.Length + 2, 14) + _slot.ToUpper() + ": " + Style.ColorFormat(_name, ColorAnsi.WHEAT, FormatAnsi.UNDERLINE);
                gearLine += Style.GetRemainingSpace(14 + _name.Length, 30) + Style.Color($"+{_value} {_unit}", _color);
            }
            else
            {
                gearLine += Style.GetRemainingSpace(_slot.Length + 2, 14) + _slot.ToUpper() + ": " + Style.Color("(Empty)", ColorAnsi.RUST);
            }

            return gearLine;
        }

        /// <summary>
        /// It notifies the window for the next render that the gear has changed.
        /// </summary>
        public void UpdateGear()
        {
            HasChanged = true;
        }
    }
}
