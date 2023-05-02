using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class ActionMoveDefinedPath : Action
{
    [SerializeField] SplineContainer spline;

    protected override void execute() {
        if (preCheck()) {
            MobMoveDefinedPath MMDP;
            if (!PerformingObj.TryGetComponent<MobMoveDefinedPath>(out MMDP)) {
                MMDP = PerformingObj.AddComponent<MobMoveDefinedPath>();   
                MMDP.setSpline(spline);
            }

            MMDP.Resume();
        }
    }

    private bool preCheck() {
        if (PerformingObj == null) return false;
        if (spline == null) return false;
        return true; 
    }
}       
