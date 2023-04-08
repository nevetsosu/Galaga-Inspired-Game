using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuHandler : MonoBehaviour
{
    private bool Cont = false;
    public static mainMenuHandler Instance;

    public Button @continue;
    public Button @new;

    private GameObject cur_screen;

    public GameObject[] menus;

    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;

        // set default menu
        cur_screen = menus[0];

        // clear menus
        foreach (GameObject i in menus) {
            i.SetActive(false);
        }
        
        // start default menu
        cur_screen.SetActive(true);

        // determine new or continue button
        if (Cont) {
            @continue.gameObject.SetActive(true); 
            @new.gameObject.SetActive(false); 
        } else {
            @new.gameObject.SetActive(true); 
            @continue.gameObject.SetActive(false); 
        }
    }

    public void loadScreen(int i) {
        cur_screen.SetActive(false);
        cur_screen = menus[i];
        cur_screen.SetActive(true);
    }

    public void newGameButton() {
        GameManager.Instance.LoadScene(1);
    }

    public void exitButton() {
        GameManager.Instance.exitGame();
    }


}
