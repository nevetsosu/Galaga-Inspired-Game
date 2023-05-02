using UnityEngine;

// Component that will make the gameObject face toward a certain direction or position.
// Component has a set turning speed
public class MobLook : MonoBehaviour
{
    [SerializeField] private int increment_angle; 

    private Quaternion rot_goal;

    // default looks toward the player
    void Awake() {
        increment_angle = 1;  
        if (PlayerHandler.Instance != null) {
            lookAt(PlayerHandler.Instance.gameObject);
        }
    }

    void Update() {
        // Increment current rotation toward ideal rotation
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rot_goal, increment_angle);
    }

    // sets the desired look-direction.
    public void lookToward(Vector3 direction) {
        rot_goal = Quaternion.LookRotation(Vector3.forward, direction);
    }

    // sets the desired look-toward position
    public void lookAt(Vector3 position) {
        Vector3 current_pos = gameObject.transform.position;
        lookToward(position - current_pos);
    }

    public void lookAt(GameObject obj) {
        lookAt(obj.transform.position);
    }
}
