using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D col);
    protected int damage;
    protected float velocity;
}
