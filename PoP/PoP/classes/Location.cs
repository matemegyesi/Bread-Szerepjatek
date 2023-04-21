using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    ///<summary>
    /// The type of location, either dialogue or combat.
    ///</summary>
    enum LocationType
    {
        DIALOGUE,
        COMBAT
    }
    internal abstract class Location
    {
        ///<summary>
        /// The location ID.
        ///</summary>
        public int id;

        ///<summary>
        /// The x-coordinate of the location on the game map.
        ///</summary>
        public int positionX { get; set; }

        ///<summary>
        /// The y-coordinate of the location on the game map.
        ///</summary>
        public int positionY { get; set; }

        ///<summary>
        /// A flag indicating whether the location has been completed or not.
        ///</summary>
        public bool isCompleted { get; set; }

        ///<summary>
        /// The path of the location.
        ///</summary>
        public string Path { get; set; }

        ///<summary>
        /// The name of the location.
        ///</summary>
        public string Name { get; set; }
        public Location(int id, int x, int y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            isCompleted = false;
        }

        ///<summary>
        /// Loads the location.
        ///</summary>
        public abstract void LoadLocation();

        ///<summary>
        /// Starts the location.
        ///</summary>
        public abstract void Start();

        ///<summary>
        /// Ends the location.
        ///</summary>
        public abstract void End();

    }
}
