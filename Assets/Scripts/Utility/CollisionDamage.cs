using UnityEngine;

public class CollisionDamage : MonoBehaviour 
{
    [SerializeField] protected int collision_damage = 5;
    [SerializeField] protected string DamageeTag = "Player";
    
   void OnTriggerEnter2D(Collider2D col)
    {
        // Damage enemies then disappear
        if (col.gameObject.tag == DamageeTag) {
            if (col.gameObject.TryGetComponent<HealthController>(out HealthController HC)) {
                HC.take_damage(collision_damage);
            }
        }
    }
}