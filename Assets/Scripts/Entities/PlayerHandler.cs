using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : Entity
{
    public GameObject laserPrefab;
    public float speed;
    private Rigidbody2D RigidBody;
    public static PlayerHandler Instance;

    protected AttackController AC;
    protected MobMovementController MMC; 
    protected HealthController HC;

    void Awake() {
        // Singleton
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }   
        Instance = this;

        speed = 10.0f; 
        RigidBody = this.GetComponent<Rigidbody2D>();

        if (!gameObject.TryGetComponent<AttackController>(out AC)) {
            AC = gameObject.AddComponent<AttackController>();
            AC.laser = laserPrefab;
        }

        if (!gameObject.TryGetComponent<MobMovementController>(out MMC)) {
            MMC = gameObject.AddComponent<MobMovementController>();
        }

        if (!gameObject.TryGetComponent<HealthController>(out HC)) {
            HC = gameObject.AddComponent<HealthController>();
        }
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
            AC.shootProjectile();
        }
    }
}
