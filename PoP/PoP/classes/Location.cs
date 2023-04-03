﻿using System;
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

        public readonly int Id;
        public int positionX { get; private set; }
        public int positionY { get; private set; }

        public Location(int id, int x, int y)
        {
            Id = id;
            positionX = x;
            positionY = y;
        }

        public static void LoadLocation()
        {
            GameLoop.Phase = GamePhase.DIALOGUE;
            GameLoop.display.DrawString("loc", 10, 10);
        }

    }
}
