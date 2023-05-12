using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public static pauseMenu Instance;

    public GameObject pauseMenuUI;
    
    private void Awake() {

        // only one pause menu
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

    // for the mainMenu Button on the pause menu to execute on push
    public void mainMenuButton() {
        LevelHandler.Instance.GameOver = false;
        hidePauseMenu();
        GameManager.Instance.Resume();
        GameManager.Instance.exitLevel(); 
    }

    // for the Resume button on the pause menu to execute on push
    public void resumeButton() {
        hidePauseMenu(); 
        GameManager.Instance.Resume();
    }

    // checks if the user is trying to toggle pause
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
