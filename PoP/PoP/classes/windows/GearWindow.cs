using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class GearWindow : Window
    {
        public string Title
        {
            get
            {
                return $" ¶○> GEAR <○¶ ";
            }
        }

        public GearWindow()
        {
            Height = 22;
            Width = 44;
        }
    }
}
