using UnityEngine;

public abstract class Action2 : MobUtility 
{
    protected GameObject PerformingOn;

    protected bool taskDone = false;

    public bool TaskDone 
    {
        get { return taskDone; }
    }
    
    public abstract void execute(GameObject PerformOn);
}