using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTrackObject : MonoBehaviour
{
    [SerializeField] private int incrementAngle;
    [SerializeField] private bool isTracking;
    [SerializeField] private GameObject target;

    public GameObject Target 
    {
        get { return target; }
    }

    public int IncrementAngle
    {
        get { return incrementAngle; }
        set { incrementAngle = value; }
    }

    public bool IsTracking
    {
        get { return isTracking; }
    }
    
    // default incrementAngle of 1
    void Awake() {
        incrementAngle = 1;
        isTracking = false;
    }

    void Start() {
        target = PlayerHandler.Instance.gameObject;
    }

    void Update() {
        if (isTracking) {
            if (target != null) rotateToward(target);
            else Destroy(this);                                // remove tracking if target is null
        }
    }
    
   private void rotateToward(GameObject target) {
        // get the direction vector
        Vector3 ideal = target.transform.position - gameObject.transform.position;

        // get the associated quaternion rotation and assign as current rotation
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, ideal);

        // Increment current rotation toward ideal rotation
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rot, incrementAngle);
   }

   public void Resume() {
        isTracking = true;
   }

   public void Pause() {
        isTracking = false;
   }

   public void setTarget(GameObject target) {
        this.target = target;
   }
}

