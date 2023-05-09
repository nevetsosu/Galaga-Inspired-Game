using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    protected HealthController HC;
    [SerializeField] protected TextMeshProUGUI TM;
    [SerializeField] protected GameObject subject;

    void Awake() {
         if (!subject) {
            if (PlayerHandler.Instance) {
                subject = PlayerHandler.Instance.gameObject;
            }
            else {
                Debug.LogWarning("no subject and no player instance");
                this.enabled = false;
            }
        }
        
        if (!subject.TryGetComponent<HealthController>(out HC)) {
            Debug.LogWarning("no health controller");
            this.enabled = false;
        }

        if (!TM) {
            Debug.LogWarning("no text mesh");
            this.enabled = false;
        }
    }
    void Start()
    {
        TM.text = "100";
    }

    // Update is called once per frame
    void Update()
    {
        if (subject) TM.text = HC.Health.ToString();
        else Destroy(gameObject);
    }
}
