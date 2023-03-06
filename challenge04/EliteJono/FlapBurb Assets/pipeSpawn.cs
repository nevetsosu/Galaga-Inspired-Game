using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeSpawn : MonoBehaviour
{
    public GameObject pipe;
    public float rate = 2;
    private float timer = 0;
    public float heightOffset = 10;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < rate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawn();
            timer = 0;
        }

        
    }

    void spawn()
    {
        float low = transform.position.y - heightOffset;
        float high = transform.position.y + heightOffset;
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(low, high), 0), transform.rotation);
    }
}
