using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : Projectile
{

    void Awake() {
        // Set default values of Friendly Projectile
        damage = 5;
        velocity = 10;

        Rigidbody2D RigidBody = gameObject.GetComponent<Rigidbody2D>();
        RigidBody.velocity = new Vector2(0, velocity); 
    }

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
