using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cat : MonoBehaviour
{

    //bármit felvehet akár több ugyanolyan slotot is de csak x itemet összesen
    List<Item> CatGear = new List<Item>(10);

    List<Spell> spells= new List<Spell>();

    void Interact()
    {
        Debug.Log("Simogatás");
    }

    void UpgradeSpell(string spellName)
    {
        //spells.Where(x => x.Name == spellName).ToList().First().
    }
}
