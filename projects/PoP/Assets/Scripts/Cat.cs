using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cat : MonoBehaviour
{

    //b�rmit felvehet ak�r t�bb ugyanolyan slotot is de csak x itemet �sszesen
    List<Item> CatGear = new List<Item>(10);

    List<Spell> spells= new List<Spell>();

    void Interact()
    {
        Debug.Log("Simogat�s");
    }

    void UpgradeSpell(string spellName)
    {
        //spells.Where(x => x.Name == spellName).ToList().First().
    }
}
