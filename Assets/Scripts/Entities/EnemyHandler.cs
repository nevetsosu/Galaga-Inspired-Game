using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public Rigidbody2D enemy;

    public int health = 5;
    public float speed = 10.0f;
    
    Vector2 velocitybeforeCol = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In EnemyHandler #2/3");
        enemy = this.gameObject.GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        if(enemy.velocity != Vector2.zero){
            velocitybeforeCol = enemy.velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name == "Left Bound" || col.gameObject.name == "Right Bound"){
            Vector2 vel = velocitybeforeCol;
            vel.x = -vel.x;
            enemy.velocity = vel;
            Debug.Log("hit" + velocitybeforeCol + " " + enemy.velocity);
        }
    }
}
