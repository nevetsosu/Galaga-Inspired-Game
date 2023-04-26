using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract void take_damage(int damage);
    public abstract void attack();
    public int Health() {
        return health;
    }

    public void die() {
        Destroy(this.gameObject);
    }
    
    protected int health;
    protected bool invulnerable;
}
