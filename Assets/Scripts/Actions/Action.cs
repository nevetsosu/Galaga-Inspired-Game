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
    }

    public void Execute() {
        if (preCheck()) {
            execute();
        } else {
            Debug.Log("Precheck fail ACTION");
        }
    }
    
    public void Execute(GameObject PerformObj) {
        this.PerformingObj = PerformObj;
        Execute();
    }

    protected override bool preCheck() {
        bool valid = true;

        if (PerformingObj == null) { 
            Debug.Log("null PerformingOn");
            valid = false;
        }

        return valid;
    }

    protected virtual void execute() {
        this.enabled = true;
    }
}