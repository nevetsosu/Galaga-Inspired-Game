using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    void Awake() {
        // Set default values of Friendly Projectile
        damage = 5;
        velocity = 10;
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        
        // Only damage the player and disappear
        if (col.gameObject.tag == "Player") {
            PlayerHandler.Instance.take_damage(damage);
        }
        Destroy(gameObject);
        return;
    }
}
