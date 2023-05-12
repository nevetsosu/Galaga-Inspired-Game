using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawnZone : MonoBehaviour
{
    // despawn zone for only projectiles
    void OnTriggerEnter2D(Collider2D col) {       
        if (!col.gameObject.CompareTag("Projectile")) return;

        // send despawn report if despawn handler found on object
        if (col.gameObject.TryGetComponent<DespawnHandler>( out DespawnHandler DH )) {
            DH.reportReceive();
        }
    }
}
