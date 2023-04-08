using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    

namespace PoP.classes
{
    class Combat : Location
    {
        Enemy enemy;

        public Combat(int id, int x, int y, string enemyFile) : base(id, x, y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            
        }

        public override void IncreaseDialogueIndex()
        {
            //not dialogue
        }

        public override void LoadLocation()
        {
            Map.CurrentLocation = this;
            GameLoop.Phase = GamePhase.COMBAT;
            GameLoop.display.DrawString("loc", 10, 10);
        }

        public override void Start()
        {
            
        }
    }
}
