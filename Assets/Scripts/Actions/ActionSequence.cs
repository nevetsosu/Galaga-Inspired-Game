using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class ActionSequence : Action
{
    List<Action> Actions = new List<Action>();
    [SerializeField] GameObject ActionList;
    [SerializeField] protected bool ActivateOnWake = false;

    protected override void Awake() {
        base.Awake();

        if (ActionList == null) ActionList = gameObject.transform.GetChild(0).gameObject;

        taskDone = true;
        if (ActivateOnWake) Execute(gameObject); 
    }

    protected override async void execute() {
        while (!taskDone) {
            await Task.Yield();
        }

        taskDone = false;
        foreach (Action a in Actions) {
            a.Execute(gameObject); 

            while (!a.TaskDone) {
                await Task.Yield(); 
            }
        }  
        taskDone = true;
    }

    protected override bool preCheck()
    {
        bool valid = true; 

        if (ActionList == null) {
            Debug.Log("invalid list");
            valid = false;
        }

        foreach (Action a in ActionList.GetComponentsInChildren<Action>()) {
            if (a == this) {
                Debug.Log("Found myself, skipping");
                continue;
            }

            Actions.Add(a); 
        }

        return valid;
    }
}

