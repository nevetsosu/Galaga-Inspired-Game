using UnityEngine;

public abstract class MobController : MonoBehaviour
{
    void Awake() {
        if (gameObject.GetComponent<Mob>() == null) {
            Debug.Log("Missing Mob Component, Removing Disabling Controller");
            this.enabled = false; 
        }
    }

    protected abstract void PreCheck();
}   