using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class ActionSequence : Action
{
    List<Action> Actions = new List<Action>();
    [SerializeField] GameObject ActionList;
    [SerializeField] protected int totalActions = 0;
    [SerializeField] protected int currentAction = 0;
    [SerializeField] protected int taskDoneAfterActionID = 0; 
    [SerializeField] protected bool executeAsSelf = true;

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
        taskDone = true;
    }

    protected override async void execute() {
        while (!taskDone) {
            await Task.Yield();
        }

        taskDone = false;

        for (currentAction = 0; currentAction < Mathf.Clamp(taskDoneAfterActionID + 1, 0, totalActions); currentAction++) {
            await doAction(currentAction); 
        }

        taskDone = true;

        for (; currentAction < totalActions; currentAction++) {
            await doAction(currentAction);
        }
    }

    protected override bool preCheck()
    {
        bool valid = true; 

        if (!ActionList) {
            ActionList = PerformingObj.transform.GetChild(0).gameObject;

            if (!ActionList) {
                Debug.LogError("Action list null");
                return false; 
            }
        }

        foreach (Action a in ActionList.GetComponents<Action>()) {
            Actions.Add(a); 
        }

        for (int i = 0 ; i < ActionList.transform.childCount; i++) {
            GameObject GM = ActionList.transform.GetChild(i).gameObject;

            if (GM.TryGetComponent<Action>(out Action A)) {
                Actions.Add(A);
            }
        }

        totalActions = Actions.Count;
        taskDoneAfterActionID = Mathf.Clamp(taskDoneAfterActionID, 0, totalActions - 1);

        return valid;
    }

    private async Task doAction(int currentAction) {
        if (Actions[currentAction]) {
            Actions[currentAction].gameObject.SetActive(true);
            if (executeAsSelf) Actions[currentAction].Execute(PerformingObj); 
            else Actions[currentAction].Execute();
        }
        
        while (!Actions[currentAction].TaskDone && Actions[currentAction]) {
            await Task.Yield(); 
        }

        if (!Actions[currentAction]) return;
    }
}

