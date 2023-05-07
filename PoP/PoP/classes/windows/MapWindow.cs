using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PoP.classes.windows
{
    internal class MapWindow : Window
    {
        static private Tile[,] map = new Tile[46, 147];
        private bool isMapLoaded;
        public List<Location> LocationList = new List<Location>();

        public int PosX { get; set; }
        public int PosY { get; set; }

        // Change detection
        List<int> changedRows = new List<int>();

        public MapWindow()
        {
            Height = 46;
            Width = 148;

            isMapLoaded = false;

            // Importing the map
            // -- Not here.

            // Importing the coloring (WIP)
            // ...
        }

        protected override List<string> GenerateLines()
        {

            if (isMapLoaded)
            {
                if (CachedLineList.Count != Height)
                {
                    LineList.Clear();

                    for (int row = 0; row < map.GetLength(0); row++)
                    {
                        string newLine = GenerateRow(row);

                        AddLine(newLine);
                    }

                    return LineList;
                }
                else
                {
                    LineList = CachedLineList;

                    foreach (int rowIndex in changedRows)
                    {
                        LineList.RemoveAt(rowIndex);
                        LineList.Insert(rowIndex, GenerateRow(rowIndex));
                    }

                    return CachedLineList;
                }
            }
            else
            {
                return new List<string>();
            }

        }

        private void GenerateLocationMarker(Location loc, ColorAnsi color = ColorAnsi.DARK_BLUE)
        {
            if (isMapLoaded && !loc.isHidden)
            {
                // Setting the color
                if (loc.isCompleted)
                {
                    color = ColorAnsi.DARK_GREY;
                }

                string _innerText = Style.AddPadding(loc.Name);

                // Writing the card
                int _cardPosX = loc.positionX;

                if (loc.positionX < Width / 2)
                {
                    _cardPosX += 4;

                    map[loc.positionY, _cardPosX].Char = Border.SINGLE_TO_DOUBLE_T_RIGHT;
                    map[loc.positionY, _cardPosX + _innerText.Length + 1].Char = Border.SINGLE_VERTICAL;
                }
                else
                {
                    _cardPosX -= (4 + _innerText.Length + 1);

                    map[loc.positionY, _cardPosX].Char = Border.SINGLE_VERTICAL;
                    map[loc.positionY, _cardPosX + _innerText.Length + 1].Char = Border.SINGLE_TO_DOUBLE_T_LEFT;
                }

                map[loc.positionY, _cardPosX].SetStartStyle(Style.GetColor(color));

                // -- Top border
                for (int i = 0; i < _innerText.Length + 2; i++)
                {
                    if (i == 0)
                    {
                        map[loc.positionY - 1, _cardPosX + i].Char = Border.CURVED_TOPLEFT;
                        map[loc.positionY - 1, _cardPosX + i].SetStartStyle(Style.GetColor(color));
                    }
                    else if (i == _innerText.Length + 1)
                    {
                        map[loc.positionY - 1, _cardPosX + i].Char = Border.CURVED_TOPRIGHT;
                        map[loc.positionY - 1, _cardPosX + i].SetEndStyle(Style.END);
                    }
                    else
                    {
                        map[loc.positionY - 1, _cardPosX + i].Char = Border.SINGLE_HORIZONTAL;
                    }
                }

                // -- Inner text
                for (int i = 0; i < _innerText.Length; i++)
                {
                    map[loc.positionY, _cardPosX + i + 1].Char = _innerText[i];

                    if (i == 0)
                    {
                        map[loc.positionY, _cardPosX + 1].SetStartStyle(Style.GetFormat(FormatAnsi.HIGHLIGHT));
                    }

                    if (i == _innerText.Length - 1)
                    {
                        map[loc.positionY, _cardPosX + i + 1].SetEndStyle(Style.END);
                    }
                }

                // -- Bottom border
                for (int i = 0; i < _innerText.Length + 2; i++)
                {
                    if (i == 0)
                    {
                        map[loc.positionY + 1, _cardPosX + i].Char = Border.CURVED_BOTTOMLEFT;
                        map[loc.positionY + 1, _cardPosX + i].SetStartStyle(Style.GetColor(color));
                    }
                    else if (i == _innerText.Length + 1)
                    {
                        map[loc.positionY + 1, _cardPosX + i].Char = Border.CURVED_BOTTOMRIGHT;
                        map[loc.positionY + 1, _cardPosX + i].SetEndStyle(Style.END);
                    }
                    else
                    {
                        map[loc.positionY + 1, _cardPosX + i].Char = Border.SINGLE_HORIZONTAL;
                    }
                }

                map[loc.positionY, _cardPosX + _innerText.Length + 1].SetStartStyle(Style.GetColor(color));
                map[loc.positionY, _cardPosX + _innerText.Length + 1].SetEndStyle(Style.END);

                // Writing the arrow
                string _arrow = Style.GetBlankLine(2, Border.DOUBLE_HORIZONTAL);
                int _arrowPosX = loc.positionX;

                if (loc.positionX < Width / 2)
                {
                    _arrow = _arrow.Insert(0, "◄");
                    _arrowPosX += 1;
                }
                else
                {
                    _arrow += "►";
                    _arrowPosX -= 3;
                }

                map[loc.positionY, _arrowPosX].SetStartStyle(Style.GetColor(color));
                for (int i = 0; i < _arrow.Length; i++)
                {
                    map[loc.positionY, _arrowPosX + i].Char = _arrow[i];
                }
                map[loc.positionY, _arrowPosX + _arrow.Length - 1].SetEndStyle(Style.END);

                //map[loc.positionY, loc.positionX].Char = '×';

                // Changes
                changedRows.Add(loc.positionY - 1);
                changedRows.Add(loc.positionY);
                changedRows.Add(loc.positionY + 1);
            }
        }

        private string GenerateRow(int rowIndex)
        {
            Tile[] row = Enumerable.Range(0, map.GetLength(1)).Select(x => map[rowIndex, x]).ToArray();
            string newLine = string.Empty;

            for (int col = 0; col < map.GetLength(1); col++)
            {

                if (rowIndex == PosY && col == PosX)
                {
                    newLine += Style.Color('H', ColorAnsi.RED);
                }
                else
                {
                    newLine += row[col].StartStyle + row[col].Char + row[col].EndStyle;
                }

            }

            return newLine + ' ';
        }

        /// <summary>
        /// Moves the Player (H) to the specified position for the next render.
        /// </summary>
        /// <param name="newPosX">X position index.</param>
        /// <param name="newPosY">Y position index.</param>
        public void MoveCharacterMarker(int newPosX, int newPosY)
        {
            if (newPosY != PosY)
            {
                changedRows.Add(PosY);
            }
            changedRows.Add(newPosY);

            PosX = newPosX;
            PosY = newPosY;

            HasChanged = true;
        }

        /// <summary>
        /// Updates the specified Location's marker for the next render.
        /// </summary>
        /// <param name="loc">The location that will be changed.</param>
        /// <param name="color">The color the marker will have when the Location isn't completed or hidden.</param>
        public void UpdateLocationMarker(Location loc, ColorAnsi color = ColorAnsi.DARK_BLUE)
        {
            GenerateLocationMarker(loc, color);

            HasChanged = true;
        }

        /// <summary>
        /// Imports the map with the specified path into the window's map matrix.
        /// </summary>
        /// <param name="filePath">The path to the ASCII art map file.</param>
        public void ImportMap(string filePath)
        {
            int _rowIndex = 0;
            foreach (string row in File.ReadAllLines(filePath, Encoding.UTF8))
            {
                int _colIndex = 0;
                foreach (char col in row)
                {
                    map[_rowIndex, _colIndex] = new Tile(col);

                    _colIndex++;
                }

                _rowIndex++;
            }

            isMapLoaded = true;

            foreach (Location loc in LocationList)
            {
                GenerateLocationMarker(loc);
            }

            LineList.Clear();
            HasChanged = true;
        }
    }
}
