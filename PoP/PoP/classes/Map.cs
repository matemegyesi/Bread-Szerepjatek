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

        public int CurrentLocation { get; set; }

        public readonly string path;

        public Dictionary<string, Location> locations = new Dictionary<string, Location>();

        public Map(string path) 
        {
            this.path = path;

            maps.Add(this);
        }
        public void LoadMap()
        {
            GameLoop.display.DrawMap(path);
        }
   } 
}
