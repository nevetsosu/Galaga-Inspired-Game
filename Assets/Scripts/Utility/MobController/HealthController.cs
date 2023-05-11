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
    [SerializeField] protected string damage_noise = "minecraftHit";
    protected DespawnHandler DH;

    public int Health
    {
        get { return health; }
    }

    // By default inflicts specified damage if target is NOT invulnerabe;
    public void take_damage(int damage) {
        if (!invulnerable) {
            health -= Mathf.Abs(damage);
        }

        AudioManager.Instance.Play(damage_noise);
        return;
    }

    public void restoreHealth(int health) {
        this.health += Mathf.Abs(health);
    }
    void Update() {
        if (health < 1) {
            DH.reportReceive(); 
            // this.enabled = false;
            AudioManager.Instance.Play(deathSound);
            if (endGame) LevelHandler.Instance.GameOver = true; 
        }
    }
    protected override void Awake() {
        if (!gameObject.TryGetComponent<DespawnHandler>(out DH)) {
            DH = gameObject.AddComponent<DespawnHandler>();
        }
    }
}
