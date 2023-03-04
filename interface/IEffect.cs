using System;
namespace interfacek
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

