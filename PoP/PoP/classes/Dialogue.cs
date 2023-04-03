using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    internal class Dialogue : Location
    {
        public Dialogue(int id, int x, int y) : base(id, x, y)
        {

        }

        public override void LoadLocation()
        {
            GameLoop.Phase = GamePhase.DIALOGUE;
            GameLoop.display.DrawString("loc", 10, 10);
        }
    }
}
