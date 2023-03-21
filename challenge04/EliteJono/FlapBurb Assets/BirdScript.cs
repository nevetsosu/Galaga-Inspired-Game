using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myBody;
    public float flapStr;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Mumei";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myBody.velocity = Vector2.up * flapStr;
        }
    }
} 
