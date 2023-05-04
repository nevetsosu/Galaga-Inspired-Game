using UnityEngine;

public abstract class Action : MobUtility 
{
    [SerializeField] protected GameObject PerformingObj;

    protected bool taskDone = false;

    public bool TaskDone 
    {
        get { return taskDone; }
    }

    protected override void Awake() {
        base.Awake();    
        if (PerformingObj == null) PerformingObj = gameObject;
        Debug.Log("Mob Action2 Awake");
    }

    public void Execute() {
        if (PerformingObj == null) { 
            Debug.Log("null PerformingOn");
            return;
        }

        if (!preCheck()) {
            Debug.Log("preCheck fail");
            return;
        }

        execute();
    }
    
    public void Execute(GameObject PerformObj) {
        this.PerformingObj = PerformObj;
        Execute();
    }

    protected virtual void execute() {
        this.enabled = true;
    }
}