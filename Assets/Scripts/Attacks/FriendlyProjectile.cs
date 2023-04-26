using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : Projectile
{
    // TEST AWAKE

    void Awake() {
        damage = 5;
        // Rigidbody2D RigidBody = gameObject.GetComponent<Rigidbody2D>();

        // RigidBody.velocity = new Vector2(0, 1.5f); 
    }
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy") {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.take_damage(damage);

            Destroy(gameObject);
        }
    }
}
