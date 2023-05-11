using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

// executes a list of actions in order
public class ActionSequence : Action
{
    [SerializeField] GameObject ActionList; // Object that contains the list of actions as components of this object
    [SerializeField] protected int totalActions = 0;
    [SerializeField] protected int currentAction = 0;
    [SerializeField] protected int taskDoneAfterActionID = 0; // at what point the action sequence is consider done ( marks taskDone )
    [SerializeField] protected bool executeAsSelf = true; // default execute on self

    protected List<Action> Actions = new List<Action>();

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
        // if the task is already executing await its finish before starting again
        while (!taskDone) {
            await Task.Yield();
        }

        taskDone = false;

        // do all actions before task Done
        for (currentAction = 0; currentAction < Mathf.Clamp(taskDoneAfterActionID + 1, 0, totalActions); currentAction++) {
            await doAction(currentAction); 
        }

        taskDone = true;

        // do all actions after task done
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
        
        // execute the current action
        if (Actions[currentAction]) {
            Actions[currentAction].gameObject.SetActive(true);
            if (executeAsSelf) Actions[currentAction].Execute(PerformingObj); 
            else Actions[currentAction].Execute();
        }
        
        // wait for it to be done and check if it still exists (its object has died or not)
        while (!Actions[currentAction].TaskDone && Actions[currentAction]) {
            await Task.Yield(); 
        }

        // if (!Actions[currentAction]) return;
    }
}

