using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void die() {     
        Destroy(this.gameObject);
    }
    
    protected int health;
    protected bool invulnerable;
}
