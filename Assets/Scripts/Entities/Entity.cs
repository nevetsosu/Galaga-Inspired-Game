using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // getter for health
    public int Health() {
        return health;
    }

    public virtual void die() {     
        Destroy(this.gameObject);
    }
    
    protected int health;
    protected bool invulnerable;
}
