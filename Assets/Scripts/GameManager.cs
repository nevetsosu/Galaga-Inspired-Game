using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Button continueButton;
    public Button newButton;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (Load_Game_Data()) {
            continueButton.enabled = true;
            newButton.gameObject.SetActive(false); 
        } else {
            newButton.enabled = true;
            continueButton.gameObject.SetActive(false); 
        }
    }

    bool Load_Game_Data() {
        // data not loaded
        return false;

        // data loaded
        // return true; 
    }   


    void load_level_one() {
        SceneManager.LoadScene(1);
    }
}
