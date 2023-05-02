using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Enemy Class
public class Enemy : Mob
{  
    protected int collision_damage;
    [SerializeField] protected GameObject laserPrefab;
    // Does nothing by default
    public override void attack()
    {
        gameObject.transform.GetPositionAndRotation(out Vector3 current_pos, out Quaternion current_rot);

        // Get normalized direction vector with Quarternion x Vector math
        Vector3 dir = Vector3.Normalize(current_rot * Vector3.up);
        Vector3 velocity = dir;

        // Scale how far the laser should be from the player
        dir *= 10;

        // Scale speed
        velocity *= laserPrefab.GetComponent<Projectile>().Velocity();

        // Create initial position vector for new laser

        Vector3 laser_pos = current_pos + dir;

        // create new object and initialize
        GameObject new_laser = Instantiate(laserPrefab, laser_pos, current_rot);
        new_laser.SetActive(true);
        new_laser.GetComponent<Rigidbody2D>().velocity = velocity;

        return;
    }

    // By default inflicts specified damage if target is NOT invulnerabe;
    public override void take_damage(int damage) {
        if (!invulnerable) {
            health -= damage;
            Debug.Log("Damage Taken, current health " + health);
        }
        return;
    }

    private void rotateToward(GameObject obj, float incrementAngle) {
        Vector3 obj_pos = obj.transform.position;
        Vector3 current_pos = gameObject.transform.position;

        // get the direction vector
        Vector3 ideal = obj_pos - current_pos;

        // get the associated quaternion rotation and assign as current rotation
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, ideal);

        // Increment current rotation toward ideal rotation
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rot, incrementAngle);
    }

    public void rotateTowardPlayer(float incrementAngle) {
        rotateToward(PlayerHandler.Instance.gameObject, incrementAngle);
    }
}
