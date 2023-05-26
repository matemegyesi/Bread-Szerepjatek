using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class Page
    {
        public List<Item> ContainedItems { get; set; } = new List<Item>();
        public List<Spell> ContainedSpells { get; set; } = new List<Spell>();
        private int remainingSpace;

        public Page(int availableSpace)
        {
            remainingSpace = availableSpace;
        }

        /// <summary>
        /// (Re)creates the pages of items for the inventory.
        /// </summary>
        /// <param name="pageList">The reference to the page list.</param>
        /// <param name="itemList">The changed list of items.</param>
        /// <param name="availableSpace">The amount of lines that the page can take up.</param>
        static public void CreatePages(ref List<Page> pageList, int availableSpace, List<Item> itemList)
        {
            pageList.Clear();

            Page currentPage = new Page(availableSpace);

            for (int i = 0; i < itemList.Count; i++)
            {
                currentPage.remainingSpace -= InventoryWindow.CalculateItemCardHeight(itemList[i]);

                if (currentPage.remainingSpace >= -1)
                {
                    currentPage.ContainedItems.Add(itemList[i]);
                    if (i == itemList.Count - 1)
                    {
                        pageList.Add(currentPage);
                    }
                }
                else
                {
                    pageList.Add(currentPage);
                    currentPage = new Page(availableSpace);
                    i--;
                }
            }
        }

        /// <summary>
        /// (Re)creates the pages of items for the inventory.
        /// </summary>
        /// <param name="pageList">The reference to the page list.</param>
        /// <param name="spellList">The changed list of items.</param>
        /// <param name="availableSpace">The amount of lines that the page can take up.</param>
        static public void CreatePages(ref List<Page> pageList, int availableSpace, List<Spell> spellList)
        {
            pageList.Clear();

            Page currentPage = new Page(availableSpace);

            for (int i = 0; i < spellList.Count; i++)
            {
                currentPage.remainingSpace -= SorceryWindow.CalculateSpellCardHeight(spellList[i]);

                if (currentPage.remainingSpace >= -1)
                {
                    currentPage.ContainedSpells.Add(spellList[i]);
                    if (i == spellList.Count - 1)
                    {
                        pageList.Add(currentPage);
                    }
                }
                else
                {
                    pageList.Add(currentPage);
                    currentPage = new Page(availableSpace);
                    i--;
                }
            }
        }
    }
}
