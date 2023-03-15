using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ansi;

namespace PoP.classes
{
    internal class Style
    {

        public static void EnableStyling()
        {
            WindowsConsole.TryEnableVirtualTerminalProcessing();
        }

    }
}
