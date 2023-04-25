using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace PoP.classes
{

	class Map
	{
        /// <summary> 
        /// A list of all maps in the game
        /// </summary>
        public static List<Map> maps = new List<Map>();
        
        /// <summary>
        /// The current map the player is on
        /// </summary>
        public static Map CurrentMap { get; set; }

        /// <summary>
        /// The current location the player is on 
        /// </summary>
        public static Location CurrentLocation { get; set; }

        /// <summary>
        /// The path to the image file of the map
        /// </summary>
        public readonly string path;

        /// <summary>
        /// A list of all the locations on the map
        /// </summary>
        public List<Location> locations = new List<Location>();

        /// <summary>
        /// Constructs a new map with the given path and adds it to the game's list of maps.
        /// </summary>
        /// <param name="path">The path to the image file of the map</param>
        public Map(string path) 
        {
            this.path = path;

            maps.Add(this);
        }

        /// <summary>
        /// Loads the map into the game by displaying its image and setting it as the current map.
        /// </summary>
        public void LoadMap()
        {
            // Display the map's image
            GameLoop.display.DrawMap(path);

            // Set the current map to this map
            CurrentMap = this;
        }

        /// <summary>
        /// Adds a new location to the map with the given properties.
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <param name="x">The x-coordinate of the location on the map</param>
        /// <param name="y">The y-coordinate of the location on the map</param>
        /// <param name="type">The type of the location (dialogue or combat)</param>
        /// <param name="path">The path to the file containing the location's content</param>
        public void AddLocation(int id, int x, int y, LocationType type, string path)
        {
            switch (type)
            {
                // If the location is a dialogue location, add a new Dialogue object to the locations list
                case LocationType.DIALOGUE:
                    locations.Add(new Dialogue(id, x, y, path));
                    break;

                // If the location is a combat location, add a new Combat object to the locations list
                case LocationType.COMBAT:
                    locations.Add(new Combat(id, x, y, path));
                    break;

                // If the location type is not recognized, do nothing
                default:
                    break;
            }
        }
   } 
}
