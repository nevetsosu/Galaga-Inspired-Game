using UnityEngine;
using UnityEngine.Splines;

public class ActionTEST : Action
{
    [SerializeField] SplineContainer spline;

    protected override void execute() {
        if (preCheck()) {
            Debug.Log("precheck pass");
            MobMoveDefinedPath MMDP;
            if (!PerformingObj.TryGetComponent<MobMoveDefinedPath>(out MMDP)) {
                MMDP = PerformingObj.AddComponent<MobMoveDefinedPath>();   
                MMDP.setSpline(spline);
            }

            MMDP.Resume();
        }
    }

    private bool preCheck() {
        bool valid = true;
        
        if (PerformingObj == null)  {
            Debug.Log("Missing Performing Obj");
            valid = false;
        }

        if (spline == null) {
            Debug.Log("Missing Spline");
            valid = false;
        }
        return valid; 
    }
}       
