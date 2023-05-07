using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class StatsWindow : Window
    {
        public string Title
        {
            get
            {
                return $" #•> STATISTICS <•# ";
            }
        }

        public StatsWindow()
        {
            Height = 23;
            Width = 44;
        }
    }
}
