﻿using System;
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

        /// <summary>
        /// It gives a list of the lines of the window.
        /// </summary>
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

        /// <summary>
        /// Generates the uncached or changed parts of the window.
        /// </summary>
        /// <returns>A list of the lines of the window.</returns>
        protected virtual List<string> GenerateLines()
        {
            LineList.Clear();

            // Content

            return LineList;
        }

        /// <summary>
        /// Appends a line full of spaces to the bottom of the window.
        /// </summary>
        protected virtual void AddBlankLine()
        {
            LineList.Add(Style.GetBlankLine(Width));
        }

        /// <summary>
        /// Appends specified amount of lines full of spaces to the bottom of the window.
        /// </summary>
        /// <param name="lineAmount">The amount of blank lines.</param>
        protected virtual void AddBlankLine(int lineAmount)
        {
            for (int i = 0; i < lineAmount; i++)
            {
                LineList.Add(Style.GetBlankLine(Width));
            }
        }

        /// <summary>
        /// Appends a line full of spaces to the bottom of the window.
        /// </summary>
        /// <param name="localList">The local list that will be changed.</param>
        protected virtual void AddBlankLineLocal(ref List<string> localList)
        {
            localList.Add(Style.GetBlankLine(Width));
        }

        /// <summary>
        /// Appends a line (or lines if the text is too long) to the bottom of the window.
        /// </summary>
        /// <param name="text">The content of the line(s).</param>
        /// <param name="alignedLeft">The alignment of the content.</param>
        protected virtual void AddLine(string text, bool alignedLeft = true) // If the text is multi-line long, then the formatting will be removed. // Don't use: \n \t
        {
            string textAnsiPurged = Style.PurgeAnsi(text);

            if (textAnsiPurged.Length > Width)
            {
                foreach (string line in Style.BreakLine(textAnsiPurged, Width))
                {
                    if (alignedLeft)
                        LineList.Add(line + Style.GetRemainingSpace(line, Width));
                    else
                        LineList.Add(Style.GetRemainingSpace(line, Width) + line);
                }
            }
            else if (textAnsiPurged.Length < Width)
            {
                if (alignedLeft)
                    LineList.Add(text + Style.GetRemainingSpace(textAnsiPurged, Width));
                else
                    LineList.Add(Style.GetRemainingSpace(textAnsiPurged, Width) + text);
            }
            else if (textAnsiPurged.Length == Width)
            {
                LineList.Add(text);
            }
        }

        /// <summary>
        /// Appends a line (or lines if the text is too long) to the bottom of a local list.
        /// </summary>
        /// <param name="localList">The local list that will be changed.</param>
        /// <param name="text">The content of the line(s).</param>
        /// <param name="hasMargin">Optionally adds an indent to the beginning of the line(s).</param>
        /// <param name="alignedLeft">The alignment of the content.</param>
        protected virtual void AddLineLocal(ref List<string> localList, string text, bool alignedLeft = true, bool hasMargin = false)
        {
            string textAnsiPurged = Style.PurgeAnsi(text);

            if (textAnsiPurged.Length > Width)
            {
                foreach (string line in Style.BreakLine(textAnsiPurged, Width, hasMargin))
                {
                    if (alignedLeft)
                        localList.Add(line + Style.GetRemainingSpace(line, Width));
                    else
                        localList.Add(Style.GetRemainingSpace(line, Width) + line);
                }
            }
            else if (textAnsiPurged.Length < Width)
            {
                if (alignedLeft)
                    localList.Add(text + Style.GetRemainingSpace(textAnsiPurged, Width));
                else
                    localList.Add(Style.GetRemainingSpace(textAnsiPurged, Width) + text);
            }
            else if (textAnsiPurged.Length == Width)
            {
                localList.Add(text);
            }
        }

        /// <summary>
        /// Inserts a line full of spaces to the specified index of the window.
        /// </summary>
        /// <param name="index">The index of where it should be inserted to.</param>
        protected virtual void InsertBlankLine(int index)
        {
            LineList.Insert(index, Style.GetBlankLine(Width));
        }

        /// <summary>
        /// Inserts a line (or lines if the text is too long) to the specified index of the window.
        /// </summary>
        /// <param name="index">The index of where it should be inserted to.</param>
        /// <param name="text">The content of the line(s).</param>
        /// <param name="hasMargin">Optionally adds an indent to the beginning of the line(s).</param>
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

        /// <summary>
        /// Inserts a range of lines starting from the specified index of the window.
        /// </summary>
        /// <param name="index">The index of where it should be inserted to.</param>
        /// <param name="textRange">The content of the lines.</param>
        /// <param name="hasMargin">Optionally adds an indent to the beginning of the line(s).</param>
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

        /// <summary>
        /// It forces the window to look for changes at the next render.
        /// </summary>
        public void ForceUpdate()
        {
            HasChanged = true;
        }
    }
}
