using UnityEngine;
using System.Threading.Tasks;

public class ActionAwait : Action
{
    [SerializeField] protected int delay = 0; 
    protected async override void execute() {
        await Task.Delay(delay);
        taskDone = true;
    }

    
}