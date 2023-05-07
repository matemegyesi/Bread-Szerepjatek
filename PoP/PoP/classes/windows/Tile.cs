using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class Tile
    {
        public char Char { get; set; }
        public string StartStyle { get; private set; }
        public string EndStyle { get; private set; }

        public Tile(char character)
        {
            Char = character;
            StartStyle = string.Empty;
            EndStyle = string.Empty;
        }

        public void SetStartStyle(string style)
        {
            StartStyle = style;
        }

        public void SetEndStyle(string style)
        {
            EndStyle = style;
        }

        public void ResetStyle()
        {
            StartStyle = string.Empty;
            EndStyle = string.Empty;
        }
    }
}
