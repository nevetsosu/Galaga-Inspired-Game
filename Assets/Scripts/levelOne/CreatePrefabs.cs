using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CreatePrefabs : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab; // the prefab you want to instantiate
    public int count = 5; // the number of prefabs you want to spawn
    public Vector2 location = new Vector2(0,40); // the location where you want to spawn the prefabs

    async void Start(){

        for (int i = 0; i < count; i++)
        {
            GameObject SpawnedInstance = Instantiate(prefab, location, Quaternion.identity);

            BasicEnemyLvl1 enemyScript = SpawnedInstance.GetComponent<BasicEnemyLvl1>();
            Debug.Log(i);
            await Task.Delay(1000);
        }
    }
}
