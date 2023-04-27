using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : Mob
{
    public float speed;
    private Rigidbody2D RigidBody;
    public static PlayerHandler Instance;

    void Awake() {
        // Singleton
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }   
        Instance = this;

        // intialize default values
        health = 100;
        invulnerable = false;
        speed = 10.0f; 
        RigidBody = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // for(int i = 0; i < Gamepad.all.Count; i++){
        //     Debug.Log(Gamepad.all[i].name);
        // }
    }

    void Update() { 
        // Update velocity based on Horizontal
        RigidBody.velocity = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), 0), 1) * speed;

        // Bound Player to playfield width
        if (Mathf.Abs(RigidBody.position.x) > LevelHandler.PLAYFIELDWIDTH / 2) {
            RigidBody.position = Vector2.ClampMagnitude(new Vector2(RigidBody.position.x, 0), LevelHandler.PLAYFIELDWIDTH / 2);
        }

        // Attack with space button
        if (Input.GetKeyDown(KeyCode.Space)) {
            attack();
        }
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
        
    //     // if(Gamepad.all.Count > 0) {
    //     //     if(Gamepad.all[0].leftStick.left.isPressed) {
    //     //         RigidBody.transform.position += Vector3.left * Time.deltaTime * 20f;
    //     //     }
    //     //     if(Gamepad.all[0].leftStick.right.isPressed) {
    //     //         RigidBody.transform.position += Vector3.right * Time.deltaTime * 20f;
    //     //     }
    //     //     // if(Gamepad.all[0].leftStick.up.isPressed) {
    //     //     //     RigidBody.transform.position += Vector3.up * Time.deltaTime * 20f;
    //     //     // }
    //     //     // if(Gamepad.all[0].leftStick.down.isPressed) {
    //     //     //     RigidBody.transform.position += Vector3.down * Time.deltaTime * 20f;
    //     //     // }
    //     // }
    // }

    public override void attack()
    {
        // Spawn friendly projectile above player
        GameObject projectile = Instantiate(GameObject.Find("Test Projectile"), new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 5), Quaternion.identity);
        return;
    }

    public override void take_damage(int damage)
    {
        if (!invulnerable) {
            health -= damage;
        }
    }
}
