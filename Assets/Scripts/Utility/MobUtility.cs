using UnityEngine;

public abstract class MobUtility : MonoBehaviour
{
    protected virtual void Awake() {
        this.enabled = false;
    }
}   