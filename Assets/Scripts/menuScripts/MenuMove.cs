using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;
    public GameObject next_screen;
    public GameObject previous_screen;
    void Start()
    {
        button = GetComponent<Button>(); 
        button.onClick.AddListener(on_click);
    }

    void on_click() {
        Debug.Log("Main Menu Level Select Clicked");

        previous_screen.SetActive(false);
        next_screen.SetActive(true);

    }
}
