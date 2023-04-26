using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Enemy : Enemy
{
    private Vector2 defaultVelocity;
    public float speed;
    private Rigidbody2D RigidBody;
    private BoxCollider2D BoxCollider;
    private float BoundCollideSpeedGain;
    private float defaultXVelocity = -10;
    private float defaultYVelocity = -2;
    public Type1Enemy Instance;
    

    public Type1Enemy() {
        health = 50;
        invulnerable = false;
        speed = 10.0f;
        defaultVelocity = Vector2.ClampMagnitude(new Vector2(defaultXVelocity, defaultYVelocity), 1) * speed;
        BoundCollideSpeedGain = 1.05f;
    }

    // Start is called before the first frame update
    void Start()
    {
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
        BoxCollider = gameObject.GetComponent<BoxCollider2D>();
        RigidBody.velocity = defaultVelocity;
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "LeftBound" || col.gameObject.tag == "RightBound"){
                reverseHorizontal();
            }
    }

    private void reverseHorizontal() {
        RigidBody.velocity = new Vector2(-BoundCollideSpeedGain * RigidBody.velocity.x, RigidBody.velocity.y);
    }

    // public override void attack()
    // {
    //     return;
    // }

    // public override void take_damage(int damage)
    // {
    //     if (!invulnerable) {
    //         health -= damage;
    //     }
    // }
}
