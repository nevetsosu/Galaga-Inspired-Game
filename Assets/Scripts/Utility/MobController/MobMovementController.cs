using UnityEngine;

public class MobMovementController : MobController
{
    // movement controller can be added to in the future, but for now it doesn't provide much
    public void MoveTo(Vector3 position) {
        gameObject.transform.position = position; 
    }
}