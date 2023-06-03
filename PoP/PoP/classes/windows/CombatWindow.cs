using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class CombatWindow : Window
    {
        // Constants
        private const string INFO_PADDING = "            ";
        private const string CARD_PADDING = "    ";

        private const int COLUMN_WIDTH = 62;
        private const int COLUMN_HEIGHT = 20;
        private const int SPELL_WIDTH = 30;

        // Borders
        private Border cardTop = new Border(true ,true ,false, true, true);
        private Border cardBottom = new Border(true);

        // Line lists
        private List<string> playerLines = new List<string>(COLUMN_HEIGHT);
        private List<string> enemyLines = new List<string>(COLUMN_HEIGHT);

        private List<List<string>> loadout = new List<List<string>>(4)
        {
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>()
        };
        
        private Enemy enemy;

        // Window settings
        public string FKeyName { get; set; }
        public string SpaceKeyName { get; set; }
        public string TurnTitle { get; set; }

        // Combat settings
        private Combat combat;

        // Change detection
        private bool playerChanged;
        private bool enemyChanged;
        private bool loadoutChanged;

        public CombatWindow()
        {
            Height = 46;
            Width = 148;

            // Spell card border setup
            cardTop.TopLeft = Border.CURVED_TOPLEFT;
            cardTop.TopRight = Border.CURVED_TOPRIGHT;

            cardBottom.BottomLeft = Border.CURVED_BOTTOMLEFT;
            cardBottom.BottomRight = Border.CURVED_BOTTOMRIGHT;
            cardBottom.IntersectionList.Add(new Intersection(BorderSide.Top, 5, Border.SINGLE_T_BOTTOM));
            cardBottom.IntersectionList.Add(new Intersection(BorderSide.Top, 26, Border.SINGLE_T_BOTTOM));
        }

        protected override List<string> GenerateLines()
        {
            LineList.Clear();
            GeneratePlayerColumn();
            GenerateEnemyColumn();

            // Header
            AddBlankLine();
            string header = GenerateHeader();
            AddLine(header);

            AddBlankLine();

            // Player & Enemy column section
            for (int i = 0; i < playerLines.Count; i++)
            {
                AddLine(INFO_PADDING + playerLines[i] + enemyLines[i]);
            }

            // (Hr)
            AddLine(Style.Color(Style.GetBlankLine(INFO_PADDING.Length / 2 + 2 - 1) + '~' + Style.GetBlankLine(Width - INFO_PADDING.Length - 4, Border.SINGLE_HORIZONTAL) + '~', ColorAnsi.DARK_GREY));
            AddBlankLine();

            // Menu section
            string menu = GenerateMenu();
            AddLine(INFO_PADDING + menu);

            AddBlankLine();

            // (Hr)
            AddLine(Style.Color(Style.GetBlankLine(INFO_PADDING.Length / 2 - 1) + '~' + Style.GetBlankLine(Width - INFO_PADDING.Length, Border.SINGLE_HORIZONTAL) + '~', ColorAnsi.DARK_GREY));
            AddBlankLine();

            // Weapon section
            foreach (string line in GenerateWeapon())
            {
                AddLine(INFO_PADDING + line);
            }

            AddBlankLine();

            // (Hr)
            AddLine(Style.Color(Style.GetBlankLine(INFO_PADDING.Length / 2 - 2 - 1) + '~' + Style.GetBlankLine(Width - INFO_PADDING.Length + 4, Border.SINGLE_HORIZONTAL) + '~', ColorAnsi.DARK_GREY));
            AddBlankLine();

            // Loadout section
            GenerateLoadout();

            for (int i = 0; i < loadout[0].Count; i++)
            {
                AddLine(CARD_PADDING + loadout[0][i] + CARD_PADDING + loadout[1][i] + CARD_PADDING + loadout[2][i] + CARD_PADDING + loadout[3][i] + CARD_PADDING);
            }

            return LineList;
        }

        private string GenerateHeader()
        {
            string header = string.Empty;

            string _turn = TurnTitle;
            header += Style.GetRemainingSpace(Style.PurgeAnsi(_turn).Length / 2, Width / 2);
            header += Style.GetFormat(FormatAnsi.HIGHLIGHT) + _turn;

            return header;
        }

        private List<string> GenerateSpellCard(Spell spell, char key, bool enabled, bool poisoned)
        {
            List<string> spellCardList = new List<string>();
            Width = SPELL_WIDTH;
            
            string _padding = Style.GetBlankLine(5);
            List<string> topList = new List<string>();
            List<string> bottomList = new List<string>();

            if (spell != null)
            {
                #region TOP LIST
                
                // Key
                Width = SPELL_WIDTH - 10;

                // Text
                string _key;
                if (!poisoned)
                {
                    _key = " [" + key.ToString() + "] ";
                }
                else
                {
                    _key = "~POISONED~";
                }
                string _keyCenter = Style.GetRemainingSpace(_key.Length / 2, Width / 2);

                // Color
                if (Player.Mana < spell.ManaCost && !poisoned)
                    enabled = false;

                if (poisoned)
                {
                    if (enabled)
                    {
                        _key = Style.Color(_key, ColorAnsi.CORAL);
                    }
                    else
                    {
                        _key = Style.Color(_key, ColorAnsi.DARK_GREY);
                    }
                }
                else
                {
                    if (enabled)
                    {
                        _key = Style.ColorFormat(_key, ColorAnsi.RED, FormatAnsi.HIGHLIGHT);
                    }
                    else
                    {
                        _key = Style.ColorFormat(_key, ColorAnsi.DARK_GREY, FormatAnsi.HIGHLIGHT);
                    }
                }

                AddLineLocal(ref topList, _keyCenter + _key);

                // Borders
                topList = cardTop.Surround(topList, Width);
                foreach (string borderedLine in topList)
                {
                    spellCardList.Add(_padding + borderedLine + _padding);
                }

                #endregion

                #region BOTTOM LIST

                // Name
                Width = SPELL_WIDTH;
                AddBlankLineLocal(ref bottomList);

                string _name = spell.Name;
                string _lvl = spell.LvL.ToString("LvL 0");
                AddLineLocal(ref bottomList, ' ' + Style.ColorFormat(_name, ColorAnsi.MAGENTA, FormatAnsi.UNDERLINE) + Style.GetRemainingSpace(_name.Length + _lvl.Length + 2, Width) + Style.Color(_lvl, ColorAnsi.DARK_RED));

                AddBlankLineLocal(ref bottomList);

                // Mana cost
                string _mana = " MANA COST: ";
                AddLineLocal(ref bottomList, Style.GetRemainingSpace(_mana, 15) + _mana + Style.Color(spell.ManaCost.ToString("0 mana"), ColorAnsi.PURPLE));

                // Offence
                if (spell.Damage > 0)
                {
                    string _dmg = " DAMAGE: ";
                    AddLineLocal(ref bottomList, Style.GetRemainingSpace(_dmg, 15) + _dmg + Style.Color(spell.Damage.ToString("0.# dmg"), ColorAnsi.LIGHT_RED));
                }

                // Defence
                if (spell.Heal != 0)
                {
                    if (spell.Heal > 0)
                    {
                        string _def = " HEAL: ";
                        AddLineLocal(ref bottomList, Style.GetRemainingSpace(_def, 15) + _def + Style.Color(spell.Heal.ToString("0.# hp"), ColorAnsi.AQUA));
                    }
                    else
                    {
                        string _def = " SELF-DAMAGE: ";
                        AddLineLocal(ref bottomList, Style.GetRemainingSpace(_def, 15) + _def + Style.Color(spell.Heal.ToString("0.# hp"), ColorAnsi.RED));
                    }
                }

                // Effects
                if (spell.EffectList.Count > 0)
                {
                    string _fx = " EFFECTS: ";
                    AddLineLocal(ref bottomList, Style.GetRemainingSpace(_fx, 15) + _fx + Style.Color(String.Join(", ", spell.EffectList), ColorAnsi.PINK));
                }

                // Fills up the remaining lines
                while (bottomList.Count < 8)
                {
                    AddBlankLineLocal(ref bottomList);
                }

                bottomList = cardBottom.Surround(bottomList, Width);
                foreach (string borderedLines in bottomList)
                {
                    spellCardList.Add(borderedLines);
                }

                #endregion
            }
            else // No spell equipped
            {
                // Key
                Width = SPELL_WIDTH - 10;

                string _key = " [" + key.ToString() + "] ";
                string _keyCenter = Style.GetRemainingSpace(_key.Length / 2, Width / 2);

                AddLineLocal(ref topList, _keyCenter + Style.Color(_key, ColorAnsi.RUST));

                topList = cardTop.Surround(topList, Width);
                foreach (string borderedLine in topList)
                {
                    spellCardList.Add(_padding + borderedLine + _padding);
                }

                // The rest
                Width = SPELL_WIDTH;
                AddBlankLineLocal(ref bottomList);

                string _none = "(Empty)";
                string _noneCenter = Style.GetRemainingSpace(_none.Length / 2, Width / 2);
                AddLineLocal(ref bottomList, _noneCenter + Style.Color(_none, ColorAnsi.RUST));

                // Fills up the remaining lines
                while (bottomList.Count < 8)
                {
                    AddBlankLineLocal(ref bottomList);
                }

                bottomList = cardBottom.Surround(bottomList, Width);
                foreach (string borderedLines in bottomList)
                {
                    spellCardList.Add(borderedLines);
                }
            }

            Width = 148;
            return spellCardList;
        }

        private List<string> GeneratePlayerColumn()
        {
            List<string> playerColumn = new List<string>();
            Width = COLUMN_WIDTH;

            // Name
            AddLineLocal(ref playerColumn, GenerateName(Player.Name, true));

            AddBlankLineLocal(ref playerColumn);

            // Health
            foreach (string line in GenerateSlider("Health", Player.Health, Player.MaxHealth, true, Style.HealthColor))
            {
                AddLineLocal(ref playerColumn, line);
            }

            AddBlankLineLocal(ref playerColumn);

            // Defence
            AddLineLocal(ref playerColumn, GenerateInfo("DEFENCE: ", Player.Defence.ToString("0.# def"), true, Style.DefenceColor, GenerateEffectNotice(Effect.Burn)));

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
            AddLineLocal(ref playerColumn, GenerateInfo("MANA REGENERATION: ", Player.ManaRate.ToString("+0.# mana/turn"), true, Style.ManaRegColor, GenerateEffectNotice(Effect.Freeze)));

            // Fills up the remaining lines
            while (playerColumn.Count < COLUMN_HEIGHT)
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
            Width = COLUMN_WIDTH;

            // Name
            AddLineLocal(ref enemyColumn, GenerateName(enemy.Name, false));

            AddBlankLineLocal(ref enemyColumn);

            // Health
            foreach (string line in GenerateSlider("Health", enemy.Health, enemy.MaxHealth, false, Style.HealthColor))
            {
                AddLineLocal(ref enemyColumn, line);
            }

            AddBlankLineLocal(ref enemyColumn);

            // Defence
            AddLineLocal(ref enemyColumn, GenerateInfo("DEFENCE: ", enemy.Defence.ToString("0.# def"), false, Style.DefenceColor, GenerateEffectNotice(Effect.Burn, enemy)));

            AddBlankLineLocal(ref enemyColumn);

            // Effect
            foreach (string line in GenerateEffectIndicator(enemy.EffectDict, false))
            {
                AddLineLocal(ref enemyColumn, line);
            }

            AddBlankLineLocal(ref enemyColumn);

            // Damage
            AddLineLocal(ref enemyColumn, GenerateInfo("DAMAGE: ", enemy.Damage.ToString("0.# dmg"), false, ColorAnsi.LIGHT_RED, GenerateEffectNotice(Effect.Buff, enemy) + GenerateEffectNotice(Effect.Debuff, enemy)));

            AddBlankLineLocal(ref enemyColumn);

            // Random effect(s)
            AddLineLocal(ref enemyColumn, GenerateInfo("POSSIBLE EFFECT(S): ", String.Join(", ", enemy.RandomEffects), false, ColorAnsi.PINK, GenerateEffectNotice(Effect.Poison, enemy)));

            // Fills up the remaining lines
            while (enemyColumn.Count < COLUMN_HEIGHT)
            {
                AddBlankLineLocal(ref enemyColumn);
            }

            Width = 148;
            enemyLines = enemyColumn;
            return enemyColumn;
        }

        private List<string> GenerateWeapon()
        {
            List<string> weaponList = new List<string>();
            Width = 148 - INFO_PADDING.Length * 2;

            string _weapon = string.Empty;
            string _weaponTitle = "Weapon attack";
            string _weaponKey = " [T] ";

            if (combat.CanUseWeapon)
            {
                _weapon += Style.ColorFormat(_weaponTitle, ColorAnsi.LIGHT_BLUE, FormatAnsi.UNDERLINE);
                _weapon += ' ';
                _weapon += Style.ColorFormat(_weaponKey, ColorAnsi.RED, FormatAnsi.HIGHLIGHT);
            }
            else
            {
                _weapon += Style.ColorFormat(_weaponTitle, ColorAnsi.DARK_GREY, FormatAnsi.UNDERLINE);
                _weapon += ' ';
                _weapon += Style.ColorFormat(_weaponKey, ColorAnsi.DARK_GREY, FormatAnsi.HIGHLIGHT);
            }

            string _info = string.Empty;
            string _infoTitle = "DAMAGE:";
            string _infoValue = Player.Damage.ToString("0.# dmg");

            _info += Style.Color(_infoTitle, ColorAnsi.WHITE);
            _info += ' ';
            _info += Style.Color(_infoValue, ColorAnsi.LIGHT_RED);

            AddLineLocal(ref weaponList, _weapon + "   " + _info + ' ' + GenerateEffectNotice(Effect.Buff) + GenerateEffectNotice(Effect.Debuff));

            Width = 148;
            return weaponList;
        }

        private void GenerateLoadout()
        {
            loadout[0] = GenerateSpellCard(Inventory.sorcery[0], 'Q', combat.CanUseSorcery, Player.PoisonedSpells[0]);
            loadout[1] = GenerateSpellCard(Inventory.sorcery[1], 'W', combat.CanUseSorcery, Player.PoisonedSpells[1]);
            loadout[2] = GenerateSpellCard(Inventory.sorcery[2], 'E', combat.CanUseSorcery, Player.PoisonedSpells[2]);
            loadout[3] = GenerateSpellCard(Inventory.sorcery[3], 'R', combat.CanUseSorcery, Player.PoisonedSpells[3]);
        }

        private string GenerateMenu()
        {
            string footer = string.Empty;
            Width = 148 - INFO_PADDING.Length * 2;

            string _flee = FKeyName;
            string _fleeKey = " [F] ";

            string _space = SpaceKeyName;
            string _spaceKey = " [SPACE] ";

            if (combat.CanSkip)
            {
                footer += Style.ColorFormat(_flee, ColorAnsi.YELLOW, FormatAnsi.UNDERLINE);
                footer += ' ';
                footer += Style.ColorFormat(_fleeKey, ColorAnsi.RED, FormatAnsi.HIGHLIGHT);
            }
            else
            {
                footer += Style.ColorFormat(_flee, ColorAnsi.DARK_GREY, FormatAnsi.UNDERLINE);
                footer += ' ';
                footer += Style.ColorFormat(_fleeKey, ColorAnsi.DARK_GREY, FormatAnsi.HIGHLIGHT);
            }

            footer += Style.GetRemainingSpace(_flee.Length + _fleeKey.Length + _space.Length + _spaceKey.Length + 2, Width);

            if (combat.CanContinue)
            {
                footer += Style.ColorFormat(_space, ColorAnsi.YELLOW, FormatAnsi.UNDERLINE);
                footer += ' ';
                footer += Style.ColorFormat(_spaceKey, ColorAnsi.RED, FormatAnsi.HIGHLIGHT);
            }
            else
            {
                footer += Style.ColorFormat(_space, ColorAnsi.DARK_GREY, FormatAnsi.UNDERLINE);
                footer += ' ';
                footer += Style.ColorFormat(_spaceKey, ColorAnsi.DARK_GREY, FormatAnsi.HIGHLIGHT);
            }

            Width = 148;
            return footer;
        }

        private string GenerateName(string name, bool alignedLeft)
        {
            return (alignedLeft ? "" : Style.GetRemainingSpace(name.Length + 2, COLUMN_WIDTH)) + Style.Color('<', ColorAnsi.WHITE) + Style.ColorFormat(name, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + Style.Color('>', ColorAnsi.WHITE);
        }

        private List<string> GenerateSlider(string name, double value, double maxValue, bool alignedLeft, ColorAnsi color)
        {
            List<string> sliderList = new List<string>();

            // Slider name
            AddLineLocal(ref sliderList, Style.Color($"{name.ToUpper()} [{value.ToString("0.#")}/{maxValue.ToString("0.#")}]", ColorAnsi.WHITE), alignedLeft);

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

        private string GenerateInfo(string name, string value, bool alignedLeft, ColorAnsi color, string additionalInfo = null)
        {
            string info = string.Empty;

            if (!alignedLeft)
            {
                if (additionalInfo == null)
                    info += Style.GetRemainingSpace(name + value, Width);
                else
                    info += Style.GetRemainingSpace(name.Length + value.Length + Style.PurgeAnsi(additionalInfo).Length + 1, Width);
            }

            if (additionalInfo == null)
            {
                info += Style.Color(name, ColorAnsi.WHITE) + Style.Color(value, color);
            }
            else
            {
                if (alignedLeft)
                    info += Style.Color(name, ColorAnsi.WHITE) + Style.Color(value, color) + ' ' + additionalInfo;
                else
                    info += additionalInfo + ' ' + Style.Color(name, ColorAnsi.WHITE) + Style.Color(value, color);
            }

            return info;
        }

        private List<string> GenerateEffectIndicator(Dictionary<Effect, int> effectDict, bool alignedLeft)
        {
            List<string> effectIndicatorList = new List<string>();

            Dictionary<Effect, int> _activeEffectDict = new Dictionary<Effect, int>(effectDict.Count(x => x.Value > 0));
            foreach (var pair in effectDict)
            {
                if (pair.Value > 0)
                {
                    _activeEffectDict.Add(pair.Key, pair.Value);
                }
            }

            string _fxTitle = "ACTIVE EFFECT(S): ";

            string _currentLine = string.Empty;
            _currentLine += Style.Color(_fxTitle, ColorAnsi.WHITE);

            if (_activeEffectDict.Count > 0)
            {
                for (int i = 0; i < _activeEffectDict.Count; i++)
                {
                    if (i != 0)
                    {
                        _currentLine += Style.GetBlankLine(_fxTitle.Length);
                    }

                    KeyValuePair<Effect, int> _current = _activeEffectDict.ElementAt(i);

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
                            _currentLine = _currentLine.Insert(0, Style.GetBlankLine(COLUMN_WIDTH - 41));
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
                    _currentLine = _currentLine.Insert(0, Style.GetBlankLine(COLUMN_WIDTH - 41));
                }
                AddLineLocal(ref effectIndicatorList, _currentLine);
            }

            return effectIndicatorList;
        }

        private string GenerateEffectNotice(Effect effect, Enemy enemy = null)
        {
            string notice = string.Empty;

            if (enemy == null)
            {
                switch (effect)
                {
                    case Effect.Burn:
                        if (Player.Defence < Player.BaseDefence)
                            notice = $"({Style.Color(effect.ToString(), ColorAnsi.CORAL)})";
                        break;

                    case Effect.Freeze:
                        if (Player.ManaRate < Player.BaseManaRate)
                            notice = $"({Style.Color(effect.ToString(), ColorAnsi.CORAL)})";
                        break;

                    case Effect.Buff:
                        if (Player.Damage > Player.BaseDamage)
                            notice = $"({Style.Color(effect.ToString(), ColorAnsi.CORAL)})";
                        break;

                    case Effect.Debuff:
                        if (Player.Damage < Player.BaseDamage)
                            notice = $"({Style.Color(effect.ToString(), ColorAnsi.CORAL)})";
                        break;
                }
            }
            else
            {
                switch (effect)
                {
                    case Effect.Burn:
                        if (enemy.Defence < enemy.BaseDefence)
                            notice = $"({Style.Color(effect.ToString(), ColorAnsi.CORAL)})";
                        break;

                    case Effect.Poison:
                        if (!enemy.CanCastEffect)
                            notice = $"({Style.Color(effect.ToString(), ColorAnsi.CORAL)})";
                        break;

                    case Effect.Buff:
                        if (enemy.Damage > enemy.BaseDamage)
                            notice = $"({Style.Color(effect.ToString(), ColorAnsi.CORAL)})";
                        break;

                    case Effect.Debuff:
                        if (enemy.Damage < enemy.BaseDamage)
                            notice = $"({Style.Color(effect.ToString(), ColorAnsi.CORAL)})";
                        break;
                }
            }

            return notice;
        }

        public void SetCombat(Combat combat)
        {
            this.combat = combat;
        }

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
        }

    }
}
