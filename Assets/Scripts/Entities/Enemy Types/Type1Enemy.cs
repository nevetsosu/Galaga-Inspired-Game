using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Enemy : Enemy
{
    private Vector2 defaultVelocity;
    public float speed;
    private Rigidbody2D RigidBody;
    private BoxCollider2D BoxCollider;
    

    public Type1Enemy() {
        health = 50;
        invulnerable = false;
        speed = 10.0f;
        defaultVelocity = Vector2.ClampMagnitude(new Vector2(-10, -2), 1) * speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
        BoxCollider = gameObject.GetComponent<BoxCollider2D>();
        RigidBody.velocity = defaultVelocity;
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("TRIGGER");

        if(col.gameObject.tag == "LeftBound" || col.gameObject.tag == "RightBound"){
            reverseHorizontal();
        }
    }

    private void reverseHorizontal() {
        // Debug.Log("Reverse");
        RigidBody.velocity = new Vector2(-1.05f * RigidBody.velocity.x, RigidBody.velocity.y);
    }

    public override void attack()
    {
        return;
    }

    public override void take_damage(int damage)
    {
        if (!invulnerable) {
            health -= damage;
        }
    }
}
