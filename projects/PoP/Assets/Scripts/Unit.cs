using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string Name;
    public float Health;
    public float Damage;

    public void Attack()
    {
        //attack
    }

    public void Defend()
    {
        //defend(heal, stb...)
    }

    public Unit(string name, float health, float damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }
}
