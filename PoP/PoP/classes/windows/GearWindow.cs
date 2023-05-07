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
            LineList.Clear();

            // Gear header
            AddBlankLine();
            AlignedText _header = Style.AlignCenterSpaces(Title, Width);
            AddLine(_header.Before + Style.ColorFormat(Title, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + _header.After);

            AddBlankLine();
            AddLine(Style.Color(Style.GetDashedLine(Width), ColorAnsi.GREY));
            AddBlankLine();

            // Overall info
            string _defTitle = " OVERALL DEFENCE: ";
            int _defSum = 43;
            AddLine(Style.GetBlankLine(9) + _defTitle + Style.GetRemainingSpace(_defTitle, 18) + Style.ColorFormat(_defSum + " def", ColorAnsi.TEAL, FormatAnsi.HIGHLIGHT));

            string _dmgTitle = " OVERALL DAMAGE: ";
            int _dmgSum = 25;
            AddLine(Style.GetBlankLine(9) + _dmgTitle + Style.GetRemainingSpace(_dmgTitle, 18) + Style.ColorFormat(_dmgSum + " hp", ColorAnsi.RUST, FormatAnsi.HIGHLIGHT));

            AddBlankLine();

            // Individual info
            foreach (var item in Inventory.gear)
            {
                AddLine(GenerateGearCard(item.Key, item.Value));
                AddBlankLine();
            }

            return LineList;
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
    }
}
