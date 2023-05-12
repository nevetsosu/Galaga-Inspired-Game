using UnityEngine;

public class DespawnHandler : MonoBehaviour
{
    [SerializeField] protected GameObject ReportTo; // the game object that should also be informed of this object's death
    [SerializeField] protected int reportCount = 1; // how many reports a game object should receive before dying
    protected DespawnHandler ReportToDH;

    void Awake() {
        if (ReportTo && !ReportTo.TryGetComponent<DespawnHandler>(out ReportToDH)) { // make sure the reportTo also has a despawn handler
            ReportToDH = ReportTo.AddComponent<DespawnHandler>(); 
        }
    }

    public void Despawn() {
        Destroy(gameObject);
    }

    // notify the reportTo object by calling its reportRecieve
    private void reportSend() {
        ReportToDH.reportReceive();
    }

    public virtual void reportReceive() {
        if (ReportTo) reportSend(); // report to reportTo if there is one

        if (--reportCount <= 0) Despawn(); // check if this object should despawn
    }
}