using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    /// <summary>
    /// The possible sides of a wall.
    /// </summary>
    public enum BorderSide
    {
        /// <summary>
        /// Top side of the wall.
        /// </summary>
        Top,
        /// <summary>
        /// Right side of the wall.
        /// </summary>
        Right,
        /// <summary>
        /// Bottom side of the wall.
        /// </summary>
        Bottom,
        /// <summary>
        /// Left side of the wall.
        /// </summary>
        Left
    }

    internal class Intersection
    {
        public BorderSide Side { get; private set; }
        public int Position { get; private set; }
        public char Character { get; private set; }

        public Intersection(BorderSide side, int position, char character)
        {
            Side = side;
            Position = position;
            Character = character;
        }
    }
}
