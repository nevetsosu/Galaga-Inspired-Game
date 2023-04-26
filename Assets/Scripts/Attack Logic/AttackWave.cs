using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackWave : MonoBehaviour
{
    protected bool requireFullClear;
    protected bool defeated;
    List<GameObject> fleet;

    public AttackWave() {
        requireFullClear = false;
        defeated = false;
    }

    // Sends the wave.
    public abstract void execute();

    // 
    public bool Defeated() {
        return defeated;
    }

    // Returns whether a wave should be fulled cleared before the next wave executes.
    public bool RequireFullClear() {
        return requireFullClear;
    }
}
