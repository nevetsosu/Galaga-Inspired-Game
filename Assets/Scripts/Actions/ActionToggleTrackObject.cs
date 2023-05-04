using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class ActionToggleTrackObject : Action
{

    [SerializeField] private GameObject target; 
    [SerializeField] private int incrementAngle = 1;
    [SerializeField] public bool awaitOnTarget = false;

    void Start() {
        execute();
    }

    protected async override void execute() {

        taskDone = false;

        MobTrackObject MTO;
        if (!PerformingObj.TryGetComponent<MobTrackObject>(out MTO)) {
            MTO = PerformingObj.AddComponent<MobTrackObject>(); 
        }
            
        MTO.target = target;
        MTO.incrementAngle = incrementAngle; 

        MTO.Execute(PerformingObj); 

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

        if (target == null) {
            target = PlayerHandler.Instance.gameObject;

            if (target == null) {
                Debug.Log("Missing TargetObj");
                valid = false;
            }
        }

        return valid; 
    }
}       
