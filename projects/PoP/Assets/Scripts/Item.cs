using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Item
{
    // az item neve
    public string Name;

    //milyen slotokra tehetõek be itemek
    public enum Slot
    {
        Head,
        Chest,
        Leg,
        Hand,
        Ring
    }

    //Mennyi esély van arra, hogyy boss fight után lootolni lehessen
    private float rarity;

    //saját gear slot
    public Slot slot;

    //Sebzés és Páncél statok --lehetnek több egyéb stat de ezek az alapok
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
