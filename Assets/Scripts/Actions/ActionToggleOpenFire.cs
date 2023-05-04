using UnityEngine;

public class ActionToggleOpenFire : Action
{
    [SerializeField] private int firingDelay = 1000;

    protected override void execute() {
        OpenFire OF; 
        if (!PerformingObj.TryGetComponent<OpenFire>(out OF)) {
            OF = PerformingObj.AddComponent<OpenFire>();
        }
        
        OF.firingDelay = firingDelay;

        OF.Execute(PerformingObj); 

        taskDone = true;
    }
}