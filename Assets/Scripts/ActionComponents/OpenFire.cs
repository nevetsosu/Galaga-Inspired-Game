using UnityEngine;
using System.Threading.Tasks; 

public class OpenFire : MonoBehaviour 
{
    [SerializeField] private bool firing = false;
    [SerializeField] private int fireRateDelay = 1000;
    [SerializeField] private bool CoolDown;
    private Mob M; 

    public bool Firing 
    {
        get { return firing; }
    }

    public int FireRateDelay 
    {
        get { return fireRateDelay; }
        set { fireRateDelay = value; }
    }

    void Awake() { // for some reason this spawns in disabled by default
        this.enabled = true; 
    }

    void Start() {
        M = gameObject.GetComponent<Mob>();
        if (M == null) {
            Debug.Log("NO MOB SCRIPT ATTACHED, removing OPENFIRE");
            Destroy(this);
        }
    }

    void Update() {
        if (firing && !CoolDown) {
            M.attack();
            CoolDown = true;
            StartCoroutine("Cooldown");
        }
    }

    public void openFire() {
        firing = true; 
    } 

    public void stopFiring() {
        firing = false;
    }

    public void setFireRateDelay(int delay) {
        fireRateDelay = delay; 
    }

    async void Cooldown() { 
        await Task.Delay(fireRateDelay);
        CoolDown = false;
    }

    public bool toggleOpenFire() {
        firing = !firing;
        return firing;
    }
}