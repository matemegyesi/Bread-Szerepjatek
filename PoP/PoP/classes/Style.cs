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

        private static readonly Dictionary<string, byte> ansiCodes = new Dictionary<string, byte>()
        {
            { "RED", 9 },
            { "PINK", 13 },
            { "BLUE", 12 },
            { "CYAN", 14 },
            { "DARK_CYAN", 6 },
            { "GREEN", 42 },
            { "YELLOW", 11 },
            { "ORANGE", 214 },
            { "DARK_GREY", 8 },
            { "DARK_GRAY", 8 },
            { "WHITE", 15 },
            { "BLACK", 0 },

            { "BOLD", 1 }, // csak színezetlen szövegen van értelme
            { "UNDERLINE", 4 },
            { "HIGHLIGHT", 7 }
        };

        private const string END = "\u001b[0m";


        #region Publikus metódusok

        public static void EnableStyling()
        {
            WindowsConsole.TryEnableVirtualTerminalProcessing();
        }

        /// <summary>
        /// Körülveszi a szöveget ANSI escape kóddal, megváltoztatva a színét.
        /// </summary>
        /// <param name="text">Stilizálandó szöveg.</param>
        /// <param name="color">Angolul, nagybetűkkel, space helyett alsóvonallal.</param>
        public static string Color(string text, string color)
        {
            try
            {
                string formattedText = AnsiColor(ansiCodes[color]) + text + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Körülveszi a szöveget ANSI escape kóddal, megváltoztatva a dekorációját.
        /// </summary>
        /// <param name="text">Stilizálandó szöveg.</param>
        /// <param name="format">BOLD / UNDERLINE / HIGHLIGHT</param>
        public static string Format(string text, string format)
        {
            try
            {
                string formattedText = AnsiFormat(ansiCodes[format]) + text + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Körülveszi a szöveget ANSI escape kóddal, megváltoztatva a színét és dekorációját.
        /// </summary>
        /// <param name="text">Stilizálandó szöveg.</param>
        /// <param name="color">Angolul, nagybetűkkel, space helyett alsóvonallal.</param>
        /// <param name="format">BOLD / UNDERLINE / HIGHLIGHT</param>
        public static string ColorFormat(string text, string color, string format)
        {
            try
            {
                string formattedText = AnsiColor(ansiCodes[color]) + AnsiFormat(ansiCodes[format]) + text + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// String tömböt ad vissza, amiben a szöveg ki van színezve és formázva.
        /// </summary>
        public static string[] TestStyle()
        {
            string coloredText = $"╔═════════════════╤═════════════════╗\n║ {ColorFormat("GYŰRŰSPÁNCÉL", "CYAN", "UNDERLINE")}    │ {ColorFormat("ARANYKORONA", "CYAN", "UNDERLINE")}     ║\n║ {Color("Páncélzat", "DARK_CYAN")}       │ {Color("Páncélzat", "DARK_CYAN")}       ║\n║ {Color("Mellkas", "DARK_CYAN")}         │ {ColorFormat("Fej", "DARK_CYAN", "BOLD")}             ║\n║ {Color("- - - - - - - -", "DARK_GREY")} │ {Color("- - - - - - - -", "DARK_GREY")} ║\n║ {Format("Védelem", "BOLD")}: {ColorFormat("+50", "GREEN", "HIGHLIGHT")}    │ {Format("Védelem", "BOLD")}: {ColorFormat("+10", "GREEN", "HIGHLIGHT")}    ║\n║ Gyorsaság: {ColorFormat("-10", "ORANGE", "HIGHLIGHT")}  │ Stun: {ColorFormat("-10%", "GREEN", "HIGHLIGHT")}      ║\n║ Lopakodás: {ColorFormat("-5", "ORANGE", "HIGHLIGHT")}   │                 ║\n╚═════════════════╧═════════════════╝\n";
            return coloredText.Split(new string[] { "\n" }, StringSplitOptions.None);
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
