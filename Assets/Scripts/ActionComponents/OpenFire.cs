using UnityEngine;
using System.Threading.Tasks;

public class OpenFire : Action
{
    public static OpenFire Instance;
    [SerializeField] public int firingDelay = 1000;
    [SerializeField] private bool coolDown = false;

    protected override void Awake() {
        base.Awake();

        // there should only be one fire script on a single object
        if (Instance != null) {
            Destroy(this);
            return; 
        }
        Instance = this;

        taskDone = true; 
    }

    void Update() { 
        if (!coolDown) {
            PerformingObj.GetComponent<Mob>().attack();
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

        if(PerformingObj.GetComponent<Mob>() == null) {
            Debug.Log("Mob component not found");
        }
        return valid;
    }
}