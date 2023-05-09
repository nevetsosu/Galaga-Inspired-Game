using UnityEngine; 
using System.Threading.Tasks;

public class LimitedAttackHandler : MonoBehaviour
{
    [SerializeField] public int delayCoolDown = 0; 
    [SerializeField] protected bool attackLock = false; 
    [SerializeField] protected int burstDelay = 100;
    protected AttackController AC;
    void Awake() {
        if (!gameObject.TryGetComponent<AttackController>(out AC)) {
            Debug.LogWarning("Cannot attack without attack controller.");
            Destroy(this);
        }
    }

    public void TrySingle() {
        if (!attackLock) { 
            AC.shootProjectile();
            attackLock = true; 
            StartCoroutine("awaitCoolDownReset"); 
        }
    }

    public async void TryBurst() {
        if (!attackLock) { 
            attackLock = true;
            if (AC) AC.shootProjectile();

            await Task.Delay(burstDelay);

            if (AC) AC.shootProjectile();

            await Task.Delay(burstDelay);

            if (AC) AC.shootProjectile();

            if (AC) StartCoroutine("awaitCoolDownReset"); 
        }
    }

    async void awaitCoolDownReset() {
        await Task.Delay(delayCoolDown);
        attackLock = false;
    }
}