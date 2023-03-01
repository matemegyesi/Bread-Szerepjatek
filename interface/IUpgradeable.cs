using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    interface IUpgradeable
    {

        bool CanUpgrade { get; }
        int UpgradeCost { get; }

        void Upgrade();
        
    }
}
