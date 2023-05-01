using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class Type2Enemy : Enemy
{
    private bool OpenFire;
    private bool TrackPlayer;
    private Rigidbody2D RigidBody;
    List<Action> Actions;

    void Awake() {
        // initilize default values
        health = 25;
        collision_damage = 5;
        invulnerable = false;
        OpenFire = false;
        TrackPlayer = false;

        Actions = new List<Action>(); 

        RigidBody = gameObject.GetComponent<Rigidbody2D>();

        foreach (Action a in transform.GetChild(0).gameObject.GetComponentsInChildren<Action>()) {
           Actions.Add(a); 
        }
    }

    void Start() {
        MobTrackObject FO = gameObject.AddComponent<MobTrackObject>();
        FO.setTarget(PlayerHandler.Instance.gameObject);
        FO.Resume();

        executeActions();
    }

    void OnTriggerEnter2D(Collider2D col) {

        // does collision damage
        if (col.gameObject.tag == "Player") {
            PlayerHandler.Instance.take_damage(collision_damage);
        }
    }

    public override void attack()
    {
        Debug.Log("ATTACK");
        return; 
    }

    public override void die() {
        Destroy(gameObject.transform.parent.gameObject);
    }

    async void executeActions() {
        foreach (Action a in Actions) {
            MobMoveDefinedPath MMDP = gameObject.AddComponent<MobMoveDefinedPath>();
            MMDP.setSpline(a.splineContainer);
            MMDP.Speed = a.Speed;

            // check gungho and set openfire HERE
            
            MMDP.Resume();
            while(!MMDP.isFinished()) {
                await Task.Yield();
            }

            // check if NOT standingFIRE and GUNGHO
                // disable GUNGHO
            // else ATTACH OPENFIRE

            // check the next spline and begin looking in its direction

            Destroy(MMDP);
        }
    }
}
