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
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Time.timeScale = 1f;
    }

    async void Start() {
        // before game start

        // game start

        ActionSequence AS = gameObject.GetComponent<ActionSequence>();

        AS.Execute(); 

        while (!AS.TaskDone) {
            await Task.Yield(); 
        }

        // pause time
        if (!gameOver) {
            Debug.Log("Level complete!");
            endGame();
        }
        

        // game end
    }
    
    void Update()
    {
        // Check pause if there is a pause menu
        if (pauseMenu.Instance != null && !gameOver) {
            pauseMenu.Instance.pauseMenuCheck();
        }
    }

    void FixedUpdate() { 
        timeElapsed += Time.fixedDeltaTime;
        timeText.text = timeElapsed.ToString("0.00");
        if (GameManager.Instance) GameManager.Instance.FinalTime.text = "Elapsed Time: " + timeElapsed.ToString("0.00") + "s";
    }

    public void endGame() {
        gameOver = true;
        GameManager.Instance.Pause();
        GameManager.Instance.showGameOverMenu();
        StopAllCoroutines(); 
    }
}
