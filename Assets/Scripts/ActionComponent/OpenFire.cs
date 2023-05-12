using UnityEngine;
using System.Threading.Tasks;

public class OpenFire : Action
{
    [SerializeField] public int firingDelay = 1000;
    [SerializeField] private bool coolDown = false;
    [SerializeField] protected int firingMode = 0;

    protected LimitedAttackHandler LAH;
    protected override void Awake() {
        base.Awake();

        // there should only be one fire script on a single object
        taskDone = true; 
    }

    void Update() { 
        if ( GameManager.Instance && GameManager.Instance.isPaused() ) return; 
        if (!coolDown) {
            fire();
            coolDown = true;
            StartCoroutine("awaitCoolDown");
        }
    }

    protected async void awaitCoolDown() {
        await Task.Delay(firingDelay);
        coolDown = false; 
    }

    protected override void execute() {
        if (firingDelay <= 0) {
            this.enabled = false;
            return;
        }
        else if (firingDelay < 10) firingDelay = 10; 
        
        this.enabled = true;

    }

    protected override bool preCheck() {
        base.preCheck();

        bool valid = true;

        if(!PerformingObj.TryGetComponent<LimitedAttackHandler>(out LAH)) {
            LAH = PerformingObj.AddComponent<LimitedAttackHandler>();
        }
        
        return valid;
    }

    protected void fire() {
        switch (firingMode) {
            case 0: // single
                LAH.TrySingle(); 
                break; 
            case 1: // burst
                LAH.TryBurst();
                break; 
            default:
                Debug.LogWarning("Invalid shooting mode");
                break;
        }
    }
}