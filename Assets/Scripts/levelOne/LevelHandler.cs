using UnityEngine;
using System.Threading.Tasks;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] protected float timeElapsed = 0; 
    [SerializeField] protected TextMeshProUGUI timeText;
    public static LevelHandler Instance;
    public bool gameOver = false;
    public static float PLAYFIELDWIDTH = 36;

    void Awake() {
        // Singleton
        // if (Instance != null) {
        //     Destroy(gameObject);
        //     return;
        // }
        Instance = this;

        Time.timeScale = 1f;
    }

    async void Start() {
        gameOver = false; 
        // before game start

        // game start

        ActionSequence AS = gameObject.GetComponent<ActionSequence>();

        AS.Execute(); 

        while (!AS.TaskDone) {
            await Task.Yield(); 
        }

        gameOver = true; 
        
        // game end
    }
    
    void Update()
    {
        if (pauseMenu.Instance != null && !gameOver) {
            pauseMenu.Instance.pauseMenuCheck();
        }

        if (gameOver) { 
            endGame();
            this.enabled = false;
        } 
        // Check pause if there is a pause menu
        
    }

    void FixedUpdate() { 
        timeElapsed += Time.fixedDeltaTime;
        timeText.text = timeElapsed.ToString("0.00");
        if (GameManager.Instance) GameManager.Instance.FinalTime.text = "Elapsed Time: " + timeElapsed.ToString("0.00");
    }

    public void endGame() {
        StopAllCoroutines(); 
        GameManager.Instance.Pause();
        GameManager.Instance.showGameOverMenu();
    }
}
