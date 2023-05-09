using UnityEngine;

public class MobLookController : MobController
{

    // sets the desired look-direction.
    public void lookToward(Vector3 direction) {
        lookToward(Quaternion.LookRotation(Vector3.forward, direction));
    }

    public void lookToward(Quaternion rot) {
        gameObject.transform.rotation = rot;
    }

    // sets the desired look-toward position
    public void lookAt(Vector3 position) {
        Vector3 current_pos = gameObject.transform.position;
        lookToward(position - current_pos);
    }

    public void lookAt(GameObject target) {
        if (target) lookAt(target.transform.position);
    }

    public void incrementToward(Vector3 direction, int incrementAngle) {
        incrementToward(Quaternion.LookRotation(Vector3.forward, direction), incrementAngle);
    }

    public void incrementToward(Quaternion rot, int incrementAngle) {
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rot, incrementAngle);
    }

    public void incrementToward(GameObject target, int incrementAngle) {
        if (target) incrementToward(target.transform.position - gameObject.transform.position, incrementAngle);
    }
}