using UnityEngine;
using System.Threading.Tasks;

public class ActionOpenFire : Action2
{
    [SerializeField] private int firingDelay = 1000;
    [SerializeField] private bool coolDown = false;

    public override void execute() {
        
    }

    void Start() {
        taskDone = true; 
    }

    void Update() {
        if (!coolDown) {
            gameObject.GetComponent<Mob>().attack();
            coolDown = true;
            StartCoroutine("awaitCoolDown");
        }
    }

    protected async void awaitCoolDown() {
        await Task.Delay(firingDelay);
        coolDown = false; 
    }
}