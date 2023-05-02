using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public abstract class Action : MonoBehaviour
{
    protected GameObject PerformingObj;
    public void Execute(GameObject performingObj) {
        PerformingObj = performingObj;
        execute(); 
    }
    protected abstract void execute();
    public void performAs(GameObject as_this) {
        PerformingObj = as_this;
    }
}       
