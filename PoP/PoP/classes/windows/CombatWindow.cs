using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class CombatWindow : Window
    {

        public string Title
        {
            get
            {
                return $" >>> Combat <<< ";
            }
        }
        public string HowToUse
        {
            get
            {
                return "USE: (...)";
            }
        }

        public CombatWindow()
        {
            Height = 46;
            Width = 148;
        }

        protected override List<string> GenerateLines()
        {
            LineList.Clear();



            return LineList;
        }

        public void ShowLoadout()
        {

        }

        public void ShowCombat()
        {

        }

        public void ShowResolution()
        {

        }
    }
}
