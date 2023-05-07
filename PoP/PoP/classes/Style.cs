using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ansi;

namespace PoP.classes
{
    enum ColorAnsi
    {
        DARK_RED = 88,
        RED = 160,
        LIGHT_RED = 210,

        PURPLE = 93,
        MAGENTA = 169,
        PINK = 219,

        DARK_BLUE = 25,
        BLUE = 27,
        LIGHT_BLUE = 39,
        CYAN = 87,
        AQUA = 43,
        TEAL = 73,

        DARK_GREEN = 64,
        GREEN = 41,
        LIGHT_GREEN = 119,

        WHEAT = 186,
        YELLOW = 227,
        CORAL = 214,
        ORANGE = 208,
        RUST = 131,

        GREY = 8,
        GRAY = 8,
        DARK_GREY = 239,
        DARK_GRAY = 239,
        WHITE = 15,
        BLACK = 0
    }

    enum FormatAnsi
    {
        UNDERLINE = 4,
        HIGHLIGHT = 7
    }

    struct AlignedText
    {
        public string Before;
        public string After;
    }

    internal class Style
    {

        private const string END = "\u001b[0m";


        #region Public methods

        public static void EnableStyling()
        {
            WindowsConsole.TryEnableVirtualTerminalProcessing();
        }

        // Coloring

        /// <summary>
        /// Surrounds the text with ANSI escape code, changing its color.
        /// </summary>
        /// <param name="text">The text that needs to be stylized.</param>
        /// <param name="color">Enum of the color.</param>
        public static string Color(string text, ColorAnsi color)
        {
            try
            {
                string formattedText = AnsiColor((byte)color) + text + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        // Formatting

        /// <summary>
        /// Surrounds the text with ANSI escape code, changing its format.
        /// </summary>
        /// <param name="text">The text that needs to be stylized.</param>
        /// <param name="format">Enum of the format.</param>
        public static string Format(string text, FormatAnsi format)
        {
            try
            {
                string formattedText = AnsiFormat((byte)format) + text + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        // Coloring & Formatting

        /// <summary>
        /// Surrounds the text with ANSI escape code, changing its color and format.
        /// </summary>
        /// <param name="text">The text that needs to be stylized.</param>
        /// <param name="color">Enum of the color.</param>
        /// <param name="format">Enum of the format.</param>
        public static string ColorFormat(string text, ColorAnsi color, FormatAnsi format)
        {
            try
            {
                string formattedText = AnsiColor((byte)color) + AnsiFormat((byte)format) + text + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }
        

        #endregion


        private static string AnsiColor(byte colorNumber)
        {
            return $"\u001b[38;5;{colorNumber}m";
        }

        private static string AnsiFormat(byte formatNumber)
        {
            return $"\u001b[{formatNumber}m";
        }

    }
}
