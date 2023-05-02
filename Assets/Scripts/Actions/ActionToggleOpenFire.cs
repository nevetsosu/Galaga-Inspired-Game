using UnityEngine;

public class ActionToggleOpenFire : Action
{
    private OpenFire OF; 
    [SerializeField] private int FireRateDelay = 1000;

    protected override void execute() {
        if (!PerformingObj.TryGetComponent<OpenFire>(out OF)) {
            OF = PerformingObj.AddComponent<OpenFire>();
        }

        if (!OF.Firing) {
            OF.FireRateDelay = FireRateDelay;
        }

        OF.toggleOpenFire();
        taskDone = true;
    }
}