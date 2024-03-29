﻿using System;
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
        COMBAT,
        TRAVEL
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

        public bool IsHidden { get; set; }
        public string Name { get; set; }

        ///<summary>
        /// The path of the location.
        ///</summary>
        public string Path { get; set; }
        public Location(int id)
        {
            this.id = id;
            isCompleted = false;
        }

        ///<summary>
        /// Loads the location.
        ///</summary>
        public abstract void LoadLocation();

        ///<summary>
        /// Starts the location.
        ///</summary>
        public virtual void Start()
        {
            GameLoop.playerMovement.DisableMovement();
        }

        ///<summary>
        /// Ends the location.
        ///</summary>
        public virtual void End()
        {
            GameLoop.playerMovement.EnableMovement();
        }

    }
}
