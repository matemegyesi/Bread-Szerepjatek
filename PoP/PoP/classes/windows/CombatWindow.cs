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

            // Hp bar
            int _hpLength = 30;
            double _hpPercent = Player.Health / (double)Player.MaxHealth;
            ColorAnsi _hpColor = ColorAnsi.LIGHT_BLUE;

            infoList.Add(Style.Color($"HEALTH [{Player.Health}/{Player.MaxHealth}]", ColorAnsi.WHITE));
            infoList.Add(Style.Color('┇', _hpColor) + Style.GetSlider(_hpLength, _hpPercent, _hpColor) + Style.Color('┇', _hpColor));

            // Break
            infoList.Add(string.Empty);

            // Mana bar
            int _manaLength = 30;
            double _manaPercent = Player.Mana / (double)Player.MaxMana;
            ColorAnsi _manaColor = ColorAnsi.PURPLE;

            infoList.Add(Style.Color($"MANA [{Player.Mana}/{Player.MaxMana}]", ColorAnsi.WHITE));
            infoList.Add(Style.Color('┇', _manaColor) + Style.GetSlider(_manaLength, _manaPercent, _manaColor) + Style.Color('┇', _manaColor));



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
