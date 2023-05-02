using UnityEngine;

public class MobController : MonoBehaviour
{
    void Awake() {
        if (gameObject.GetComponent<Mob>() == null) {
            Debug.Log("Missing Mob Component, Removing Disabling Controller");
            this.enabled = false; 
        }
    }

    abstract void PreCheck();
}