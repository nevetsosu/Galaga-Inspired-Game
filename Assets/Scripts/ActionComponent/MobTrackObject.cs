using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MobTrackObject : Action
{
    [SerializeField] public int incrementAngle = 1;
    [SerializeField] public GameObject target;
    private MobLookController MLC;

    public bool found = false;
    
    // default incrementAngle of 1
    protected override void Awake() {
        base.Awake();

        if (!PerformingObj.TryGetComponent<MobLookController>(out MLC)) {
            MLC = PerformingObj.AddComponent<MobLookController>();
        }
    }

    void Update() {
        if (target && !onTarget()) {
            found = false;
            MLC.incrementToward(target, incrementAngle);
            Debug.Log("Turning");
        }         
    }

    protected override void execute() {
        Debug.Log("TOGGLE");
        this.enabled = !this.enabled;
    }

    public bool onTarget() {
        if (PerformingObj.transform.rotation == Quaternion.LookRotation(Vector3.forward, target.transform.position - PerformingObj.transform.position)) {
            found = true;
            return true;
        }

        return false; 
    }
}

