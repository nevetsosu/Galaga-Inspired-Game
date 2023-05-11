using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler Instance;
    public bool GameOver = false;
    [SerializeField] protected bool levelWin = false; 
    public static float PLAYFIELDWIDTH = 36;

    public LevelData data = new LevelData();
    [SerializeField] protected float timeElapsed = 0; 
    [SerializeField] protected TextMeshProUGUI timeText;
    [SerializeField] protected TextMeshProUGUI scoresText;
    SortedList< float, string > scoreboard = new SortedList< float, string >(); 

    public float TimeElapsed
    {
        get { return timeElapsed; }
    }

    void Awake() {
        // Singleton
        // if (Instance != null) {
        //     Destroy(gameObject);
        //     return;
        // }
        Instance = this;

        Time.timeScale = 1f;

        data = GameManager.Instance.load();

        if (data.names.Count != data.times.Count ) {
            Debug.LogWarning("Times and names aren't lining up");
        }

        for (int i = 0; i < data.names.Count; i++) {
            Debug.Log("Adding name");
            // scoresText.text += data.names[i] + " " + data.times[i].ToString() + "\n";

            scoreboard.Add(data.times[i], data.names[i]);
        }

        int counter = 0; 
        foreach (var i in scoreboard) {
            if (counter++ >= 10) break;
            scoresText.text += i.Value + " " + i.Key.ToString("0.00") + "\n";
        }        
    }

    async void Start() {
        GameOver = false; 
        // before game start

        // game start

        ActionSequence AS = gameObject.GetComponent<ActionSequence>();

        AS.Execute(); 

        while (!AS.TaskDone) {
            await Task.Yield(); 
        }

        GameOver = true; 
        levelWin = true;
        
        // game end
    }
    
    void Update()
    {
        if (pauseMenu.Instance != null && !GameOver) {
            pauseMenu.Instance.pauseMenuCheck();
        }

        if (GameOver) { 
            this.enabled = false;
            endGame();
        } 
        // Check pause if there is a pause menu
        
    }

    void FixedUpdate() { 
        timeElapsed += Time.fixedDeltaTime;
        timeText.text = timeElapsed.ToString("0.00");
        if (GameManager.Instance) {
            GameManager.Instance.FinalTimeFail.text = "Elapsed Time: " + timeElapsed.ToString("0.00");
            GameManager.Instance.FinalTimeSuccess.text = "Elapsed Time: " + timeElapsed.ToString("0.00");
        }
    }

    public void endGame() {
        StopAllCoroutines(); 
        GameManager.Instance.Pause();
        if (levelWin) GameManager.Instance.showWinMenu();
        else GameManager.Instance.showGameOverMenu();
    }

    
}
