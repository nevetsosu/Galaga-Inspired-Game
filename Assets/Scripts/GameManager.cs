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
    [SerializeField] protected int currentLevel = 0;
    [SerializeField] protected GameObject GameOverUI;
    [SerializeField] protected GameObject WinUI; 
    [SerializeField] public TextMeshProUGUI FinalTimeFail; 
    [SerializeField] public TextMeshProUGUI FinalTimeSuccess;
    [SerializeField] public TextMeshProUGUI nameInput;
    private bool paused = false;
    public static GameManager Instance;

    public int CurrentLevel
    {
        get {return currentLevel; }
    }

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
        currentLevel = i; 
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

    public void showWinMenu() {
        WinUI.SetActive(true);
    }

    public void hideWinMenu() {
        WinUI.SetActive(false);
    }

    public void gameWinSaveAndQuitButton() {
        if (nameInput.text.Length > 6) return; 
        
        
        if (GameManager.Instance.nameInput.text.Trim().Length != 1) {
            savePlayer(); 
        }
        
        hideWinMenu();
        mainMenuReset();
    }

    public void gameOverMainMenuButton() {
        hideGameOverMenu();
        mainMenuReset();
    }

    private void mainMenuReset() {
        GameManager.Instance.Resume();
        GameManager.Instance.exitLevel(); 
    }

    public void save(LevelData saveData) {
        for (int i = 0; i < saveData.names.Count; i++) {
            Debug.Log("BEFORE SAVE" + saveData.names[i] + " " + saveData.times[i]);
        }
        
        string fileName = "level" + currentLevel.ToString();
        string strData = JsonUtility.ToJson(saveData);
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        Debug.Log("Saving at path " + path);

        System.IO.File.WriteAllText(path, strData);
        Debug.Log("Save completed.");
    }

    public LevelData load() {
        Debug.Log("attempting data load");

        string fileName = "level" + currentLevel.ToString();
        string path = Application.persistentDataPath + "/" + fileName + ".json";

        if (System.IO.File.Exists(path)) {
            string strData = System.IO.File.ReadAllText(path);
            
            return JsonUtility.FromJson<LevelData>(strData);
        } else { 
            Debug.Log("Old file not found, returning new data");
            return new LevelData(); 
        }
    }

    public void savePlayer() {
        LevelHandler.Instance.data.times.Add(LevelHandler.Instance.TimeElapsed);
        LevelHandler.Instance.data.names.Add(GameManager.Instance.nameInput.text);

        save(LevelHandler.Instance.data);
    }

    public void clearAllData() {
        for (int i = 0; i < 6; i++) {
            string fileName = "level" + i;
            string path = Application.persistentDataPath + "/" + fileName + ".json";

            System.IO.File.WriteAllText(path, JsonUtility.ToJson(new LevelData()));
        }
    }

}

[System.Serializable]
public class LevelData {
    public List<float> times = new List<float>(); 
    public List<string> names = new List<string>();
}

