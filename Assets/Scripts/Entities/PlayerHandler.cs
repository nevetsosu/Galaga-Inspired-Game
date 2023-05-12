using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class PlayerHandler : Entity
{
    public static PlayerHandler Instance;

    [SerializeField] protected GameObject laserPrefab;
    [SerializeField] protected float speed;
    [SerializeField] protected int coolDownDelay = 100; // delay between attack triggers
    private Rigidbody2D RigidBody;

    protected AttackController AC;
    protected MobMovementController MMC; 
    protected HealthController HC;
    protected LimitedAttackHandler LAH;

    void Awake() {
        // only one player handler, though this could be potentially be changed to support local 2 player coop // with 2 controllers or on the same keyboard
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }   
        Instance = this;

        // the player is the only one that really uses the physics system to have smooth gradual acceleration on the player
        // rigidbody is the element that is in the physics simulation
        RigidBody = this.GetComponent<Rigidbody2D>();

        // make sure the player has the proper controllers
        if (!gameObject.TryGetComponent<AttackController>(out AC)) {
            AC = gameObject.AddComponent<AttackController>();
            AC.laser = laserPrefab;
        }

        if (!gameObject.TryGetComponent<LimitedAttackHandler>(out LAH)) {
           LAH = gameObject.AddComponent<LimitedAttackHandler>();
        }
        LAH.delayCoolDown = coolDownDelay;

        if (!gameObject.TryGetComponent<MobMovementController>(out MMC)) {
            MMC = gameObject.AddComponent<MobMovementController>();
        }

        if (!gameObject.TryGetComponent<HealthController>(out HC)) {
            HC = gameObject.AddComponent<HealthController>();
        }
    }

    void Update() { 
        if (!LevelHandler.Instance.GameOver && !GameManager.Instance.isPaused()) { 
            // Update velocity based on Horizontal
            RigidBody.velocity = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), 0), 1) * speed;

            // Bound Player to playfield width
            if (Mathf.Abs(RigidBody.position.x) > LevelHandler.PLAYFIELDWIDTH / 2) {
                RigidBody.position = Vector2.ClampMagnitude(new Vector2(RigidBody.position.x, 0), LevelHandler.PLAYFIELDWIDTH / 2);
            }

            // Attack with space button
            if (Input.GetButtonDown("Fire1")) {
                LAH.TryBurst(); 
            }
        }
    }


}
