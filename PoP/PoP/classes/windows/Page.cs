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
    }
}
