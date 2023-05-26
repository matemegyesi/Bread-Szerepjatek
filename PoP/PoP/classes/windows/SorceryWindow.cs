using System;
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

        // Change detection
        private bool headerChanged;
        private bool spellCardsChanged;
        private bool pageIndicatorChanged;

        public SorceryWindow(int limit)
        {
            Height = 46;
            Width = 41;

            sorceryLimit = limit;

            Page.AvailableSpace = Height - 9;

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


            // Fills the remaining lines between the items and the page number indicator
            int _beforeLineCount = LineList.Count;
            for (int i = 0; i < Height - _beforeLineCount - 2; i++)
            {
                AddBlankLine();
            }

            // Page number indicator
            //pageAmount = Page.List.Count;
            //foreach (string line in GeneratePageIndicator(currentPage, pageAmount))
            //{
            //    AddLine(line);
            //}

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

    }
}
