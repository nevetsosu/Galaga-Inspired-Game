using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : Projectile
{

    public override void OnTriggerEnter2D(Collider2D col)
    {
        // Damage enemies then disappear
        if (col.gameObject.tag == "Enemy") {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.take_damage(damage);

            Destroy(gameObject);
        }
    }
}
