using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaveType1 : AttackWave
{
  public GameObject enemyPrefab;
  void Awake() {
    SpawnPattern = new LinkedList< KeyValuePair< GameObject, SpawnInfo > >(); 

    for(int i = 0; i < 10; i++) {
      addEnemy(enemyPrefab, 1000, new Vector2(0f, 40f), new Vector2(5f, -2f));
      addEnemy(enemyPrefab, 0, new Vector2(0f, 35f), new Vector2(5f, -2f));
    }
    
    execute(); 
  }
}
