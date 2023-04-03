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

        public void StartCombat()
        {
            
        }

        public override void LoadLocation()
        {
            GameLoop.Phase = GamePhase.COMBAT;
            GameLoop.display.DrawString("loc", 10, 10);
        }
    }
}
