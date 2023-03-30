using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public Rigidbody2D enemy;

    public float speed = 10.0f;
    public bool switchDir = true;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject.GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(switchDir){
            enemy.velocity = new Vector2(1,0) * -1 * speed;
        } else if(!switchDir){
            enemy.velocity = new Vector2(1,0)*speed;
        }
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name == "Left Bound" || col.gameObject.name == "Right Bound"){
            switchDir = !switchDir; 
        }
    }
}
