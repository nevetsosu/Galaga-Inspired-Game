using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class Projectile
public abstract class Projectile : Action
{
    [SerializeField] public int Piercing = 0;
    [SerializeField] public int damage = 5;
    [SerializeField] public float speed = 10;
    public Vector3 direction = Vector3.up;

    protected MobMovementController MMC;
    protected MobLookController MLC;

    void Update() {
        MMC.MoveTo(PerformingObj.transform.position + Vector3.Normalize(direction) * speed);
        MLC.lookToward(direction);
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.TryGetComponent<Mob>(out Mob M)) {
            M.take_damage(damage); 
        }

        if (Piercing-- <= 0) {
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

        return valid; 
    }
}
