using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Windows.Markup;

namespace PoP.classes
{
    class Travel : Location
    {
        public int TravelingID { get; set; }
        public Travel(int id, int travelingId, string path) :base(id)
        {
            this.id = id;
            TravelingID = travelingId;
            Path = path;

            string json = File.ReadAllText(path);
            List<Dictionary<string, object>> traveling = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            positionX = int.Parse(traveling[0]["posX"].ToString());
            positionY = int.Parse(traveling[0]["posY"].ToString());

            bool isHidden;
            if (bool.TryParse(traveling[0]["isHidden"].ToString(), out isHidden))
            {
                IsHidden = isHidden;
            }
            else
            {
                IsHidden = true;
            }

            if (!IsHidden)
            {
                Name = traveling[0]["locationName"].ToString();
            }
        }
        public override void LoadLocation()
        {
            Map.maps.Where(x => x.ID == TravelingID).First().LoadMap();
        }
    }
}
