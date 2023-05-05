using UnityEngine;

public abstract class MobUtility : MonoBehaviour
{
    protected virtual void Awake() {
        this.enabled = false;
    }

    // this should return true or the component will not start
    protected virtual bool preCheck() { return true; }
    
}   