using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler Instance;
    [SerializeField] public bool GameOver = false;
    [SerializeField] protected bool levelWin = false; 

    public static float PLAYFIELDWIDTH = 36;
    public LevelData data = new LevelData();

    [SerializeField] protected float timeElapsed = 0; 
    [SerializeField] protected TextMeshProUGUI timeText;
    [SerializeField] protected TextMeshProUGUI scoresText;
    protected SortedList< float, string > scoreboard = new SortedList< float, string >(); 

    public float TimeElapsed
    {
        get { return timeElapsed; }
    }

    void Awake() {
        // one level at a time
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // at some point timeScale started defaulting to zero and i dont wanna have to go find it, this works 
        GameManager.Instance.Resume();

        // attempt data load
        data = GameManager.Instance.load();

        // double check data validity
        if (data.names.Count != data.times.Count ) {
            Debug.LogWarning("Times and names aren't lining up");
        }

        // add data to sorted list // perhaps it would be better to find a way to serialize the SORTED data, but this works
        for (int i = 0; i < data.names.Count; i++) {
            scoreboard.Add(data.times[i], data.names[i]);
        }

        // only show 10 scoreboard entries
        int counter = 0; 
        foreach (var i in scoreboard) {
            if (counter++ >= 10) break;
            scoresText.text += i.Value + " " + i.Key.ToString("0.00") + "\n";
        }        
    }

    async void Start() { 

        // start level
        ActionSequence AS = gameObject.GetComponent<ActionSequence>();
        AS.Execute(); 

        // await level finish
        while (!AS.TaskDone) {
            await Task.Yield(); 
        }

        // end level with success
        GameOver = true; 
        levelWin = true;
    }
    
    void Update()
    {
        // check for pause menu 
        if (pauseMenu.Instance != null && !GameOver) {
            pauseMenu.Instance.pauseMenuCheck();
        }

        // lose game by default and stop updating level
        if (GameOver) { 
            this.enabled = false;
            endGame();
        }  
    }

    void FixedUpdate() { 
        // time elapsed update
        timeElapsed += Time.fixedDeltaTime;

        // update time display bottom right
        timeText.text = timeElapsed.ToString("0.00");
        
        // update time displays on menus
        if (GameManager.Instance) {
            GameManager.Instance.FinalTimeFail.text = "Elapsed Time: " + timeElapsed.ToString("0.00");
            GameManager.Instance.FinalTimeSuccess.text = "Elapsed Time: " + timeElapsed.ToString("0.00");
        }
    }

    public void endGame() {
        StopAllCoroutines(); 
        GameManager.Instance.Pause();

        // determine win or lose screen
        if (levelWin) GameManager.Instance.showWinMenu();
        else GameManager.Instance.showGameOverMenu();
    }

    
}
