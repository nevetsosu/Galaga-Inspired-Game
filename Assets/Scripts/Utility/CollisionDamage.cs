using UnityEngine;

public class CollisionDamage : MonoBehaviour 
{
    [SerializeField] protected int collision_damage = 5;
    [SerializeField] protected string DamageeTag = "Player";
    
    // this one is quite similar to the projectile script and may be altered to work along side the projectile script to make a projectile to reduce the AMOUNT of code, but would increase components.
   public void OnTriggerEnter2D (Collider2D col)
    {   
        // Damage enemies then disappear
        if (col.gameObject.CompareTag(DamageeTag)) {
            if (col.gameObject.TryGetComponent<HealthController>(out HealthController HC)) {
                HC.take_damage(collision_damage); 
            }
        }
    }
}