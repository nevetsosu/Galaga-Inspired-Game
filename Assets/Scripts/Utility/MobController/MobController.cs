using UnityEngine;

public abstract class MobController : MobUtility
{
    public bool inUse = false; // may REMOVE but this is meant to make sure no two scripts try to change postion at the saame time, or two direction scripts at the same time
}   