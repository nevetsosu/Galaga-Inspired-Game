using UnityEngine;
using UnityEngine.UI;

public class mainMenuHandler : MonoBehaviour
{
    public static mainMenuHandler Instance;

    public Button @continue; // contine button isn't currently used
    public Button @new; // referernce to the new game button

    [SerializeField] protected GameObject[] menus; // array of all menu UIs
    protected bool Cont = false; // whether continue or new should be shown // not yet implemented // wwould be set after checking save data
    protected GameObject cur_screen; // current screen // part of the legacy UI system, UI menus need a rework

    void Awake()
    {
        // only one main menu
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;

        // set default menu
        cur_screen = menus[0];

        // clear menus
        foreach (GameObject i in menus) {
            i.SetActive(false);
        }
        
        // start default menu
        cur_screen.SetActive(true);

        // determine new or continue button
        if (Cont) {
            @continue.gameObject.SetActive(true); 
            @new.gameObject.SetActive(false); 
        } else {
            @new.gameObject.SetActive(true); 
            @continue.gameObject.SetActive(false); 
        }
    }

    // change current menu
    public void loadScreen(int i) {
        cur_screen.SetActive(false);
        cur_screen = menus[i];
        cur_screen.SetActive(true);
    }

    // for the new Game button to execute on press
    public void newGameButton() {
        GameManager.Instance.LoadScene(1);
    }

    // for the exit button to execute on press
    public void exitButton() {
        GameManager.Instance.exitGame();
    }


}
