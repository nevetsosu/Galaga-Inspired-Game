using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawnZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {       
        if (col.gameObject.tag != "Projectile") return;

        DespawnHandler DH;
        if (col.gameObject.TryGetComponent<DespawnHandler>( out DH )) {
            DH.reportReceive();
        }
    }
}
