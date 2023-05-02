using UnityEngine;
using UnityEngine.Splines;

public class ActionTEST : Action
{
    [SerializeField] SplineContainer spline;

    protected override void execute() {
        if (preCheck()) {
            
        }
    }

    private bool preCheck() {
        bool valid = true;
        return valid; 
    }
}       
