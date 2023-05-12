using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Enemy Class
public class HealthController : MobController
{  
    [SerializeField] public bool invulnerable = false;
    [SerializeField] protected string deathSound = "explosionDeath"; // default sound is enemy death
    [SerializeField] protected int health = 50; // default Health is enemy health
    [SerializeField] private bool endGame = false; // whether this death should end the game (for player)
    [SerializeField] protected string damage_noise = "minecraftHit"; // sound to be played upon getting hit
    protected DespawnHandler DH;

    public int Health
    {
        get { return health; }
    }

    // By default inflicts specified damage if target is NOT invulnerabe;
    public void take_damage(int damage) {
        Debug.Log("take_damage");
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
            Debug.Log("deat");  
            // inform the despawn handler
            DH.reportReceive();

            // death sound
            AudioManager.Instance.Play(deathSound);

            if (endGame) LevelHandler.Instance.GameOver = true; 
        }
    }
    protected override void Awake() {
        // Dont disable on start 
        
        // make sure there is a despawn Handler
        if (!gameObject.TryGetComponent<DespawnHandler>(out DH)) {
            DH = gameObject.AddComponent<DespawnHandler>();
        }
    }
}
