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

        /// <summary>
        /// Gives back the ANSI escape code that stops every styling.
        /// </summary>
        public const string END = "\u001b[0m";


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

        /// <summary>
        /// Surrounds the character with ANSI escape code, changing its color.
        /// </summary>
        /// <param name="character">The character that needs to be stylized.</param>
        /// <param name="color">Enum of the color.</param>
        public static string Color(char character, ColorAnsi color)
        {
            try
            {
                string formattedText = AnsiColor((byte)color) + character + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Surrounds the number with ANSI escape code, changing its color.
        /// </summary>
        /// <param name="number">The number that needs to be stylized.</param>
        /// <param name="color">Enum of the color.</param>
        public static string Color(int number, ColorAnsi color)
        {
            try
            {
                string formattedText = AnsiColor((byte)color) + number.ToString() + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Creates an ANSI escape code of the parameter.
        /// </summary>
        /// <param name="color">Enum of the color.</param>
        /// <returns>ANSI escape code of the specified color.</returns>
        public static string GetColor(ColorAnsi color)
        {
            return AnsiColor((byte)color);
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

        /// <summary>
        /// Surrounds the character with ANSI escape code, changing its format.
        /// </summary>
        /// <param name="character">The character that needs to be stylized.</param>
        /// <param name="format">Enum of the format.</param>
        public static string Format(char character, FormatAnsi format)
        {
            try
            {
                string formattedText = AnsiFormat((byte)format) + character + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Surrounds the number with ANSI escape code, changing its format.
        /// </summary>
        /// <param name="number">The number that needs to be stylized.</param>
        /// <param name="format">Enum of the format.</param>
        public static string Format(int number, FormatAnsi format)
        {
            try
            {
                string formattedText = AnsiFormat((byte)format) + number.ToString() + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Creates an ANSI escape code of the parameter.
        /// </summary>
        /// <param name="format">Enum of the format.</param>
        /// <returns>ANSI escape code of the specified format.</returns>
        public static string GetFormat(FormatAnsi format)
        {
            return AnsiFormat((byte)format);
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

        /// <summary>
        /// Surrounds the character with ANSI escape code, changing its color and format.
        /// </summary>
        /// <param name="character">The character that needs to be stylized.</param>
        /// <param name="color">Enum of the color.</param>
        /// <param name="format">Enum of the format.</param>
        public static string ColorFormat(char character, ColorAnsi color, FormatAnsi format)
        {
            try
            {
                string formattedText = AnsiColor((byte)color) + AnsiFormat((byte)format) + character + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Surrounds the number with ANSI escape code, changing its color and format.
        /// </summary>
        /// <param name="number">The number that needs to be stylized.</param>
        /// <param name="color">Enum of the color.</param>
        /// <param name="format">Enum of the format.</param>
        public static string ColorFormat(int number, ColorAnsi color, FormatAnsi format)
        {
            try
            {
                string formattedText = AnsiColor((byte)color) + AnsiFormat((byte)format) + number.ToString() + END;
                return formattedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        // Aligning

        /// <summary>
        /// [OBSOLETE] Gives back the space before and after the text that is aligned in the center.
        /// </summary>
        /// <param name="text">The text that needs to be aligned in the center.</param>
        /// <param name="lineLength">The length of the line.</param>
        /// <returns>An AlignedText class.</returns>
        public static AlignedText AlignCenterSpaces(string text, int lineLength)
        {
            int spaceBefore = (lineLength - text.Length) / 2;
            int spaceAfter = lineLength - spaceBefore - text.Length;

            AlignedText currentAlignedText = new AlignedText();
            for (int _ = 0; _ < spaceBefore; _++)
            {
                currentAlignedText.Before += ' ';
            }
            for (int _ = 0; _ < spaceAfter; _++)
            {
                currentAlignedText.After += ' ';
            }

            return currentAlignedText;
        }

        /// <summary>
        /// Gives back the remaining space of a line that already has some text taking up space of it.
        /// </summary>
        /// <param name="text">The text that takes up some space.</param>
        /// <param name="lineLength">The length of the line.</param>
        /// <returns>A string of spaces.</returns>
        public static string GetRemainingSpace(string text, int lineLength)
        {
            string remainingText = string.Empty;
            for (int _ = 0; _ < lineLength - text.Length; _++)
            {
                remainingText += ' ';
            }
            return remainingText;
        }

        /// <summary>
        /// Gives back the remaining space of a line that already has some text taking up space of it.
        /// </summary>
        /// <param name="textLength">The amount of space that the text that takes up.</param>
        /// <param name="lineLength">The length of the line.</param>
        /// <returns>A string of spaces.</returns>
        public static string GetRemainingSpace(int textLength, int lineLength)
        {
            string remainingText = string.Empty;
            for (int _ = 0; _ < lineLength - textLength; _++)
            {
                remainingText += ' ';
            }
            return remainingText;
        }

        // Line generating

        /// <summary>
        /// Gives back a line with the specified length made up of spaces.
        /// </summary>
        /// <param name="lineLength">The length of the line.</param>
        /// <returns>The blank line.</returns>
        public static string GetBlankLine(int lineLength)
        {
            string newLine = string.Empty;
            for (int _ = 0; _ < lineLength; _++)
            {
                newLine += ' ';
            }
            return newLine;
        }

        /// <summary>
        /// Gives back a line with the specified length of the specified character.
        /// </summary>
        /// <param name="lineLength">The length of the line.</param>
        /// <param name="fill">The cahracter that the line should be filled up with.</param>
        /// <returns>The filled line.</returns>
        public static string GetBlankLine(int lineLength, char fill)
        {
            string newLine = string.Empty;
            for (int _ = 0; _ < lineLength; _++)
            {
                newLine += fill;
            }
            return newLine;
        }

        /// <summary>
        /// Gives back a dashed line.
        /// </summary>
        /// <param name="lineLength">The length of the line.</param>
        /// <returns>Dashed line.</returns>
        public static string GetDashedLine(int lineLength)
        {
            char border = Border.SINGLE_HORIZONTAL;

            string newLine = string.Empty;
            if (lineLength % 2 != 0)
            {
                for (int i = 0; i < lineLength; i++)
                {
                    if (i % 2 == 0)
                    {
                        newLine += ' ';
                    }
                    else
                    {
                        newLine += border;
                    }
                }
            }
            else
            {
                for (int i = 0; i < lineLength; i++)
                {
                    if (i == lineLength / 2)
                    {
                        if (lineLength % 4 == 0)
                        {
                            newLine += border;
                        }
                        else
                        {
                            newLine += ' ';
                        }
                    }
                    else if (i < lineLength / 2)
                    {
                        if (i % 2 == 0)
                        {
                            newLine += ' ';
                        }
                        else
                        {
                            newLine += border;
                        }
                    }
                    else if (i > lineLength / 2)
                    {
                        if (i % 2 == 0)
                        {
                            newLine += border;
                        }
                        else
                        {
                            newLine += ' ';
                        }
                    }
                }
            }

            return newLine;
        }

        /// <summary>
        /// Word wraps the text.
        /// </summary>
        /// <param name="text">The text that is longer than the width.</param>
        /// <param name="width">The max width of a line.</param>
        /// <param name="hasMargin">Adds indent to the beginning of the texts.</param>
        /// <returns>The word wrapped list of text.</returns>
        public static List<string> BreakLine(string text, int width, bool hasMargin = false)
        {
            List<string> brokenText = new List<string>();
            string[] splitText = text.Split(' ');

            string currentLine = hasMargin ? " " : "";
            for (int i = 0; i < splitText.Length; i++)
            {
                if (splitText[i].Length > width)
                {
                    brokenText.Add(splitText[i].Substring(0, width));
                }
                else if (currentLine.Length + splitText[i].Length + 1 <= width)
                {
                    currentLine += splitText[i] + ' ';
                }
                else
                {
                    brokenText.Add(currentLine);
                    currentLine = hasMargin ? " " : "";
                    currentLine += splitText[i] + ' ';
                }

                if (i == splitText.Length - 1)
                {
                    brokenText.Add(currentLine);
                }
            }

            return brokenText;
        }

        /// <summary>
        /// Gives back a slider.
        /// </summary>
        /// <param name="length">The full length of the slider.</param>
        /// <param name="percent">Number between 0 and 1.</param>
        /// <param name="color">The color of the slider.</param>
        /// <returns>A string of the slider.</returns>
        public static string GetSlider(int length, double percent, ColorAnsi color/*, bool showPercentage = true*/)
        {
            string sliderText = string.Empty;
            
            if (percent >= 1.0 || percent <= 0.0)
            {
                string fill = GetBlankLine((int)Math.Round(length * percent), Border.SHADE_FULL);
                string blank = GetBlankLine(length - fill.Length, Border.SHADE_LIGHT);
                sliderText = GetColor(color) + fill + blank + END;
            }
            else if (percent >= .5)
            {
                string percentText = percent.ToString(" 0% ");

                string fill = GetBlankLine((int)Math.Round(length * percent) - percentText.Length, Border.SHADE_FULL);
                string blank = GetBlankLine(length - (fill.Length + percentText.Length), Border.SHADE_LIGHT);
                sliderText = GetColor(color) + fill + Format(percentText, FormatAnsi.HIGHLIGHT) + GetColor(color) + blank + END;
            }
            else if (percent < .5)
            {
                string percentText = percent.ToString(Border.SHADE_LIGHT + "0%" + Border.SHADE_LIGHT);

                string fill = GetBlankLine((int)Math.Round(length * percent), Border.SHADE_FULL);
                string blank = GetBlankLine(length - (fill.Length + percentText.Length), Border.SHADE_LIGHT);
                sliderText = GetColor(color) + fill + percentText + blank + END;
            }

            return sliderText;
        }

        // Spacing

        /// <summary>
        /// Adds a space to the beginning and to the ending of the text.
        /// </summary>
        /// <param name="text">The text that needs to be padded.</param>
        /// <returns>The padded text.</returns>
        public static string AddPadding(string text)
        {
            return ' ' + text + ' ';
        }

        // Unholy act...

        /// <summary>
        /// Exterminates the ANSI escape code from the specified text.
        /// </summary>
        /// <param name="corruptedText">The text plagued with heresy.</param>
        /// <returns>Holy text.</returns>
        public static string PurgeAnsi(string corruptedText)
        {
            if (corruptedText.Contains("\u001b"))
            {
                string newText = string.Empty;
                bool notAnsi = true;
                for (int i = 0; i < corruptedText.Length; i++)
                {
                    if (char.IsControl(corruptedText[i]))
                    {
                        notAnsi = false;
                    }
                    else if (corruptedText[i] == 'm' && !notAnsi)
                    {
                        notAnsi = true;
                    }
                    else if (notAnsi)
                    {
                        newText += corruptedText[i];
                    }
                }

                return newText;
            }
            else
            {
                return corruptedText;
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
