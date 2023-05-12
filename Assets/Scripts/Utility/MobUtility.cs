using UnityEngine;

public abstract class MobUtility : MonoBehaviour
{
    // most mobutilities need to be off by default
    protected virtual void Awake() {
        this.enabled = false;
    }
}   