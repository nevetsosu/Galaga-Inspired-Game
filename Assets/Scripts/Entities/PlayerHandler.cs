using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private float horizontalInput;
    public float force_magnetude = 500;
    public float drag = 10;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().drag = drag;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        // transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);  
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Time.deltaTime * force_magnetude * horizontalInput, ForceMode2D.Impulse);
    }
}
