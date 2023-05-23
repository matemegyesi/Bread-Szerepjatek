using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class StatsWindow : Window
    {
        public string Title
        {
            get
            {
                return $" #•> STATISTICS <•# ";
            }
        }

        public StatsWindow()
        {
            Height = 23;
            Width = 44;
        }

        protected override List<string> GenerateLines()
        {
            LineList.Clear();

            // Stats header
            AddBlankLine();
            AlignedText _header = Style.AlignCenterSpaces(Title, Width);
            AddLine(_header.Before + Style.ColorFormat(Title, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + _header.After);

            AddBlankLine();
            AddLine(Style.Color(Style.GetDashedLine(Width), ColorAnsi.GREY));

            AddBlankLine();



            //

            AddBlankLine();
            foreach (var item in Inventory.SpellList)
            {
                AddLine($"{Style.Color(item.Name, ColorAnsi.CORAL)} ({item.ManaCost} mana) --- Effect: {item.Effects}");
                string _dmg = item.Damage > 0 ? Style.Color(item.Damage.ToString(), ColorAnsi.LIGHT_RED) + " dmg" : "";
                string _hp = item.Damage > 0 ? Style.Color("+" + item.Heal.ToString(), ColorAnsi.AQUA) + " hp" : "";
                if (_dmg != "" || _hp != "")
                {
                    AddLine($"{_dmg} | {_hp}");
                }
                AddBlankLine();
            }

            //



            // Fills the remaining lines
            int remainingLineCount = Height - LineList.Count;
            for (int i = 0; i < remainingLineCount; i++)
            {
                AddBlankLine();
            }

            // Last resort: deletes additional lines
            if (LineList.Count > Height)
            {
                LineList.RemoveRange(Height, LineList.Count - Height);
            }

            return LineList;
        }
    }
}
