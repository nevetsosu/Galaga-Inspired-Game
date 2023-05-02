using UnityEngine;

public class testDriver : MonoBehaviour 
{
    void Awake() {
        Debug.Log("Driver Awakoen");
    }

    void Start() {
        Debug.Log("Driver Started");

        Debug.Log("ADding Componoent");

        test t1 = gameObject.AddComponent<test>();
        t1.enabled = false;
    }
}