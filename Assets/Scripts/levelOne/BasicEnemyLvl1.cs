using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyLvl1 : EnemyHandler
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In Lvl 1 #2/3");
        enemy = GetComponent<Rigidbody2D>();
        enemy.velocity = new Vector2(speed, -1);
    }
}
