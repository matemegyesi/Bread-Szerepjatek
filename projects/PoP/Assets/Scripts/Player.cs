using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Csak 1 j�t�kos van, singleton
    public static Player Instance { get; private set; }

    //nem a harc hp-ja hanem a j�t�k �ltal�nos hp-ja
    int Health = 2;

    //harc beli mana
    float Mana = 500;
    //Mana visszat�lt�si r�ta, k�r�nk�nt
    float ManaRegenerationRate = 50f;

    //a felszerel�s dict-je, a key egy Item.Slot a value maga az Item
    Dictionary<Item.Slot, Item> Gear = new Dictionary<Item.Slot, Item>();

    //a statisztika fict-je, minden key egy string (pl.: "Kitart�s", "Er�", "Intelligencia") a value pedig egy double �rt�k
    Dictionary<string, float> Stats;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void EquipGear(Item item)
    {
        if (!Gear.ContainsKey(item.slot))
            //ha m�g nincs pl.: Head helyen item hozz�adjuk a kulcsot is
            Gear.Add(item.slot, item);
        else
            Gear[item.slot] = item;
    }
    void UpdateStat(string statName, float value)
    {
        Stats[statName] += value;    
    }
}
