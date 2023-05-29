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

        private List<string> playerLines = new List<string>(22);
        private List<string> enemyLines = new List<string>(22);

        // Change detection
        private bool playerChanged;
        private bool enemyChanged;
        private bool loadoutChanged;

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
            GeneratePlayerColumn();
            GenerateEnemyColumn();

            // Player & Enemy column section
            for (int i = 0; i < playerLines.Count; i++)
            {
                AddLine(INFO_PADDING + playerLines[i] + enemyLines[i]);
            }

            AddLine(Style.Color(' ' + Style.GetBlankLine(Width - 2, Border.SINGLE_HORIZONTAL) + ' ', ColorAnsi.DARK_GREY));

            // Loadout section




            return LineList;
        }

        private List<string> GeneratePlayerColumn()
        {
            List<string> playerColumn = new List<string>();
            Width = 68;

            // Name
            AddLineLocal(ref playerColumn, GenerateName(Player.Name, true));

            AddBlankLineLocal(ref playerColumn);

            // Health
            foreach (string line in GenerateSlider("Health", Player.Health, Player.MaxHealth, true, Style.HealthBarColor))
            {
                AddLineLocal(ref playerColumn, line);
            }

            AddBlankLineLocal(ref playerColumn);

            // Defence
            AddLineLocal(ref playerColumn, GenerateInfo("DEFENCE: ", Player.Defence.ToString("0 def"), true, Style.DefenceBarColor));

            AddBlankLineLocal(ref playerColumn);

            // Effect
            foreach (string line in GenerateEffectIndicator(Player.EffectDict, true))
            {
                AddLineLocal(ref playerColumn, line);
            }

            AddBlankLineLocal(ref playerColumn);

            // Mana
            foreach (string line in GenerateSlider($"Mana", Player.Mana, Player.MaxMana, true, Style.ManaColor))
            {
                AddLineLocal(ref playerColumn, line);
            }

            AddBlankLineLocal(ref playerColumn);
            
            // Mana regeneration rate
            AddLineLocal(ref playerColumn, GenerateInfo("MANA REGENERATION: ", Player.ManaRate.ToString("+0 mana/turn"), true, Style.ManaRegColor));

            // Fills up the remaining lines
            while (playerColumn.Count < 22)
            {
                AddBlankLineLocal(ref playerColumn);
            }

            Width = 148;
            playerLines = playerColumn;
            return playerColumn;
        }

        private List<string> GenerateEnemyColumn()
        {
            List<string> enemyColumn = new List<string>();
            Width = 68;

            // Name
            AddLineLocal(ref enemyColumn, GenerateName(enemy.Name, false));

            AddBlankLineLocal(ref enemyColumn);

            // Health
            foreach (string line in GenerateSlider("Health", enemy.Health, enemy.MaxHealth, false, Style.HealthBarColor))
            {
                AddLineLocal(ref enemyColumn, line);
            }

            AddBlankLineLocal(ref enemyColumn);

            // Defence
            AddLineLocal(ref enemyColumn, GenerateInfo("DEFENCE: ", enemy.Defence.ToString("0 def"), false, Style.DefenceBarColor));

            AddBlankLineLocal(ref enemyColumn);

            // Effect
            foreach (string line in GenerateEffectIndicator(enemy.EffectDict, false))
            {
                AddLineLocal(ref enemyColumn, line);
            }

            AddBlankLineLocal(ref enemyColumn);

            // Fills up the remaining lines
            while (enemyColumn.Count < 22)
            {
                AddBlankLineLocal(ref enemyColumn);
            }

            Width = 148;
            enemyLines = enemyColumn;
            return enemyColumn;
        }

        private string GenerateName(string name, bool alignedLeft)
        {
            return (alignedLeft ? "" : Style.GetRemainingSpace(name.Length + 2, 68)) + Style.Color('<', ColorAnsi.WHITE) + Style.ColorFormat(name, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + Style.Color('>', ColorAnsi.WHITE);
        }

        private List<string> GenerateSlider(string name, double value, double maxValue, bool alignedLeft, ColorAnsi color)
        {
            List<string> sliderList = new List<string>();

            // Slider name
            AddLineLocal(ref sliderList, Style.Color($"{name.ToUpper()} [{value}/{maxValue}]", ColorAnsi.WHITE), alignedLeft);

            // Slider
            int _sliderLength = 40;
            double _percent;
            if (maxValue != 0)
            {
                _percent = value / maxValue;
            }
            else
            {
                _percent = 0;
            }
            AddLineLocal(ref sliderList, Style.Color('┇', color) + Style.GetSlider(_sliderLength, _percent, color) + Style.Color('┇', color), alignedLeft);

            return sliderList;
        }

        private string GenerateInfo(string name, string value, bool alignedLeft, ColorAnsi color)
        {
            string info = string.Empty;

            if (!alignedLeft)
                info += Style.GetRemainingSpace(name + value, Width);

            info += Style.Color(name, ColorAnsi.WHITE) + Style.Color(value, color);

            if (alignedLeft)
                info += Style.GetRemainingSpace(name + value, Width);

            return info;
        }

        private List<string> GenerateEffectIndicator(Dictionary<Effect, int> effectDict, bool alignedLeft)
        {
            List<string> effectIndicatorList = new List<string>();

            string _fxTitle = "ACTIVE EFFECT(S): ";

            string _currentLine = string.Empty;
            _currentLine += Style.Color(_fxTitle, ColorAnsi.WHITE);

            if (effectDict.Count(x => x.Value > 0) > 0)
            {
                for (int i = 0; i < effectDict.Count; i++)
                {
                    if (i != 0)
                    {
                        _currentLine += Style.GetBlankLine(_fxTitle.Length);
                    }

                    KeyValuePair<Effect, int> _current = effectDict.ElementAt(i);

                    if (_current.Value > 0)
                    {
                        _currentLine += Style.Color(_current.Key.ToString(), ColorAnsi.PINK) + Style.GetRemainingSpace(_current.Key.ToString(), 7);
                        _currentLine += " -  " + Style.Color(_current.Value + " ", ColorAnsi.ORANGE);
                        if (_current.Value == 1)
                        {
                            _currentLine += Style.Color("turn left", ColorAnsi.CORAL);
                        }
                        else
                        {
                            _currentLine += Style.Color("turns left", ColorAnsi.CORAL);
                        }

                        if (!alignedLeft)
                        {
                            _currentLine = _currentLine.Insert(0, Style.GetBlankLine(27));
                        }
                        AddLineLocal(ref effectIndicatorList, _currentLine);
                    }
                    _currentLine = string.Empty;
                }
            }
            else
            {
                _currentLine += Style.Color("(Empty)", ColorAnsi.RUST);

                if (!alignedLeft)
                {
                    _currentLine = _currentLine.Insert(0, Style.GetBlankLine(27));
                }
                AddLineLocal(ref effectIndicatorList, _currentLine);
            }

            return effectIndicatorList;
        }

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
        }

    }
}
