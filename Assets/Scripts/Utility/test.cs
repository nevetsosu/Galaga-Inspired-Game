using UnityEngine;

public class test : MonoBehaviour {
    void Awake() {
        Debug.Log("test awake");
        this.enabled = false; 
    }

    void Start() {
        Debug.Log("Test STArt");
    }
}