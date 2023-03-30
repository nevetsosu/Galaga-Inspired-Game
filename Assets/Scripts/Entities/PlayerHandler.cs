using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    //private float horizontalInput;
    //public float force_magnetude = 500;
    //public float drag = 10;
    public float speed = 10.0f;
    public Rigidbody2D player;
    void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
        //gameObject.GetComponent<Rigidbody2D>().drag = drag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        //horizontalInput = Input.GetAxis("Horizontal");
        // transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);  
        //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Time.deltaTime * force_magnetude * horizontalInput, ForceMode2D.Impulse);
    }
}
