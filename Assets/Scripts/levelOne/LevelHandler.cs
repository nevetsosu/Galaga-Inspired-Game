using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler Instance;
    public static float PLAYFIELDWIDTH = 36;

    void Awake() {
        // Singleton
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void FixedUpdate()
    {
        // Check pause if there is a pause menu
        if(pauseMenu.Instance != null) {
            pauseMenu.Instance.pauseMenuCheck();
        }
    }
}
