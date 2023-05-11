using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public static pauseMenu Instance;

    public GameObject pauseMenuUI;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;
    } 

    public void showPauseMenu() { 
        pauseMenuUI.SetActive(true); 
    }

    public void hidePauseMenu() {
        pauseMenuUI.SetActive(false); 
    }

    public void mainMenuButton() {
        LevelHandler.Instance.GameOver = false;
        hidePauseMenu();
        GameManager.Instance.Resume();
        GameManager.Instance.exitLevel(); 
    }

    public void resumeButton() {
        hidePauseMenu(); 
        GameManager.Instance.Resume();
    }

    public void pauseMenuCheck() {
        if (Input.GetButtonDown("Cancel")) {
            if(GameManager.Instance.isPaused()) {
                resumeButton();
            } else {
                GameManager.Instance.Pause();
                showPauseMenu();
            }
        }
    }

}
