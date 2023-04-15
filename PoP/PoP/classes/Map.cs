using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{

	class Map
	{
        public static List<Map> maps = new List<Map>();

        public static Map CurrentMap { get; set; }
        public static Location CurrentLocation { get; set; }

        public readonly string path;

        public List<Location> locations = new List<Location>();

        public Map(string path) 
        {
            this.path = path;

            maps.Add(this);
        }
        public void LoadMap()
        {
            GameLoop.display.DrawMap(path);
            CurrentMap = this;
        }
        public void AddLocation(int id, int x, int y, LocationType type, string path)
        {
            switch (type)
            {
                case LocationType.DIALOGUE:
                    locations.Add(new Dialogue(id, x, y, ""));
                    break;
                case LocationType.COMBAT:
                    locations.Add(new Combat(id, x, y, ""));
                    break;
                default:
                    break;
            }
        }
   } 
}
