using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name;
    public float Health;
    public float Damage;

    public Enemy(string name, float health, float damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }
}
