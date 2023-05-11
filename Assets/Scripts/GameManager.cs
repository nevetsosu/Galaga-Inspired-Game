using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    // initialized in the editor 
    [SerializeField] public TextMeshProUGUI FinalTimeFail; // final time on the fail screen
    [SerializeField] public TextMeshProUGUI FinalTimeSuccess; // final time on the success screen
    [SerializeField] public TextMeshProUGUI nameInput; // name input field on the success screen

    public static GameManager Instance;
    

    // initialized in editor
    [SerializeField] protected GameObject GameOverUI; 
    [SerializeField] protected GameObject WinUI; 

    [SerializeField] protected int currentLevel = 0; // for data saving
    private bool paused = false;

    public int CurrentLevel
    {
        get {return currentLevel; }
    }

    private void Awake() {
        // only one game manager
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;

        // keep across scenes
        DontDestroyOnLoad(gameObject);
    } 

    private void Start() {
        // game start prep
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
        if (nameInput.text.Length > 6) return; // names cannot be longer than length 5, (starting field size of 1 not 0)  
        
        if (GameManager.Instance.nameInput.text.Trim().Length != 1) { // dont save if nothing is in the field
            savePlayer(); 
            GameManager.Instance.nameInput.text = "";
        }

        // back to main menu
        hideWinMenu();
        mainMenuReset();
    }

    public void gameOverMainMenuButton() {
        // back to main menu
        hideGameOverMenu();
        mainMenuReset();
    }

    private void mainMenuReset() {
        // unpause time and return to main menu
        GameManager.Instance.Resume();
        GameManager.Instance.exitLevel(); 
    }

    public void save(LevelData saveData) {     
        // serialize and store with name format levelx, where x is the scene ID.   
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

        // return empty or populated LevelData depending if there is a save or not
        if (System.IO.File.Exists(path)) {
            string strData = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<LevelData>(strData);
        } else { 
            Debug.Log("Old file not found, returning new data");
            return new LevelData(); 
        }
    }

    public void savePlayer() {
        // add player data
        LevelHandler.Instance.data.times.Add(LevelHandler.Instance.TimeElapsed);
        LevelHandler.Instance.data.names.Add(GameManager.Instance.nameInput.text);

        // write LevelData to disk
        save(LevelHandler.Instance.data);
    }

    public void clearAllData() {
        Debug.Log("Clearing scene data 0 through " + SceneManager.sceneCountInBuildSettings);
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string fileName = "level" + i;
            string path = Application.persistentDataPath + "/" + fileName + ".json";

            System.IO.File.WriteAllText(path, JsonUtility.ToJson(new LevelData()));
        }
    }

}

// store key value pairs as two lists
[System.Serializable]
public class LevelData {
    public List<float> times = new List<float>(); 
    public List<string> names = new List<string>();
}

