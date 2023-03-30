using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    public int Scene_Number = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(load);
    }

    void load() { 
        SceneManager.LoadScene(Scene_Number);
    }
}
