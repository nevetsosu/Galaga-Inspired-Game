using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class ActionTopggleTrackObject : Action
{
    [SerializeField] private GameObject TargetObj; 
    [SerializeField] private int incrementAngle;

    protected override void execute() {
        if (preCheck()) {
            MobTrackObject MTO;
            MTO = PerformingObj.AddComponent<MobTrackObject>();  
            MTO.setTarget(TargetObj);
            MTO.IncrementAngle = incrementAngle; 
            MTO.Resume();
        } else {
            Debug.Log("Precheck fail");
        }
    }

    private bool preCheck() {
        if (PerformingObj == null)  {
            Debug.Log("Missing PerformingObj");
            return false;
        }
        if (TargetObj == null) {
            Debug.Log("Missing TargetObj");
            return false;
        }
        if (incrementAngle == 0) {
            Debug.Log("Missing incrementAngle");
            return false;
        }
        return true; 
    }
}       
