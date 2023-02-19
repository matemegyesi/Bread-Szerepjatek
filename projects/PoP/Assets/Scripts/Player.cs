using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    //Csak 1 játékos van, singleton
    public static Player Instance { get; private set; }

    //nem a harc hp-ja hanem a játék általános hp-ja
    int Health = 2;

    public float PlayerCombatHealth = 100f;

    public float PlayerDamage;

    //a felszerelés dict-je, a key egy Item.Slot a value maga az Item
    Dictionary<Item.Slot, Item> Gear;

    //a statisztika fict-je, minden key egy string (pl.: "Kitartás", "Erõ", "Intelligencia") a value pedig egy double érték
    Dictionary<string, float> Stats;

    //inventory
    List<Item> Inventory;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    void Update()
    {
        PlayerDamage = 10f;
        //PlayerDamage = Gear.Sum(x => x.Value.Damage);
    }
    /// <summary>
    /// Felveszi az itemet, csak az inventoryba rakja, szöveges opció lesz
    /// </summary>
    /// <param name="item"></param>
    void PickUpGear(Item item)
    {
        if (!Inventory.Contains(item))
            Inventory.Add(item);
        else
            Debug.Log("Már van ilyen az inventoryban.");
    }
    /// <summary>
    /// az adott itemet a megfelelõ item.slot helyre rakja be
    /// </summary>
    /// <param name="item"></param>
    void EquipGear(Item item)
    {
        if (!Gear.ContainsKey(item.slot))
            //ha még nincs pl.: Head helyen item hozzáadjuk a kulcsot is
            Gear.Add(item.slot, item);
        else
            Gear[item.slot] = item;
    }
    /// <summary>
    /// Egy statName statisztikát változtat a value értékkel
    /// </summary>
    /// <param name="statName"></param>
    /// <param name="value"></param>
    void UpdateStat(string statName, float value)
    {
        Stats[statName] += value;    
    }
}
