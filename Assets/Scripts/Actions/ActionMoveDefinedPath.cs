using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class ActionMoveDefinedPath : Action
{
    [SerializeField] SplineContainer spline;
    [SerializeField] bool isTracking = false;
    [SerializeField] bool loop = false; 
    [SerializeField] protected float speed = 1;

    protected override async void execute() {
        if (preCheck()) {
            MobMoveDefinedPath MMDP;
            if (!PerformingObj.TryGetComponent<MobMoveDefinedPath>(out MMDP)) {
                MMDP = PerformingObj.AddComponent<MobMoveDefinedPath>();  
            } else {
                if (MMDP.Loop) { 
                    MMDP.Loop = false;
                }

                while (!MMDP.singlePass()) { 
                    await Task.Yield();
                }
                
            }

            MMDP.setSpline(spline);
            MMDP.Speed = speed;
            MMDP.IsTracking = isTracking;
            MMDP.Loop = loop;

            MMDP.Resume();

            while (!MMDP.singlePass()) {
                await Task.Yield();
            } 
            
            taskDone = true;

        }
    }

    async void test() {
        await Task.Delay(1000); 
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
