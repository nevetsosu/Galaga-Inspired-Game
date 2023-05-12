using UnityEngine;
using System.Threading.Tasks;

public class AttackController : MobController {
    [SerializeField] public GameObject laser; // laser prefab assigned in edtior

    public void shootProjectile() {
        // get current pos and rot
        gameObject.transform.GetPositionAndRotation(out Vector3 current_pos, out Quaternion current_rot);

        // get direction as vector (Quaternion * vector3 = vector3)
        Vector3 direction = Vector3.Normalize(current_rot * Vector3.up);

        // laser pos is slightly further out from player location
        Vector3 laser_pos = current_pos + direction * 5;

        // create laser
        GameObject new_laser = Instantiate(laser, laser_pos, current_rot);
        
        // make sure its on
        new_laser.SetActive(true);

        // error check for if the prefab is not a projectile 
        if (!new_laser.TryGetComponent<Projectile>(out Projectile P)) {
            new_laser.AddComponent<Projectile>();
        } 

        // init settings
        P.Shooter = gameObject;
        P.direction = direction;

        // create new object and initialize
        P.Execute(); 
        
        // sound
        AudioManager.Instance.Play("shoot");

        return;
    }
}