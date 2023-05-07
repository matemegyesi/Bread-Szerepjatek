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

        public List<string> GetLines
        {
            get
            {
                if (CachedLineList.Count == Height && !HasChanged)
                {
                    return CachedLineList;
                }
                else
                {
                    List<string> ChangedLineList = GenerateLines();
                    HasChanged = false;

                    // Deletes additional lines
                    if (ChangedLineList.Count > Height)
                    {
                        ChangedLineList.RemoveRange(Height, ChangedLineList.Count - Height);
                    }

                    // Fills the remaining lines
                    int remainingLineCount = Height - ChangedLineList.Count;
                    for (int i = 0; i < remainingLineCount; i++)
                    {
                        ChangedLineList.Add(Style.GetBlankLine(Width));
                    }

                    CachedLineList = ChangedLineList;

                    return CachedLineList;
                }
            }
        }

        protected virtual List<string> GenerateLines()
        {
            LineList.Clear();

            // Content

            return LineList;
        }

        protected virtual void AddBlankLine()
        {
            LineList.Add(Style.GetBlankLine(Width));
        }

        protected virtual void AddBlankLineLocal(ref List<string> localList)
        {
            localList.Add(Style.GetBlankLine(Width));
        }

        protected virtual void AddLine(string text, bool hasMargin = false) // If the text is multi-line long, then the formatting will be removed. // Don't use: \n \t
        {
            string textAnsiPurged = Style.PurgeAnsi(text);

            if (textAnsiPurged.Length > Width)
            {
                foreach (string line in Style.BreakLine(textAnsiPurged, Width, hasMargin))
                {
                    LineList.Add(line + Style.GetRemainingSpace(line, Width));
                }
            }
            else if (textAnsiPurged.Length < Width)
            {
                LineList.Add(text + Style.GetRemainingSpace(textAnsiPurged, Width));
            }
            else if (textAnsiPurged.Length == Width)
            {
                LineList.Add(text);
            }
        }

        protected virtual void AddLineLocal(ref List<string> localList, string text, bool hasMargin = false)
        {
            string textAnsiPurged = Style.PurgeAnsi(text);

            if (textAnsiPurged.Length > Width)
            {
                foreach (string line in Style.BreakLine(textAnsiPurged, Width, hasMargin))
                {
                    localList.Add(line + Style.GetRemainingSpace(line, Width));
                }
            }
            else if (textAnsiPurged.Length < Width)
            {
                localList.Add(text + Style.GetRemainingSpace(textAnsiPurged, Width));
            }
            else if (textAnsiPurged.Length == Width)
            {
                localList.Add(text);
            }
        }

        protected virtual void InsertBlankLine(int index)
        {
            LineList.Insert(index, Style.GetBlankLine(Width));
        }

        protected virtual void InsertLine(int index, string text, bool hasMargin = false)
        {
            string textAnsiPurged = Style.PurgeAnsi(text);

            if (textAnsiPurged.Length > Width)
            {
                foreach (string line in Style.BreakLine(textAnsiPurged, Width, hasMargin))
                {
                    LineList.Insert(index++, line + Style.GetRemainingSpace(line, Width));
                }
            }
            else if (textAnsiPurged.Length < Width)
            {
                LineList.Insert(index, text + Style.GetRemainingSpace(textAnsiPurged, Width));
            }
            else if (textAnsiPurged.Length == Width)
            {
                LineList.Insert(index, text);
            }
        }

        protected virtual void InsertLineRange(int index, List<string> textRange, bool hasMargin = false)
        {
            for (int i = textRange.Count - 1; i >= 0; i--)
            {
                string textAnsiPurged = Style.PurgeAnsi(textRange[i]);

                if (textAnsiPurged.Length > Width)
                {
                    foreach (string line in Style.BreakLine(textAnsiPurged, Width, hasMargin))
                    {
                        LineList.Insert(index++, line + Style.GetRemainingSpace(line, Width));
                    }
                }
                else if (textAnsiPurged.Length < Width)
                {
                    LineList.Insert(index, textRange[i] + Style.GetRemainingSpace(textAnsiPurged, Width));
                }
                else if (textAnsiPurged.Length == Width)
                {
                    LineList.Insert(index, textRange[i]);
                }
            }
        }

        public void ForceUpdate()
        {
            HasChanged = true;
        }
    }
}
