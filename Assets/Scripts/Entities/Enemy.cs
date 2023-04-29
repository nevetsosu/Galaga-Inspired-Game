using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Enemy Class
public class Enemy : Mob
{  
    protected int collision_damage;
    // Does nothing by default
    public override void attack()
    {
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

        Vector3 proj = Vector3.Project(current_pos, obj_pos);
        Vector3 ideal = -(current_pos - proj);

        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.LookRotation(Vector3.forward, ideal), incrementAngle);
    }

    public void rotateTowardPlayer(float incrementAngle) {
        rotateToward(PlayerHandler.Instance.gameObject, incrementAngle);
    }
}
