using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : Entity
{
    public float speed;
    private Rigidbody2D RigidBody;
    public static PlayerHandler Instance;

    public PlayerHandler() {
        health = 100;
        invulnerable = false;
        speed = 10.0f; 
    }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }   

        Instance = this;
    }

    void Start()
    {
        for(int i = 0; i < Gamepad.all.Count; i++){
            Debug.Log(Gamepad.all[i].name);
        }

        RigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RigidBody.velocity = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1) * speed;
        if(Gamepad.all.Count > 0) {
            if(Gamepad.all[0].leftStick.left.isPressed) {
                RigidBody.transform.position += Vector3.left * Time.deltaTime * 20f;
            }
            if(Gamepad.all[0].leftStick.right.isPressed) {
                RigidBody.transform.position += Vector3.right * Time.deltaTime * 20f;
            }
            if(Gamepad.all[0].leftStick.up.isPressed) {
                RigidBody.transform.position += Vector3.up * Time.deltaTime * 20f;
            }
            if(Gamepad.all[0].leftStick.down.isPressed) {
                RigidBody.transform.position += Vector3.down * Time.deltaTime * 20f;
            }
        }
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
