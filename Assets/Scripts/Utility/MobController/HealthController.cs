using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Enemy Class
public class HealthController : MobController
{  
    [SerializeField] protected int health = 100; 
    [SerializeField] public bool invulnerable = false;
    protected DespawnHandler DH;

    // By default inflicts specified damage if target is NOT invulnerabe;
    public void take_damage(int damage) {
        if (!invulnerable) {
            health -= Mathf.Abs(damage);
            Debug.Log("Damage Taken, current health " + health);
        }
        return;
    }

    public void restoreHealth(int health) {
        this.health += Mathf.Abs(health);
        Debug.Log("health ADDed, current health " + health);
    }
    void Update() {
        if (health < 1) DH.reportReceive(); 
    }
    protected override void Awake() {
        if (!gameObject.TryGetComponent<DespawnHandler>(out DH)) {
            DH = gameObject.AddComponent<DespawnHandler>();
        }
    }
}
