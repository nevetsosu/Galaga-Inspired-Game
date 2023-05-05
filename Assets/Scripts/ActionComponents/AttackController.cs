using UnityEngine;
using System.Threading.Tasks;

public class AttackController : MobController {
    [SerializeField] GameObject laser;
    protected int damage = 5;
    protected int speed = 5;

    public void attack() {
        gameObject.transform.GetPositionAndRotation(out Vector3 current_pos, out Quaternion current_rot);
        Vector3 direction = Vector3.Normalize(current_rot * Vector3.up);
        Vector3 laser_pos = current_pos + direction * 10;

        GameObject new_laser = Instantiate(laser, laser_pos, current_rot);
        new_laser.SetActive(true);

        if (new_laser == null) return;

        if (!new_laser.TryGetComponent<Projectile>(out Projectile P)) {
            new_laser.AddComponent<Projectile>();
        } 

        P.speed = speed;
        P.damage = damage;
        P.direction = direction;

        // create new object and initialize
        P.Execute(); 

        return;
    }

    protected override bool preCheck() {
        bool valid = true;
        if (!base.preCheck()) valid = false;

        if (laser == null) {
            Debug.Log("laser null");
            valid = false;
        }

        return valid; 
    }

    

    // CONsider making a seperate script for attacks
    // for now attacks require the ennemy controller
}