using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

public class Type2Enemy : Enemy
{
    List<Action> Actions;

    void Awake() {
        // initilize default values
        health = 25;
        collision_damage = 5;
        invulnerable = false;   

        Actions = new List<Action>(); 

        foreach (Action a in transform.GetChild(0).gameObject.GetComponentsInChildren<Action>()) {
            Actions.Add(a); 
        }

        gameObject.AddComponent<MobLook>(); 
    }

    void Start() {
        executeActions();
    }

    void OnTriggerEnter2D(Collider2D col) {

        // does collision damage
        if (col.gameObject.tag == "Player") {
            PlayerHandler.Instance.take_damage(collision_damage);
        }
    }
    public override void die() {
        Destroy(gameObject.transform.parent.gameObject);
    }

    async void executeActions() {
        foreach (Action a in Actions) {
            a.Execute(gameObject); 

            while (!a.TaskDone) {
                await Task.Yield(); 
            }
        }   
    }
}
