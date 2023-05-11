using UnityEngine;

public class CollisionDamage : MonoBehaviour 
{
    [SerializeField] protected int collision_damage = 5;
    [SerializeField] protected string DamageeTag = "Player";
    
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