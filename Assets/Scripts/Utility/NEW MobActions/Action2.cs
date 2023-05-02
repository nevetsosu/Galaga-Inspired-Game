using UnityEngine;

public abstract class Action2 : MobUtility 
{
    protected GameObject PerformingOn;

    protected bool taskDone = false;

    public bool TaskDone 
    {
        get { return taskDone; }
    }
    
    public void Execute(GameObject PerformOn) {
        this.PerformingOn = PerformOn;

        if (PerformingOn == null) { 
            Debug.Log("null PerformingOn");
            return;
        }

        if (!preCheck()) {
            Debug.Log("preCheck fail");
            return;
        }

        execute();
    }

    protected abstract void execute();
}