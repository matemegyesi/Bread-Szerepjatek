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

            AddBlankLine();

            string _infoPadding = Style.GetBlankLine(6);
            foreach (var item in GeneratePlayerInfo())
            {
                AddLine(_infoPadding + item);
            }

            return LineList;
        }

        public List<string> GeneratePlayerInfo()
        {
            List<string> infoList = new List<string>();

            return infoList;
        }

        public List<string> GenerateEnemyInfo()
        {
            List<string> infoList = new List<string>();







            return infoList;
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
