using UnityEngine;

public abstract class Action : MobUtility 
{
    [SerializeField] protected GameObject PerformingObj;
    [SerializeField] protected bool taskDone = false;
    [SerializeField] protected bool activateOnWake = false;

    [SerializeField] public bool ActivateOnWake 
    {
        get { return activateOnWake; }
    }

    public bool TaskDone 
    {
        get { return taskDone; }
    }

    protected override void Awake() {
        base.Awake();

        // default performing obj is self
        PerformingObj = gameObject;

        if (ActivateOnWake) Execute(); 
    }

    // execute without assigning performing obj
    public void Execute() {
        if (preCheck()) {
            execute();
        } else {
            Debug.LogError("Precheck fail on " + PerformingObj.name);
        }
    }
    
    // execute with assigning performing Obj
    public void Execute(GameObject PerformObj) {
        this.PerformingObj = PerformObj;
        Execute();
    }

    protected virtual bool preCheck() {
        bool valid = true;

        if (!PerformingObj) { 
            Debug.Log("null PerformingOn");
            valid = false;
        }

        return valid;
    }

    protected virtual void execute() {
        this.enabled = true;
    }
}