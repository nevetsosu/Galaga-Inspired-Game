using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Despawn");
        
        col.gameObject.GetComponent<Entity>().die();
    }
}
