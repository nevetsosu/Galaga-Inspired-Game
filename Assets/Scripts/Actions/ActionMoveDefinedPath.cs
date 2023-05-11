    using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

// moves an object along a spline
public class ActionMoveDefinedPath : Action
{
    [SerializeField] protected SplineContainer spline;
    [SerializeField] protected bool isTracking = false;
    [SerializeField] protected bool loop = false; 
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected bool awaitOnePass = false;
    [SerializeField] protected float awaitRelativeProgress = 1; 
    protected MobMoveDefinedPath MMDP;

    protected override async void execute() {
        taskDone = false;

        // if this action is already enabled
        if (MMDP.enabled) { 
            // disable loop if its looping
            if (MMDP.Loop) MMDP.Loop = false;

            // wait for it to finish
            while (!MMDP.singlePass()) { 
                await Task.Yield();
            }
        }

        // initialize info
        MMDP.SplinePath = spline;
        MMDP.Speed = speed;
        MMDP.IsTracking = isTracking;
        MMDP.Loop = loop;
        MMDP.awaitRelativeProgress = awaitRelativeProgress;

        MMDP.Execute(PerformingObj);

        // wait for task to finish if awaiting One Pass
        while (awaitOnePass && !MMDP.singlePass()) {
            await Task.Yield();
        } 
        
        taskDone = true;
    }

    protected override bool preCheck() {
        bool valid = true;
        
        if(!base.preCheck()) valid = false; 

        if (spline == null) {
            Debug.Log("Missing Spline");
            valid = false;
        }

        if (!PerformingObj.TryGetComponent<MobMoveDefinedPath>(out MMDP)) {
            MMDP = PerformingObj.AddComponent<MobMoveDefinedPath>();  
        }

        return valid;
    }
}       
