using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface IEquippable
    {
        bool Equip(Inventory.Slot slot);
    }
}
