using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class Projectile
public abstract class Projectile : Action
{

    [SerializeField] public int damage = 5;
    [SerializeField] public float speed = 10;
    [SerializeField] protected bool Piercing = false;
    public Vector3 direction = Vector3.up;

    protected MobMovementController MMC;
    protected MobLookController MLC;
    protected DespawnHandler DH;
    public GameObject Shooter;

    protected override void Awake() {
        base.Awake();
    }
    void Update() {
        MMC.MoveTo(PerformingObj.transform.position + Vector3.Normalize(direction) * speed * Time.deltaTime);
        MLC.lookToward(direction);
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (Shooter && col == Shooter) return; 

        if (col.TryGetComponent<HealthController>(out HealthController HC)) {
            HC.take_damage(damage); 
        }

        if (!Piercing) {
            Destroy(gameObject); 
        }
    }

    protected override bool preCheck()
    {
        bool valid = true;
        if (!base.preCheck()) valid = false;

        if (!PerformingObj.TryGetComponent<MobMovementController>(out MMC)) {
            MMC = PerformingObj.AddComponent<MobMovementController>(); 
        }

        if (!PerformingObj.TryGetComponent<MobLookController>(out MLC)) {
            MLC = PerformingObj.AddComponent<MobLookController>(); 
        }

        if (!PerformingObj.TryGetComponent<DespawnHandler>(out DH)) {
            DH = PerformingObj.AddComponent<DespawnHandler>(); 
        }

        return valid; 
    }
}
