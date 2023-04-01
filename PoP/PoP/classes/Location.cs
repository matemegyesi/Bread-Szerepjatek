using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    internal abstract class Location
    {

        public readonly int Id;
        public int positionX { get; private set; }
        public int positionY { get; private set; }

        public Location(int id, int x, int y)
        {
            Id = id;
            positionX = x;
            positionY = y;
        }

    }
}
