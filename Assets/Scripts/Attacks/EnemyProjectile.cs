using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
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
