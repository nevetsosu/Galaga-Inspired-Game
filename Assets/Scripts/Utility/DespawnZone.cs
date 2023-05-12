using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnZone : MonoBehaviour
{
    
    // removes objects with despawn handlers upon entry
    void OnTriggerEnter2D(Collider2D col) {    
        DespawnHandler DH;
        if (col.gameObject.TryGetComponent<DespawnHandler>( out DH )) {
            DH.reportReceive();
        }
    }
}
