using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class ActionToggleTrackObject : Action
{

    [SerializeField] private GameObject target; 
    [SerializeField] private int incrementAngle = 1;
    [SerializeField] public bool awaitOnTarget = false; // if true, taskDone will wait until the target is found before marking true

    protected async override void execute() {
        taskDone = false;

        // add primitive action script
        MobTrackObject MTO;
        if (!PerformingObj.TryGetComponent<MobTrackObject>(out MTO)) {
            MTO = PerformingObj.AddComponent<MobTrackObject>(); 
        }
            
        // init settings
        MTO.target = target;
        MTO.incrementAngle = incrementAngle; 

        MTO.Execute(PerformingObj); 

        // wait for the target to be found if awaiting Target
        if (awaitOnTarget && MTO.enabled) {
            while (!MTO.onTarget()) {
                await Task.Yield();
            }
        } 

        taskDone = true;
    }

    protected override bool preCheck() {
        bool valid = true;

        if (!base.preCheck()) valid = false;

        if (!target && PlayerHandler.Instance) {
            target = PlayerHandler.Instance.gameObject;
        }

        return valid; 
    }
}       
