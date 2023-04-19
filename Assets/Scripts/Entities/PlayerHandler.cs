using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    //private float horizontalInput;
    //public float force_magnetude = 500;
    //public float drag = 10;
    public float speed = 10.0f;
    public Rigidbody2D player;
    void Start()
    {
        for(int i = 0; i < Gamepad.all.Count; i++){
            Debug.Log(Gamepad.all[i].name);
        }

        player = this.GetComponent<Rigidbody2D>();
        //gameObject.GetComponent<Rigidbody2D>().drag = drag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        if(Gamepad.all.Count > 0) {
            if(Gamepad.all[0].leftStick.left.isPressed) {
                player.transform.position += Vector3.left * Time.deltaTime * 20f;
            }
            if(Gamepad.all[0].leftStick.right.isPressed) {
                player.transform.position += Vector3.right * Time.deltaTime * 20f;
            }
            if(Gamepad.all[0].leftStick.up.isPressed) {
                player.transform.position += Vector3.up * Time.deltaTime * 20f;
            }
            if(Gamepad.all[0].leftStick.down.isPressed) {
                player.transform.position += Vector3.down * Time.deltaTime * 20f;
            }
        }
        //horizontalInput = Input.GetAxis("Horizontal");
        // transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);  
        //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Time.deltaTime * force_magnetude * horizontalInput, ForceMode2D.Impulse);
    }
}
