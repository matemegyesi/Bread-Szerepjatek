using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    abstract class Item
    {
        protected string name { get; private set; }

        protected Inventory.Slot slot { get; private set; }


        //minden itemnek van armor és damage statja de lehet az armor vagy a damage 0

        protected float damage { get; private set; }

        protected float armor { get; private set; }

    }
}
