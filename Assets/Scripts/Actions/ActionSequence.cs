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
    [SerializeField] protected int totalActions = 0;
    [SerializeField] protected int currentAction = 0;

    public int TotalActions
    {
        get { return totalActions; }
    }
    public int CurrentAction 
    {
        get { return currentAction; }
    }

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

        for (currentAction = 0; currentAction < totalActions; currentAction++) {
            Actions[currentAction].Execute(gameObject); 

            while (!Actions[currentAction].TaskDone) {
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

        totalActions = Actions.Count;

        return valid;
    }
}

