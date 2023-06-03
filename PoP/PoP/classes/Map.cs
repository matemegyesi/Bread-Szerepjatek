using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PoP.classes
{

	class Map
	{
        public int ID { get; set; }
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
        private readonly List<Dictionary<string, object>> mapData = new List<Dictionary<string, object>>();

        /// <summary>
        /// A list of all the locations on the map
        /// </summary>
        public List<Location> locations = new List<Location>();

        /// <summary>
        /// Constructs a new map with the given path and adds it to the game's list of maps.
        /// </summary>
        /// <param name="path">The path to the image file of the map</param>
        /// <param name="id">The ID of the new map</param>
        public Map(string path, int id) 
        {
            this.path = path;
            ID = id;

            // Reads the map file
            mapData = FileInput.GetJsonDictList(path);
            for (int i = 1; i < mapData.Count; i++)
            {
                try
                {
                    switch (mapData[i]["type"].ToString())
                    {
                        case "dialogue":
                            AddLocation(i - 1, LocationType.DIALOGUE, mapData[i]["path"].ToString());
                            break;
                        case "combat":
                            AddLocation(i - 1, LocationType.COMBAT, mapData[i]["path"].ToString());
                            break;
                        case "travel":
                            AddLocation(i - 1, LocationType.TRAVEL, mapData[i]["path"].ToString(), int.Parse(mapData[i]["id"].ToString()));
                            break;
                    }
                }
                catch (Exception) { }
            }

            maps.Add(this);
        }

        /// <summary>
        /// Loads the map into the game by displaying its image and setting it as the current map.
        /// </summary>
        public void LoadMap()
        {
            // Display the map's image
            Wire.Map.UpdateLocationList(locations);
            Wire.Map.ImportMap(mapData[0]["path"].ToString());

            // Set the current map to this map
            CurrentMap = this;
        }

        /// <summary>
        /// Adds a new location to the map with the given properties.
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <param name="type">The type of the location (dialogue or combat)</param>
        /// <param name="path">The path to the file containing the location's content</param>
        public void AddLocation(int id, LocationType type, string path, int travelid = 0)
        {
            switch (type)
            {
                // If the location is a dialogue location, add a new Dialogue object to the locations list
                case LocationType.DIALOGUE:
                    Dialogue dialogueLoc = new Dialogue(travelid, path);
                    locations.Add(dialogueLoc);
                    break;

                // If the location is a combat location, add a new Combat object to the locations list
                case LocationType.COMBAT:
                    Combat combatLoc = new Combat(travelid, path);
                    locations.Add(combatLoc);
                    break;

                // If the location is a travel location, add a new Travel object to the locations list
                case LocationType.TRAVEL:
                    Travel travelLoc = new Travel(id, travelid, path);
                    locations.Add(travelLoc);
                    break;

                // If the location type is not recognized, do nothing
                default:
                    break;
            }
        }
   } 
}
