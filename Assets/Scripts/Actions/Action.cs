using UnityEngine;

public abstract class Action : MonoBehaviour
{
    protected bool taskDone;
    public bool TaskDone
    {
        get { return taskDone; }
    }
    protected GameObject PerformingObj;
    public void Execute(GameObject performingObj) {
        PerformingObj = performingObj;
        execute(); 
    }
    protected abstract void execute();
}       
