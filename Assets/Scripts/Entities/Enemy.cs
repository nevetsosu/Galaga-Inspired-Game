using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Enemy Class
public class Enemy : Mob
{
    // Does nothing by default
    public override void attack()
    {
        return;
    }

    // By default inflicts specified damage if target is NOT invulnerabe;
    public override void take_damage(int damage) {
        if (!invulnerable) {
            health -= damage;
            Debug.Log("Damage Taken, current health " + health);
        }
        return;
    }
}
