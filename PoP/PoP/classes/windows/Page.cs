using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class Page
    {
        public static List<Page> List = new List<Page>();
        public static int AvailableSpace;

        public List<Item> ContainedItems { get; set; } = new List<Item>();
        private int remainingSpace;

        public Page()
        {
            remainingSpace = AvailableSpace;
        }

        /// <summary>
        /// (Re)creates the pages of items for the inventory.
        /// </summary>
        /// <param name="itemList">The changed list of items.</param>
        public static void CreatePages(List<Item> itemList)
        {
            List.Clear();

            Page currentPage = new Page();

            for (int i = 0; i < itemList.Count; i++)
            {
                currentPage.remainingSpace -= InventoryWindow.CalculateItemCardHeight(itemList[i]);

                if (currentPage.remainingSpace >= -1)
                {
                    currentPage.ContainedItems.Add(itemList[i]);
                    if (i == itemList.Count - 1)
                    {
                        List.Add(currentPage);
                    }
                }
                else
                {
                    List.Add(currentPage);
                    currentPage = new Page();
                    i--;
                }
            }
        }
    }
}
