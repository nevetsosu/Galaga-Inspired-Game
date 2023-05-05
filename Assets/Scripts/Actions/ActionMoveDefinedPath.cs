    using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class ActionMoveDefinedPath : Action
{
    [SerializeField] protected SplineContainer spline;
    [SerializeField] protected bool isTracking = false;
    [SerializeField] protected bool loop = false; 
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected bool awaitOnePass; 
    protected MobMoveDefinedPath MMDP;

    protected override async void execute() {
        taskDone = false;

        if (MMDP.enabled) { 
            if (MMDP.Loop) MMDP.Loop = false;

            while (!MMDP.singlePass()) { 
                await Task.Yield();
            }
        }

        MMDP.setSpline(spline);
        MMDP.Speed = speed;
        MMDP.IsTracking = isTracking;
        MMDP.Loop = loop;

        MMDP.Execute(PerformingObj);

        while (awaitOnePass && !MMDP.singlePass()) {
            await Task.Yield();
        } 
        
        taskDone = true;
    }

    protected override bool preCheck() {
        if (!PerformingObj.TryGetComponent<MobMoveDefinedPath>(out MMDP)) {
            MMDP = PerformingObj.AddComponent<MobMoveDefinedPath>();  
        }

        bool valid = true;

        if(!base.preCheck()) valid = false; 

        if (spline == null) {
            Debug.Log("Missing Spline");
            valid = false;
        }

        return valid;
    }
}       
