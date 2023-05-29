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

            // Name
            string _playerName = Player.Name;
            string _enemyName = enemy.Name;
            string _nameSeparation = Style.GetRemainingSpace(INFO_PADDING.Length * 2 + _playerName.Length + _enemyName.Length + 4, Width);
            _playerName = Style.Color('<', ColorAnsi.WHITE) + Style.ColorFormat(_playerName, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + Style.Color('>', ColorAnsi.WHITE);
            _enemyName = Style.Color('<', ColorAnsi.WHITE) + Style.ColorFormat(_enemyName, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + Style.Color('>', ColorAnsi.WHITE);

            AddLine(INFO_PADDING + _playerName + _nameSeparation + _enemyName + INFO_PADDING);
            AddBlankLine();

            // Health
            List<string> playerHp = GenerateSlider("Health", Player.Health, Player.MaxHealth, true, Style.HealthBarColor);
            List<string> enemyHp = GenerateSlider("Health", enemy.Health, enemy.MaxHealth, false, Style.HealthBarColor);
            for (int i = 0; i < playerHp.Count; i++)
            {
                AddLine(INFO_PADDING + playerHp[i] + enemyHp[i] + INFO_PADDING);
            }

            AddBlankLine();

            // Defence
            string _playerDef = GenerateInfo("DEFENCE: ", Player.Defence.ToString("0 def"), true, Style.DefenceBarColor);
            string _enemyDef = GenerateInfo("DEFENCE: ", enemy.Defence.ToString("0 def"), false, Style.DefenceBarColor);
            AddLine(INFO_PADDING + _playerDef + _enemyDef + INFO_PADDING);

            AddBlankLine();
            
            // (Player) mana
            foreach (string line in GenerateSlider($"Mana", Player.Mana, Player.MaxMana, true, Style.ManaColor))
            {
                AddLine(INFO_PADDING + line);
            }

            AddBlankLine();

            // (Player) mana regeneration
            string _playerManaReg = GenerateInfo("MANA REGENERATION: ", Player.ManaRate.ToString("+0 mana/turn"), true, Style.ManaRegColor);
            AddLine(INFO_PADDING + _playerManaReg);

            AddBlankLine();

            // Effects
            List<string> _playerFx = GenerateEffectIndicator(Player.EffectDict, true);
            List<string> _enemyFx = GenerateEffectIndicator(enemy.EffectDict, false);

            for (int i = 0; i < _playerFx.Count; i++)
            {
                AddLine(INFO_PADDING + _playerFx[i] + _enemyFx[i]);
            }

            while (LineList.Count < 22)
            {
                AddBlankLine();
            }

            AddLine(Style.GetBlankLine(Width, Border.SINGLE_HORIZONTAL));


            return LineList;
        }

        private List<string> GenerateSlider(string name, double value, double maxValue, bool alignedLeft, ColorAnsi color)
        {
            List<string> sliderList = new List<string>();
            Width = 68;

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

            Width = 148;
            return sliderList;
        }

        private string GenerateInfo(string name, string value, bool alignedLeft, ColorAnsi color)
        {
            string info = string.Empty;
            Width = 68;

            if (!alignedLeft)
                info += Style.GetRemainingSpace(name + value, Width);

            info += Style.Color(name, ColorAnsi.WHITE) + Style.Color(value, color);

            if (alignedLeft)
                info += Style.GetRemainingSpace(name + value, Width);

            Width = 148;
            return info;
        }

        private List<string> GenerateEffectIndicator(Dictionary<Effect, int> effectDict, bool alignedLeft)
        {
            List<string> effectIndicatorList = new List<string>();

            string _fxTitle = "ACTIVE EFFECT(S): ";
            Width = 68;

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
                        _currentLine += " -   " + Style.Color(_current.Value + " ", ColorAnsi.ORANGE);
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

            while (effectIndicatorList.Count < 7)
            {
                AddBlankLineLocal(ref effectIndicatorList);
            }

            Width = 148;
            return effectIndicatorList;
        }

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
        }

    }
}
