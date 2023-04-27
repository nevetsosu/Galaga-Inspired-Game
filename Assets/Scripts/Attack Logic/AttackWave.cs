using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public abstract class AttackWave : MonoBehaviour
{
    protected bool requireFullClear;
    protected int defeated;
    List<GameObject> fleet;

    protected struct SpawnInfo {
        public int time_before_spawn;
        public Transform initial_transform;
        public Vector2 initial_velocity;
    }

    LinkedList<KeyValuePair<GameObject, SpawnInfo>> SpawnPattern;

    // Sends the wave.
    public async virtual void execute() {
        foreach (KeyValuePair<GameObject, SpawnInfo> it in SpawnPattern) {
            await Task.Delay(it.Value.time_before_spawn);
            GameObject n = Instantiate(it.Key, it.Value.initial_transform);
            n.GetComponent<Rigidbody2D>().velocity = it.Value.initial_velocity; // maybe dont need to set initial velocity
        }
    }

    public int Defeated() {
        return defeated;
    }

    // Returns whether a wave should be fulled cleared before the next wave executes.
    public bool RequireFullClear() {
        return requireFullClear;
    }

    public void addEnemy(GameObject enemy, int time_before_spawn, Transform initial_transform, Vector2 initial_velocity) {
        SpawnInfo info;
        info.time_before_spawn = time_before_spawn;
        info.initial_transform = initial_transform;
        info.initial_velocity = initial_velocity;

        SpawnPattern.AddLast(new KeyValuePair<GameObject, SpawnInfo>(enemy, info));
    }
}
