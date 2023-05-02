using UnityEngine;

public class MobMovementController : MobController
{
    void MoveTo(Vector3 position) {
        gameObject.transform.position = position; 
    }
}