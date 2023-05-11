using UnityEngine;
using System.Threading.Tasks;

public class awaitDeath : Action
{
    [SerializeField] protected GameObject awaitee;
    [SerializeField] protected int pollingRate = 1000;

    protected override async void execute() {
        taskDone = false;

        while (awaitee) {
            if (pollingRate <= 1) {
                await Task.Yield();
            } else {
                await Task.Delay(pollingRate);
            }
        }

        taskDone = true;
    }
}
