using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class Type2Enemy : Enemy
{
    private bool OpenFire;
    private Rigidbody2D RigidBody;
    private SplineAnimate Animator;
    [SerializeField] List<Action> Actions;

    void Awake() {
        // initilize default values
        health = 25;
        collision_damage = 5;
        invulnerable = false;

        RigidBody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<SplineAnimate>();

        Animator.AnimationMethod = SplineAnimate.Method.Speed; 

        Actions = new List<Action>(); 

        foreach (Action a in transform.GetChild(0).gameObject.GetComponentsInChildren<Action>()) {
           Actions.Add(a); 
        }
    }

    void Start() {
        execute();
    }

    void Update() {

        if (OpenFire) {
            attack();
        }
        // check aliveness
        if (health < 1) die(); 
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
        Destroy(transform.parent.gameObject);
    }

    async void execute() {
        if(Actions.Count > 0) {
            // The first animation is triggered by the "Play on the Wake" before the Animator is first enabled
            Animator.Container = Actions[0].splineContainer;
            Animator.MaxSpeed = Actions[0].Speed;

            Debug.Log(Animator.Duration * 1000 + " vs " + Actions[0].duration);
            if (Animator.Duration * 1000  > Actions[0].duration) Actions[0].duration = Mathf.CeilToInt(Animator.Duration * 1000);
            Debug.Log("Now " + Animator.Duration * 1000 + " vs " + Actions[0].duration);

            Animator.enabled = true;
            await Task.Delay(Actions[0].duration);
            
            // All subsequent animations are done the same way
            for (int i = 1 ; i < Actions.Count; i++) {

                // SplineAnimate setup 
                Animator.Container = Actions[i].splineContainer;
                Animator.MaxSpeed = Actions[i].Speed;

                if (Animator.Duration * 1000 > Actions[i].duration) Actions[i].duration = Mathf.CeilToInt(Animator.Duration * 1000);

                Animator.Restart(true);

                // Debug.Log("Using Path: " + Actions[i].splineContainer.name + " Speed: " + Actions[i].Speed + " OpenFire: " + Actions[i].OpenFire + " Duration: " + Actions[i].duration);

                await Task.Delay(Actions[i].duration);
            }
        }

    }
}
