using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Spell : Player
    {
        private Player player;

        // The name of the spell
        string Name { get; }

        // The mana cost of the spell
        int ManaCost { get; }

        // The cooldown time of the spell
        float Cooldown { get; }

        // The current cooldown timer of the spell
        private double cooldownTimer { get; set; }

        public bool IsReady()
        {
            return cooldownTimer <= 0;
        }

        
    }




}


