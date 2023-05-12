using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class Projectile
public abstract class Projectile : Action
{
    public Vector3 direction = Vector3.up; // default projectile direction is up
    public GameObject Shooter; // reference back to the object that instantiated the projectile

    [SerializeField] public int damage = 5;
    [SerializeField] public float speed = 10;
    [SerializeField] protected bool Piercing = false;

    protected MobMovementController MMC;
    protected MobLookController MLC;
    protected DespawnHandler DH;

    void Update() {
        MMC.MoveTo(PerformingObj.transform.position + Vector3.Normalize(direction) * speed * Time.deltaTime); // determine the next position to move to
        MLC.lookToward(direction); // future proofing for if a Projectile could turn while moving, but for now doesn't really affect the direction of the projectile
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (Shooter && col.gameObject.Equals(Shooter)) return; // make sure the projectile doesn't collide with the spawning object

        // damage colliding object if it is capable of taking damage
        if (col.TryGetComponent<HealthController>(out HealthController HC)) {
            HC.take_damage(damage); 
        }

        // if not peircing, disappear on impact.
        if (!Piercing) {
            Destroy(gameObject); 
        }
    }

    protected override bool preCheck()
    {
        bool valid = true;
        if (!base.preCheck()) valid = false;

        // make sure there are mobcontrollers
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
