using UnityEngine;

public class MobMovementController : MobController
{
    public void MoveTo(Vector3 position) {
        gameObject.transform.position = position; 
    }
}