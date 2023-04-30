using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TESTENEMY : Enemy
{
    protected bool attackLock;
    protected int attackAwaitTime;

    void Awake() {
        attackLock = true;
        attackAwaitTime = 1000;
    }

    void Start() {
        attackLock = false;
    }

    void Update()
    {
        rotateTowardPlayer(1f); 
        if (!attackLock) {
            attack();
            attackLock = true; 

            StartCoroutine("awaitAttackUnlock");
        }
    }
    async void awaitAttackUnlock() {
        await Task.Delay(attackAwaitTime);
        attackLock = false;
    }
}

