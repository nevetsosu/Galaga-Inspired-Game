using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
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
}
