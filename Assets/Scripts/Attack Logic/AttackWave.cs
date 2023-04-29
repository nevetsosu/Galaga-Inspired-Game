using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public abstract class AttackWave : MonoBehaviour
{
    protected struct SpawnInfo {
        public int time_before_spawn;
        public Vector2 initial_velocity;
    }

    protected bool requireFullClear;
    protected int defeated;
    protected List<GameObject> fleet;
    protected LinkedList<KeyValuePair<GameObject, SpawnInfo>> SpawnPattern;

    public AttackWave() {
        SpawnPattern = new LinkedList<KeyValuePair<GameObject, SpawnInfo>>();
        List<GameObject> fleet = new List<GameObject>();
    }

    // Sends the wave.
    protected async void execute() {
        foreach (KeyValuePair<GameObject, SpawnInfo> it in SpawnPattern) {
            await Task.Delay(it.Value.time_before_spawn);
            it.Key.SetActive(true);

            // Initialize default PHYSICS 
            it.Key.GetComponent<Rigidbody2D>().velocity = it.Value.initial_velocity;
        }
    }
    
    public virtual void addEnemy(GameObject prefab, int time_before_spawn, Vector2 initial_pos, Vector2 initial_velocity) {
        SpawnInfo info;
        info.time_before_spawn = time_before_spawn;
        info.initial_velocity = initial_velocity;

        GameObject new_enemy = Instantiate(prefab);

        // deactivate if not already deactivated
        new_enemy.SetActive(false);

        // initialize default NON-PHYSICS values
        new_enemy.transform.position = initial_pos;

        SpawnPattern.AddLast(new KeyValuePair<GameObject, SpawnInfo>(new_enemy, info));
    }
}
