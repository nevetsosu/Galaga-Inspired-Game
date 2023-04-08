using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSelectMenu : MonoBehaviour
{
    public static levelSelectMenu Instance;
    public Button select;
    private int selectedLevel = 0;
    private bool selectionMade = false;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;

        selectionMade = false;
    } 

    public void selectLevel(int i) {
        selectionMade = true;
        selectedLevel = i;
    }

    public void backButton() {
        selectionMade = false;
        mainMenuHandler.Instance.loadScreen(0);
    }

    public void selectButton() {
        if (selectionMade) {
            selectionMade = false;
            GameManager.Instance.LoadScene(selectedLevel);
        }
    }
}
