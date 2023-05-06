using UnityEngine;

public class DespawnHandler : MonoBehaviour
{
    [SerializeField] private GameObject ReportTo = null;
    private DespawnHandler ReportToDH;

    void Awake() {
        if (ReportTo == null) {
            ReportTo = gameObject;
            ReportToDH = this;
        } else if (!ReportTo.TryGetComponent<DespawnHandler>(out ReportToDH)) {
            ReportToDH = ReportTo.AddComponent<DespawnHandler>(); 
        }
    }

    public void Despawn() {
        Destroy(gameObject);
    }

    private void reportSend() {
        ReportToDH.reportReceive();
    }

    public virtual void reportReceive() {
        if (ReportTo != gameObject) reportSend();
        Despawn(); 
    }
}