using UnityEngine;

public abstract class MobController : MobUtility
{
    protected override void Awake() {
        base.Awake(); 

        // Check for a Mob Component
        if (gameObject.GetComponent<Mob>() == null) 
        {
            Debug.Log("Mob Component not found! Self-Removing Controller.");
            Destroy(this);      
        }

        Debug.Log("MobController Awake");
    }
    protected bool inUse = false;
}   