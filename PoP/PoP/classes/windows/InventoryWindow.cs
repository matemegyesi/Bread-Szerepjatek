using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class InventoryWindow : Window
    {
        static public List<Item> itemList = new List<Item>(); //

        public string Title
        {
            get
            {
                return $" §$> Inventory ({itemList.Count}/{Inventory.inventoryLimit}) <$§ ";
            }
        }
        public string HowToUse
        {
            get
            {
                return "USE: press F12, then the value next to the item.";
            }
        }

        public bool InUse { get; private set; }

        private int currentPage;
        private int pageAmount;

        // Change detection
        private bool headerChanged;
        private bool itemCardsChanged;
        private bool pageIndicatorChanged;

        public InventoryWindow()
        {
            Height = 46;
            Width = 41;

            Page.AvailableSpace = Height - 9;

            currentPage = 1;
        }

        protected override List<string> GenerateLines()
        {
            if (CachedLineList.Count != Height)
            {
                LineList.Clear();

                // Header & how-to-use
                foreach (string line in GenerateHeader())
                {
                    AddLine(line);
                }

                // Item cards
                foreach (string line in GenerateItemCardInterface())
                {
                    AddLine(line);
                }

                // Fills the remaining lines between the items and the page number indicator
                int _beforeLineCount = LineList.Count;
                for (int i = 0; i < Height - _beforeLineCount - 2; i++)
                {
                    AddBlankLine();
                }

                // Page number indicator
                pageAmount = Page.List.Count;
                foreach (string line in GeneratePageIndicator(currentPage, pageAmount))
                {
                    AddLine(line);
                }

                return LineList;
            }
            else
            {
                if (headerChanged)
                {
                    LineList.RemoveRange(0, 7);

                    InsertLineRange(0, GenerateHeader());

                    headerChanged = false;
                }

                if (itemCardsChanged)
                {
                    LineList.RemoveRange(7, Page.AvailableSpace);
                    InsertLineRange(7, GenerateItemCardInterface());

                    //Fills the remaining lines between the items and the page number indicator
                    int _beforeLineCount = LineList.Count;
                    for (int i = 0; i < Height - _beforeLineCount; i++)
                    {
                        InsertBlankLine(LineList.Count - 2);
                    }

                    itemCardsChanged = false;
                }

                if (pageIndicatorChanged)
                {
                    LineList.RemoveRange(LineList.Count - 2, 2);

                    pageAmount = Page.List.Count;
                    foreach (string line in GeneratePageIndicator(currentPage, pageAmount))
                    {
                        AddLine(line);
                    }

                    pageIndicatorChanged = false;
                }


                return LineList;
            }
        }

        private List<string> GenerateHeader()
        {
            List<string> headerLineList = new List<string>();

            AddBlankLineLocal(ref headerLineList);
            AlignedText _header = Style.AlignCenterSpaces(Title, Width);
            AddLineLocal(ref headerLineList, _header.Before + Style.ColorFormat(Title, ColorAnsi.WHITE, FormatAnsi.HIGHLIGHT) + _header.After);

            AddBlankLineLocal(ref headerLineList);
            AddLineLocal(ref headerLineList, HowToUse, true);
            AddLineLocal(ref headerLineList, Style.Color(Style.GetDashedLine(Width), ColorAnsi.GREY));

            AddBlankLineLocal(ref headerLineList);

            return headerLineList;
        }

        public static int CalculateItemCardHeight(Item item)
        {
            int _lineCount = 0;
            _lineCount += 2;

            if (item is Weapon)
            {
                _lineCount += 2;
            }
            else if (item is Armor)
            {
                _lineCount += 2;
            }

            return _lineCount + 1;
        }

        private List<string> GenerateItemCard(Item item, int valueCount, bool hasHR = true)
        {
            List<string> itemCardLineList = new List<string>();

            // Name, select value and type
            string _name = ' ' + Style.ColorFormat(item.Name, ColorAnsi.LIGHT_BLUE, FormatAnsi.UNDERLINE) + ' ';
            string _value = ' ' + Display.keys.ElementAt(valueCount).Value + ' ';
            if (!InUse)
            {
                _value = Style.ColorFormat(_value, ColorAnsi.DARK_GREY, FormatAnsi.HIGHLIGHT);
            }
            else
            {
                _value = Style.ColorFormat(_value, ColorAnsi.LIGHT_BLUE, FormatAnsi.HIGHLIGHT);
            }
            string _type = string.Empty;
            if (item is Weapon)
            {
                _type = Style.Color("Weapon", ColorAnsi.DARK_BLUE);
            }
            else if (item is Armor)
            {
                _type = Style.Color("Armor", ColorAnsi.DARK_BLUE);
            }
            AddLineLocal(ref itemCardLineList, _name + _value + Style.GetRemainingSpace(item.Name.Length + Style.PurgeAnsi(_value).Length + Style.PurgeAnsi(_type).Length + 6, Width) + _type);

            AddBlankLineLocal(ref itemCardLineList);

            // Offence
            if (item is Weapon)
            {
                string _dmg = " DAMAGE: ";
                AddLineLocal(ref itemCardLineList, Style.GetRemainingSpace(_dmg, 15) + _dmg + Style.Color((item as Weapon).Damage.ToString("0.##") + " dmg", ColorAnsi.LIGHT_RED));
            }

            // Defence
            if (item is Armor)
            {
                string _def = " DEFENCE: ";
                AddLineLocal(ref itemCardLineList, Style.GetRemainingSpace(_def, 15) + _def + Style.Color((item as Armor).Defense.ToString("0.##") + " def", ColorAnsi.AQUA));
            }

            // Equip
            string _equip = " EQUIPABLE TO: ";
            AddLineLocal(ref itemCardLineList, Style.GetRemainingSpace(_equip, 15) + _equip + Style.ColorFormat(item.Slot.ToString(), ColorAnsi.PINK, FormatAnsi.UNDERLINE));

            if (hasHR)
            {
                AddLineLocal(ref itemCardLineList, Style.AddPadding(Style.Color(Style.GetBlankLine(Width - 5, Border.SINGLE_HORIZONTAL), ColorAnsi.DARK_GREY)));
            }

            return itemCardLineList;
        }

        private List<string> GenerateItemCardInterface()
        {
            List<string> itemCardList = new List<string>();

            if (Page.List.Count > 0)
            {
                int _remainingLineCount = Height - 9;
                int _valueCounter = 0;

                if (currentPage != 1)
                {
                    for (int i = 0; i < currentPage - 1; i++)
                    {
                        _valueCounter += Page.List[i].ContainedItems.Count;
                    }
                }

                foreach (Item item in Page.List[currentPage - 1].ContainedItems)
                {
                    _remainingLineCount -= CalculateItemCardHeight(item);
                    if (_remainingLineCount >= 2)
                    {
                        itemCardList.AddRange(GenerateItemCard(item, _valueCounter));
                        ++_valueCounter;
                    }
                    else if (_remainingLineCount >= -1)
                    {
                        itemCardList.AddRange(GenerateItemCard(item, _valueCounter, false));
                        ++_valueCounter;
                    }
                }
            }

            return itemCardList;
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
            Page.CreatePages(itemList);
            pageAmount = Page.List.Count;

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
                itemCardsChanged = true;
                HasChanged = true;
            }
        }

        /// <summary>
        /// It turns the inventory to the previous page. If it's the first page, it'll loop around.
        /// </summary>
        public void PreviousPage()
        {
            Page.CreatePages(itemList);
            pageAmount = Page.List.Count;

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
                itemCardsChanged = true;
                HasChanged = true;
            }
        }

        /// <summary>
        /// Toggles the color of the equipment keys next to the item names.
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

            itemCardsChanged = true;
            HasChanged = true;
        }

        /// <summary>
        /// Overrites the local item list of the inventory.
        /// </summary>
        /// <param name="newItemList">The changed list.</param>
        public void UpdateItemList(List<Item> newItemList)
        {
            itemList = newItemList;

            Page.CreatePages(itemList);
            if (currentPage > Page.List.Count)
            {
                currentPage = 1;
            }

            headerChanged = true;
            itemCardsChanged = true;
            pageIndicatorChanged = true;
            HasChanged = true;
        }
    }
}
