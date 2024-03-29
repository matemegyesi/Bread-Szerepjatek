﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class SorceryWindow : Window
    {
        static public List<Spell> spellList = new List<Spell>();
        private readonly int sorceryLimit;

        public string Title
        {
            get
            {
                return $" &ß> Sorcery ({spellList.Count}/{sorceryLimit}) <ß& ";
            }
        }
        public string HowToUse
        {
            get
            {
                return "USE: press [F12], then the key of the spell or [SPACE] to switch to ITEMS";
            }
        }

        public bool InUse { get; private set; }

        private int currentPage;
        private int pageAmount;

        // Page management
        private int pageAvailableSpace;
        private List<Page> pageList = new List<Page>();

        // Change detection
        private bool headerChanged;
        private bool spellCardsChanged;
        private bool pageIndicatorChanged;

        public SorceryWindow(int limit)
        {
            Height = 46;
            Width = 41;

            sorceryLimit = limit;

            pageAvailableSpace = Height - 9;
            currentPage = 1;
        }

        protected override List<string> GenerateLines()
        {
            LineList.Clear();

            // Header & how-to-use
            foreach (string line in GenerateHeader())
            {
                AddLine(line);
            }

            // Spell cards
            foreach (string line in GenerateSpellCardInterface())
            {
                AddLine(line);
            }

            // Fills the remaining lines between the spells and the page number indicator
            int _beforeLineCount = LineList.Count;
            for (int i = 0; i < Height - _beforeLineCount - 2; i++)
            {
                AddBlankLine();
            }

            // Page number indicator
            pageAmount = pageList.Count;
            foreach (string line in GeneratePageIndicator(currentPage, pageAmount))
            {
                AddLine(line);
            }

            return LineList;
        }

        private List<string> GenerateHeader()
        {
            List<string> headerLineList = new List<string>();

            AddBlankLineLocal(ref headerLineList);
            AlignedText _header = Style.AlignCenterSpaces(Title, Width);
            AddLineLocal(ref headerLineList, _header.Before + Style.ColorFormat(Title, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + _header.After);

            AddBlankLineLocal(ref headerLineList);
            AddLineLocal(ref headerLineList, HowToUse, true, true);
            AddLineLocal(ref headerLineList, Style.Color(Style.GetDashedLine(Width), ColorAnsi.GREY));

            AddBlankLineLocal(ref headerLineList);

            return headerLineList;
        }

        public static int CalculateSpellCardHeight(Spell spell)
        {
            int _lineCount = 0;
            _lineCount += 3;

            if (spell.Damage > 0)
            {
                _lineCount += 1;
            }
            if (spell.Heal != 0)
            {
                _lineCount += 1;
            }
            if (spell.EffectList.Count > 0)
            {
                _lineCount += 1;
            }

            return _lineCount + 1;
        }

        private List<string> GenerateSpellCard(Spell spell, int valueCount, bool hasHR = true)
        {
            List<string> spellCardLineList = new List<string>();

            // Name, select value and type
            string _name = ' ' + Style.ColorFormat(spell.Name, ColorAnsi.MAGENTA, FormatAnsi.UNDERLINE) + ' ';
            string _value = ' ' + Display.keys.ElementAt(valueCount).Value + ' ';
            if (!InUse)
            {
                _value = Style.ColorFormat(_value, ColorAnsi.DARK_GREY, FormatAnsi.HIGHLIGHT);
            }
            else
            {
                _value = Style.ColorFormat(_value, ColorAnsi.MAGENTA, FormatAnsi.HIGHLIGHT);
            }
            string _lvl = Style.Color("LvL " + spell.LvL, ColorAnsi.DARK_RED);
            AddLineLocal(ref spellCardLineList, _name + _value + Style.GetRemainingSpace(spell.Name.Length + Style.PurgeAnsi(_value).Length + Style.PurgeAnsi(_lvl).Length + 3, Width) + _lvl);

            AddBlankLineLocal(ref spellCardLineList);

            // Mana cost
            string _mana = " MANA COST: ";
            AddLineLocal(ref spellCardLineList, Style.GetRemainingSpace(_mana, 15) + _mana + Style.Color(spell.ManaCost.ToString("0 mana"), ColorAnsi.PURPLE));

            // Offence
            if (spell.Damage > 0)
            {
                string _dmg = " DAMAGE: ";
                AddLineLocal(ref spellCardLineList, Style.GetRemainingSpace(_dmg, 15) + _dmg + Style.Color(spell.Damage.ToString("0.# dmg"), ColorAnsi.LIGHT_RED));
            }

            // Defence
            if (spell.Heal != 0)
            {
                if (spell.Heal > 0)
                {
                    string _def = " HEAL: ";
                    AddLineLocal(ref spellCardLineList, Style.GetRemainingSpace(_def, 15) + _def + Style.Color(spell.Heal.ToString("0.# hp"), ColorAnsi.AQUA));
                }
                else
                {
                    string _def = " SELF-DAMAGE: ";
                    AddLineLocal(ref spellCardLineList, Style.GetRemainingSpace(_def, 15) + _def + Style.Color(spell.Heal.ToString("0.# hp"), ColorAnsi.RED));
                }
            }

            // Effects
            if (spell.EffectList.Count > 0)
            {
                string _fx = " EFFECTS: ";
                AddLineLocal(ref spellCardLineList, Style.GetRemainingSpace(_fx, 15) + _fx + Style.Color(String.Join(", ", spell.EffectList), ColorAnsi.PINK));
            }

            if (hasHR)
            {
                AddLineLocal(ref spellCardLineList, Style.AddPadding(Style.Color(Style.GetBlankLine(Width - 2, Border.SINGLE_HORIZONTAL), ColorAnsi.DARK_GREY)));
            }

            return spellCardLineList;
        }

        private List<string> GenerateSpellCardInterface()
        {
            List<string> spellCardList = new List<string>();

            if (pageList.Count > 0)
            {
                int _remainingLineCount = Height - 9;
                int _valueCounter = 0;

                if (currentPage != 1)
                {
                    for (int i = 0; i < currentPage - 1; i++)
                    {
                        _valueCounter += pageList[i].ContainedSpells.Count;
                    }
                }

                foreach (Spell spell in pageList[currentPage - 1].ContainedSpells)
                {
                    _remainingLineCount -= CalculateSpellCardHeight(spell);
                    if (_remainingLineCount >= 2)
                    {
                        spellCardList.AddRange(GenerateSpellCard(spell, _valueCounter));
                        ++_valueCounter;
                    }
                    else if (_remainingLineCount >= -1)
                    {
                        spellCardList.AddRange(GenerateSpellCard(spell, _valueCounter, false));
                        ++_valueCounter;
                    }
                }
            }

            return spellCardList;
        }

        private List<string> GeneratePageIndicator(int currentPage, int pageCount)
        {
            List<string> pageIndicatorLineList = new List<string>();

            pageIndicatorLineList.Add(Style.Color(Style.GetBlankLine(8, Border.DOUBLE_HORIZONTAL) + Border.DOUBLE_TO_SINGLE_T_TOP + Style.GetBlankLine(23, Border.DOUBLE_HORIZONTAL) + Border.DOUBLE_TO_SINGLE_T_TOP + Style.GetBlankLine(8, Border.DOUBLE_HORIZONTAL), ColorAnsi.DARK_GREY));

            pageIndicatorLineList.Add(Style.GetBlankLine(8) + Style.Color(Border.SINGLE_VERTICAL, ColorAnsi.DARK_GREY) + Style.GetRemainingSpace(currentPage.ToString().Length + 7, 11) + Style.Color("◄[Q]◄ ", (currentPage != 1) ? ColorAnsi.CYAN : ColorAnsi.TEAL) + Style.Color(currentPage + " / " + pageCount, ColorAnsi.WHITE) + Style.Color(" ►[E]►", (currentPage != pageCount) ? ColorAnsi.CYAN : ColorAnsi.TEAL) + Style.GetRemainingSpace(pageCount.ToString().Length + 7, 11) + Style.Color(Border.SINGLE_VERTICAL, ColorAnsi.DARK_GREY) + Style.GetBlankLine(8));

            return pageIndicatorLineList;
        }

        /// <summary>
        /// It turns the inventory to the next page. If it's already the last page, it'll loop around.
        /// </summary>
        public void NextPage()
        {
            //Page.CreatePages(spellList);
            pageAmount = pageList.Count;

            if (pageAmount != 1)
            {
                if (currentPage == pageAmount)
                {
                    currentPage = 1;
                }
                else
                {
                    currentPage += 1;
                }

                pageIndicatorChanged = true;
                spellCardsChanged = true;
                HasChanged = true;
            }
        }

        /// <summary>
        /// It turns the inventory to the previous page. If it's the first page, it'll loop around.
        /// </summary>
        public void PreviousPage()
        {
            //Page.CreatePages(spellList);
            pageAmount = pageList.Count;

            if (pageAmount != 1)
            {
                if (currentPage == 1)
                {
                    currentPage = pageAmount;
                }
                else
                {
                    currentPage -= 1;
                }

                pageIndicatorChanged = true;
                spellCardsChanged = true;
                HasChanged = true;
            }
        }

        /// <summary>
        /// Toggles the InUse boolean, toggling the color of the equipment keys next to the spell names.
        /// </summary>
        public void ToggleInUse()
        {
            if (!InUse)
            {
                InUse = true;
            }
            else
            {
                InUse = false;
            }

            spellCardsChanged = true;
            HasChanged = true;
        }

        /// <summary>
        /// Sets the InUse boolean, setting the color of the equipment keys next to the spell names.
        /// </summary>
        /// <param name="isInUse">True activates, false deactivates.</param>
        public void SetInUse(bool isInUse)
        {
            if (InUse != isInUse)
            {
                InUse = isInUse;

                spellCardsChanged = true;
                HasChanged = true;
            }
        }

        /// <summary>
        /// Overrides the local spell list of the inventory.
        /// </summary>
        /// <param name="newSpellList">The changed list.</param>
        public void UpdateSpellList(List<Spell> newSpellList)
        {
            spellList = newSpellList;

            Page.CreatePages(ref pageList, pageAvailableSpace, spellList);
            if (currentPage > pageList.Count)
            {
                currentPage = 1;
            }

            headerChanged = true;
            spellCardsChanged = true;
            pageIndicatorChanged = true;
            HasChanged = true;
        }
    }
}
