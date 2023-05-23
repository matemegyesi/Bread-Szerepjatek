using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class CombatWindow : Window
    {
        private const string INFO_PADDING = "      ";

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

        private Enemy enemy;

        public CombatWindow()
        {
            Height = 46;
            Width = 148;

            
        }

        protected override List<string> GenerateLines()
        {
            LineList.Clear();

            AddBlankLine();
            AddBlankLine();

            List<string> playerInfo = GenerateInfo("Player", Player.Health, Player.MaxHealth, Player.Mana, Player.MaxMana, true);
            List<string> enemyInfo = GenerateInfo("Enemy", enemy.Health, enemy.MaxHealth, enemy.Mana, enemy.MaxMana, false);

            for (int i = 0; i < playerInfo.Count; i++)
            {
                AddLine(INFO_PADDING + playerInfo[i] + enemyInfo[i] + INFO_PADDING);
            }

            return LineList;
        }

        private List<string> GenerateInfo(string name, double hp, double hpMax, int mana, int manaMax, bool alignedLeft, bool isCat = false)
        {
            List<string> infoList = new List<string>();
            Width = 68;

            // Entity name
            AddLineLocal(ref infoList, '<' + Style.ColorFormat(' ' + name.ToUpper() + ' ', ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + '>', alignedLeft);

            AddLineLocal(ref infoList, string.Empty);

            // Hp bar
            int _hpLength = 40;
            double _hpPercent;
            if (hpMax != 0)
            {
                _hpPercent = hp / hpMax;
            }
            else
            {
                _hpPercent = 0;
            }
            ColorAnsi _hpColor = ColorAnsi.LIGHT_BLUE;

            AddLineLocal(ref infoList, Style.Color($"HEALTH [{hp}/{hpMax}]", ColorAnsi.WHITE), alignedLeft);
            AddLineLocal(ref infoList, Style.Color('┇', _hpColor) + Style.GetSlider(_hpLength, _hpPercent, _hpColor) + Style.Color('┇', _hpColor), alignedLeft);

            AddLineLocal(ref infoList, string.Empty);

            // Mana bar
            int _manaLength = 40;
            double _manaPercent;
            if (manaMax != 0)
            {
                _manaPercent = (double)mana / manaMax;
            }
            else
            {
                _manaPercent = 0;
            }
            ColorAnsi _manaColor = ColorAnsi.PURPLE;

            AddLineLocal(ref infoList, Style.Color($"MANA [{mana}/{manaMax}]", ColorAnsi.WHITE), alignedLeft);
            AddLineLocal(ref infoList, Style.Color('┇', _manaColor) + Style.GetSlider(_manaLength, _manaPercent, _manaColor) + Style.Color('┇', _manaColor), alignedLeft);


            Width = 148;
            return infoList;
        }

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
        }

    }
}
