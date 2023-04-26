using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy() {
        health = 50;
    }

    public override void attack()
    {
        return;
    }

    public override void take_damage(int damage) {
        if (!invulnerable) {
            health -= damage;
        }
        Debug.Log("Damage Taken, current health " + health);
        return;
    }
}
