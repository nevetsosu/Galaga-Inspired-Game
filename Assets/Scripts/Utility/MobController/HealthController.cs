using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Enemy Class
public class HealthController : MobController
{  
    [SerializeField] protected string deathSound = "explosionDeath";
    [SerializeField] protected int health = 50; 
    [SerializeField] public bool invulnerable = false;
    [SerializeField] private bool endGame = false;
    protected DespawnHandler DH;

    public int Health
    {
        get { return health; }
    }

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
        if (health < 1) {
            DH.reportReceive(); 
            // this.enabled = false;
            AudioManager.Instance.Play(deathSound);
            if (endGame) LevelHandler.Instance.endGame();
        }
    }
    protected override void Awake() {
        if (!gameObject.TryGetComponent<DespawnHandler>(out DH)) {
            DH = gameObject.AddComponent<DespawnHandler>();
        }
    }
}
