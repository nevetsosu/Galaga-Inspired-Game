using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    protected HealthController HC;
    [SerializeField] protected TextMeshProUGUI TM; // reference to text that displays health
    [SerializeField] protected GameObject subject; // the object whose health data will be tracked and displayed 

    void Awake() {
        // default subject is player
         if (!subject) {
            if (PlayerHandler.Instance) {
                subject = PlayerHandler.Instance.gameObject;
            }
            else {
                Debug.LogWarning("no subject and no player instance");
                this.enabled = false;
            }
        }
        
        // error checks
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
        // display default Health
        TM.text = HC.Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // while subject is alive, update health
        if (subject) TM.text = HC.Health.ToString();
        else this.enabled = false;   // stop updating health display upon object dealth
    }
}
