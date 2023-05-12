using UnityEngine;
using UnityEngine.UI;

// each level button could just bring you to the level and that would make this simplier but we wanted a small challenge
public class levelSelectMenu : MonoBehaviour
{
    public static levelSelectMenu Instance;
    public Button select;
    private int selectedLevel = 0;
    private bool selectionMade = false;

    private void Awake() {
        // only one level select menu
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;
        
        // no level selected by default
        selectionMade = false;
    } 

    public void selectLevel(int i) {
        selectionMade = true;
        selectedLevel = i;
    }

    // for back button to execute on press
    public void backButton() {
        selectionMade = false;
        mainMenuHandler.Instance.loadScreen(0);
    }

    // for select button to execute on press
    public void selectButton() {
        if (selectionMade) {
            selectionMade = false;
            GameManager.Instance.LoadScene(selectedLevel);
        }
    }
}
