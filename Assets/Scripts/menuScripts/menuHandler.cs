using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuHandler : MonoBehaviour
{
    bool Cont = false;
    public Button continueButton;
    public Button newButton;

    public GameObject mainScreen;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] menus = GameObject.FindGameObjectsWithTag("Menu");

        foreach (GameObject i in menus) {
            i.SetActive(false);
        }

        mainScreen.SetActive(true);

        if (Cont) {
            continueButton.gameObject.SetActive(true); 
            newButton.gameObject.SetActive(false); 
        } else {
            newButton.gameObject.SetActive(true); 
            continueButton.gameObject.SetActive(false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
