using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class MenuWindow : Window
    {
        public string HowToUse
        {
            get
            {
                return "Press any key to continue";
            }
        }

        public MenuWindow()
        {
            Height = 62;
            Width = 237;
        }

        protected override List<string> GenerateLines()
        {
            //LineList.Clear();

            AddBlankLine(3);

            // Title / Logo
            foreach (string line in FileInput.GetAllLines("res\\pop_logo.txt"))
            {
                string _logo = Style.GetBlankLine(77) + Style.Color(line, ColorAnsi.RED);
                AddLine(_logo);
            }

            AddBlankLine(3);

            // Tutorial
            foreach (string line in FileInput.GetAllLines("res\\tutorial.txt"))
            {
                string _tutorial = string.Empty;

                if (line.Contains('━'))
                {
                    string[] _info = line.Split(' ');

                    _tutorial = Style.GetBlankLine(90) + Style.Color(_info[0], ColorAnsi.CORAL) + ' ' + Style.ColorFormat(_info[1], ColorAnsi.CORAL, FormatAnsi.UNDERLINE) + ' ' + Style.Color(_info[2], ColorAnsi.CORAL);
                }
                else if (line.Contains(':'))
                {
                    string[] _info = line.Split(':');

                    _tutorial = Style.GetBlankLine(90) + Style.Color(_info[0] + ':', ColorAnsi.LIGHT_BLUE) + Style.Color(_info[1], ColorAnsi.WHITE);
                }
                else if (line.Contains('└'))
                {
                    _tutorial = Style.GetBlankLine(90) + Style.Color('└', ColorAnsi.LIGHT_BLUE) + Style.Color(line.Substring(1), ColorAnsi.WHITE);
                }
                else
                {
                    _tutorial = line;
                }

                AddLine(_tutorial);
            }

            AddBlankLine(3);

            // Continue
            AddLine(Style.GetRemainingSpace(HowToUse.Length / 2, Width / 2) + HowToUse);

            return LineList;
        }
    }
}
