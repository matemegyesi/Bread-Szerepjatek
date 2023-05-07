using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class InventoryWindow : Window
    {
        public string Title
        {
            get
            {
                return $" §$> Inventory ({0}/10) <$§ ";
            }
        }
        public string HowToUse
        {
            get
            {
                return "USE: press F12, then the value next to the item.";
            }
        }

        public InventoryWindow()
        {
            Height = 46;
            Width = 41;
        }
    }
}
