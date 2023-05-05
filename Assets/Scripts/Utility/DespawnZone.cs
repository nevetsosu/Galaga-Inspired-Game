using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {       
        DespawnHandler DH;
        if (col.gameObject.TryGetComponent<DespawnHandler>( out DH )) {
            DH.reportReceive();
        }
    }
}
