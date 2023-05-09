using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : Projectile
{

    public override void OnTriggerEnter2D(Collider2D col)
    {
        // Damage enemies then disappear
        if (col.gameObject.tag == "Enemy") {
            if (col.gameObject.TryGetComponent<HealthController>(out HealthController enemy)) {
                enemy.take_damage(damage);
            }

            if (!Piercing) {
                Destroy(gameObject); 
            }
        }
    }
}
