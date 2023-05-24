using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Travel : Location
    {
        public int TravelingID { get; set; }
        public Travel(int id, int travelingId) :base(id)
        {
            this.id = id;
            TravelingID = travelingId;
            IsHidden = true;
        }
        public override void LoadLocation()
        {
            Map.maps.Where(x => x.ID == TravelingID).First().LoadMap();
        }
    }
}
