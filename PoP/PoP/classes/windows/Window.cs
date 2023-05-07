using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class Window
    {
        public int Height { get; protected set; } = 63;
        public int Width { get; protected set; } = 237;

        public bool IsEnabled { get; set; }
        public bool HasChanged { get; set; }

        protected List<string> LineList = new List<string>();
        public List<string> CachedLineList { get; set; } = new List<string>();

        public List<string> GetLines { get; set; }
    }
}
