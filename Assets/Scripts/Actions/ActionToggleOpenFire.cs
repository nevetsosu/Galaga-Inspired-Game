using UnityEngine;

public class ActionToggleOpenFire : Action
{
    [SerializeField] protected int firingDelay = 1000;

    protected override void execute() {
        OpenFire OF; 

        // make sure there is an open fire component
        if (!PerformingObj.TryGetComponent<OpenFire>(out OF)) {
            OF = PerformingObj.AddComponent<OpenFire>();
        }
        
        // init settings
        OF.firingDelay = firingDelay;

        OF.Execute(PerformingObj); 

        taskDone = true;
    }
}