using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler Instance;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    void FixedUpdate()
    {
        if(pauseMenu.Instance != null) {
            pauseMenu.Instance.pauseMenuCheck();
        }
    }
}
