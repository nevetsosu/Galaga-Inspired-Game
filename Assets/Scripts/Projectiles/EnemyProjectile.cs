using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    public override void OnTriggerEnter2D(Collider2D col)
    {
        // only damage player
        if (col.gameObject.CompareTag("Player")) {
            if (col.TryGetComponent<HealthController>(out HealthController HC)) {
                HC.take_damage(damage); 
            }
        }
        if (!Piercing) {
            Destroy(gameObject); 
        }
        return;
    }
}
