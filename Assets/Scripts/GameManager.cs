using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject GameOverUI;
    [SerializeField] public TextMeshProUGUI FinalTime; 
    private bool paused = false;
    public static GameManager Instance;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    } 

    private void Start() {
        AudioManager.Instance.Play("BGM");
        Resume(); 
    }

    public void Resume() {
        paused = false;
        Time.timeScale = 1f;
    }

    public void Pause() {
        paused = true;
        Time.timeScale = 0f;
    }

    public bool isPaused() {
        return paused;
    }

    public void LoadScene(int i) {
        SceneManager.LoadScene(i);
    }

    public void exitLevel() {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }

    public void showGameOverMenu() {
        GameOverUI.SetActive(true);
    }

    public void hideGameOverMenu() {
        GameOverUI.SetActive(false); 
    }

    public void mainMenuButton() {
        hideGameOverMenu();
        GameManager.Instance.Resume();
        GameManager.Instance.exitLevel(); 
    }

}
