using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    internal class Border
    {
        #region Border Properties
        public const char DOUBLE_HORIZONTAL = '═';
        public const char DOUBLE_VERTICAL = '║';
        public const char DOUBLE_TOPLEFT = '╔';
        public const char DOUBLE_TOPRIGHT = '╗';
        public const char DOUBLE_BOTTOMRIGHT = '╝';
        public const char DOUBLE_BOTTOMLEFT = '╚';

        public const char DOUBLE_T_TOP = '╦';
        public const char DOUBLE_T_RIGHT = '╣';
        public const char DOUBLE_T_BOTTOM = '╩';
        public const char DOUBLE_T_LEFT = '╠';

        public const char SINGLE_HORIZONTAL = '─';
        public const char SINGLE_VERTICAL = '│';
        public const char SINGLE_TOPLEFT = '┌';
        public const char SINGLE_TOPRIGHT = '┐';
        public const char SINGLE_BOTTOMRIGHT = '┘';
        public const char SINGLE_BOTTOMLEFT = '└';

        public const char SINGLE_T_TOP = '┬';
        public const char SINGLE_T_RIGHT = '┤';
        public const char SINGLE_T_BOTTOM = '┴';
        public const char SINGLE_T_LEFT = '├';

        public const char DOUBLE_TO_SINGLE_T_TOP = '╤';
        public const char DOUBLE_TO_SINGLE_T_RIGHT = '╢';
        public const char DOUBLE_TO_SINGLE_T_BOTTOM = '╧';
        public const char DOUBLE_TO_SINGLE_T_LEFT = '╟';

        public const char SINGLE_TO_DOUBLE_T_TOP = '╥';
        public const char SINGLE_TO_DOUBLE_T_RIGHT = '╡';
        public const char SINGLE_TO_DOUBLE_T_BOTTOM = '╨';
        public const char SINGLE_TO_DOUBLE_T_LEFT = '╞';

        public const char CURVED_TOPLEFT = '╭';
        public const char CURVED_TOPRIGHT = '╮';
        public const char CURVED_BOTTOMRIGHT = '╯';
        public const char CURVED_BOTTOMLEFT = '╰';

        public const char SHADE_FULL = '█';
        public const char SHADE_DARK = '▓';
        public const char SHADE_MEDIUM = '▒';
        public const char SHADE_LIGHT = '░';
        #endregion

        public bool HasTop;
        public bool HasRight;
        public bool HasBottom;
        public bool HasLeft;

        public char Horizontal { get; set; } = '═';
        public char Vertical { get; set; } = '║';
        public char TopLeft { get; set; } = '╔';
        public char TopRight { get; set; } = '╗';
        public char BottomRight { get; set; } = '╝';
        public char BottomLeft { get; set; } = '╚';
        public List<Intersection> IntersectionList { get; set; } = new List<Intersection>();

        public Border(bool singleLined = false)
        {
            HasTop = true;
            HasRight = true;
            HasBottom = true;
            HasLeft = true;

            if (singleLined)
            {
                Horizontal = '─';
                Vertical = '│';
                TopLeft = '┌';
                TopRight = '┐';
                BottomRight = '┘';
                BottomLeft = '└';
            }
        }

        public Border(bool hasTop, bool hasRight, bool hasBottom, bool hasLeft, bool singleLined = false)
        {
            HasTop = hasTop;
            HasRight = hasRight;
            HasBottom = hasBottom;
            HasLeft = hasLeft;

            if (singleLined)
            {
                Horizontal = '─';
                Vertical = '│';
                TopLeft = '┌';
                TopRight = '┐';
                BottomRight = '┘';
                BottomLeft = '└';
            }
        }

        /// <summary>
        /// Depending on the settings, it surrounds a list of strings with walls.
        /// </summary>
        /// <param name="content">The list that needs borders around it.</param>
        /// <param name="lineLength">The length a line.</param>
        /// <returns>A list where the content is surrounded.</returns>
        public List<string> Surround(List<string> content, int lineLength)
        {
            List<string> surroundedContent = new List<string>();

            if (HasTop)
            {
                string firstLine = string.Empty;

                if (HasLeft && !HasRight)
                {
                    firstLine = TopLeft + Style.GetBlankLine(lineLength, Horizontal);
                }
                else if (!HasLeft && HasRight)
                {
                    firstLine = Style.GetBlankLine(lineLength, Horizontal) + TopRight;
                }
                else
                {
                    firstLine = TopLeft + Style.GetBlankLine(lineLength, Horizontal) + TopRight;
                }

                surroundedContent.Add(firstLine);
            }

            if (HasLeft || HasRight)
            {
                if (HasLeft && HasRight)
                {
                    foreach (string line in content)
                    {
                        string currentLine = string.Empty;
                        currentLine += Vertical + line + Vertical;
                        surroundedContent.Add(currentLine);
                    }
                }
                else if (HasLeft)
                {
                    foreach (string line in content)
                    {
                        string currentLine = string.Empty;
                        currentLine += Vertical + line;
                        surroundedContent.Add(currentLine);
                    }
                }
                else if (HasRight)
                {
                    foreach (string line in content)
                    {
                        string currentLine = string.Empty;
                        currentLine += line + Vertical;
                        surroundedContent.Add(currentLine);
                    }
                }
            }
            else
            {
                surroundedContent.AddRange(content);
            }

            if (HasBottom)
            {
                string lastLine = string.Empty;

                if (HasLeft && !HasRight)
                {
                    lastLine = BottomLeft + Style.GetBlankLine(lineLength, Horizontal);
                }
                else if (!HasLeft && HasRight)
                {
                    lastLine = Style.GetBlankLine(lineLength, Horizontal) + BottomRight;
                }
                else
                {
                    lastLine = BottomLeft + Style.GetBlankLine(lineLength, Horizontal) + BottomRight;
                }

                surroundedContent.Add(lastLine);
            }

            if (IntersectionList.Count > 0)
            {
                foreach (Intersection i in IntersectionList)
                {
                    switch (i.Side)
                    {
                        case BorderSide.Top:

                            if (i.Position <= (lineLength + 2) - 1 && i.Position >= 0 && HasTop)
                            {
                                surroundedContent[0] = surroundedContent[0].Remove(i.Position, 1).Insert(i.Position, i.Character.ToString());
                            }

                            break;
                        case BorderSide.Right:

                            if (i.Position <= surroundedContent.Count - 1 && i.Position >= 0 && HasRight)
                            {
                                surroundedContent[i.Position] = surroundedContent[i.Position].Remove(surroundedContent[i.Position].Length - 1, 1).Insert(surroundedContent[i.Position].Length - 1, i.Character.ToString());
                            }

                            break;
                        case BorderSide.Bottom:

                            if (i.Position <= (lineLength + 2) - 1 && i.Position >= 0 && HasBottom)
                            {
                                surroundedContent[surroundedContent.Count - 1] = surroundedContent[surroundedContent.Count - 1].Remove(i.Position, 1).Insert(i.Position, i.Character.ToString());
                            }

                            break;
                        case BorderSide.Left:

                            if (i.Position <= surroundedContent.Count - 1 && i.Position >= 0 && HasLeft)
                            {
                                surroundedContent[i.Position] = surroundedContent[i.Position].Remove(0, 1).Insert(0, i.Character.ToString());
                            }

                            break;
                    }
                }
            }

            return surroundedContent;
        }
    }
}
