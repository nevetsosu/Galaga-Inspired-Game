using UnityEngine;

public class MobLookController : MobController
{

    // sets the desired look-direction.
    public void lookToward(Vector3 direction) {
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
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