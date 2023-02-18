using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Item
{
    // az item neve
    public string Name;

    //milyen slotokra tehet�ek be itemek
    public enum Slot
    {
        Head,
        Chest,
        Leg,
        Hand,
        Ring
    }

    //Mennyi es�ly van arra, hogyy boss fight ut�n lootolni lehessen
    private float rarity;

    //saj�t gear slot
    public Slot slot;

    //Sebz�s �s P�nc�l statok --lehetnek t�bb egy�b stat de ezek az alapok
    private float Damage;
    private float Armor;

    public Item(string name, Slot slot, float rarity, float damage, float armor)
    {
        Name = name;
        this.slot = slot;
        this.rarity = rarity;
        Damage = damage;
        Armor = armor;
    }
}
