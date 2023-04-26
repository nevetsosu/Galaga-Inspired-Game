using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Enemy : Enemy
{
    private Vector2 velocity;
    public float speed;
    private Rigidbody2D RigidBody;
    private float BoundCollideSpeedGain;
    private float defaultXVelocity = -10;
    private float defaultYVelocity = -2;
    public Type1Enemy Instance;
    

    void Awake() {
        // initilize default values
        health = 50;
        invulnerable = false;
        speed = 10.0f;
        velocity = Vector2.ClampMagnitude(new Vector2(defaultXVelocity, defaultYVelocity), 1) * speed;
        BoundCollideSpeedGain = 1.05f;
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        RigidBody.velocity = velocity;
    }

    void Update() {
        // check aliveness
        if (health < 0) die(); 
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        // reverse direction when edge reached // This can be re coded to work on the Level manager's PLAYFIELDWIDTH variable instead
        if (col.gameObject.tag == "LeftBound" || col.gameObject.tag == "RightBound"){
                reverseHorizontal();
        }
    }

    private void reverseHorizontal() {
        RigidBody.velocity = new Vector2(-BoundCollideSpeedGain * RigidBody.velocity.x, RigidBody.velocity.y);
    }

    public override void attack()
    {
        return; 
    }
}
