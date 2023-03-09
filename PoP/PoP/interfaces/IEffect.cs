using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP
{
    public interface IEffect
    {
        //---Bántó---
        void Bleed(); //Vérzés
        void Burn(); //Égés
        void Blindness(); //Vakság
        void Poison(); //Ázás ("savas eső")
        void Weakness(); //Gyengeség
        void Slowness(); //Lassúság
        void Stun(); //Elkábítani

        //---Védekező---
        void Bandage(); //Kötszer
        void HealthRegeneration(); //Élet regeneráció
        void ManaRegeneration(); //Mana regeneráció
    }
}

