using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    enum LocationType { 
        DIALOGUE,
        COMBAT
    }
	class Map
	{
        public static List<Map> maps = new List<Map>();

        public int CurrentLocation { get; set; }

        public readonly string path;

        public Dictionary<int, Location> locations = new Dictionary<int, Location>();

        public Map(string path) 
        {
            this.path = path;

            maps.Add(this);
        }
        public void LoadMap()
        {
            GameLoop.display.DrawMap(path);
        }
        public void AddLocation(int id, int x, int y, LocationType type)
        {
            switch (type)
            {
                case LocationType.DIALOGUE:
                    locations.Add(id, new Dialogue(id, x, y));
                    break;
                case LocationType.COMBAT:
                    locations.Add(id, new Combat(id, x, y));
                    break;
                default:
                    break;
            }
        }
   } 
}
