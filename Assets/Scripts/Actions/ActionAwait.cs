using UnityEngine;
using System.Threading.Tasks;

// just waits a certain amount of time
public class ActionAwait : Action
{
    [SerializeField] protected int delay = 0; 
    

    protected async override void execute() {
        await Task.Delay(delay);
        taskDone = true;
    }
}