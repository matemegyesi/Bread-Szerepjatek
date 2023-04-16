using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    enum LocationType
    {
        DIALOGUE,
        COMBAT
    }
    internal abstract class Location
    {

        public int id;
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool isCompleted { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public Location(int id, int x, int y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            isCompleted = false;
        }

        public abstract void LoadLocation(); 
        public abstract void Start();
        public abstract void End();

    }
}
