using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class Projectile
public abstract class Projectile : Entity
{
    public abstract void OnTriggerEnter2D(Collider2D col);
    protected static int damage;
    protected static float velocity;

    public float Velocity() {
        return velocity;
    }
}
