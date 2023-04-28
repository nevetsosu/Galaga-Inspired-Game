using System.Collections;
using System.Collections.Generic;
using UnityEngine.Splines;
using UnityEngine;

public class testSpline : MonoBehaviour
{
    SplineAnimate SA;
    void Start() {
        SA = gameObject.GetComponent<SplineAnimate>();
        SA.Play();
    
    }

    void Update() {
        if (SA.NormalizedTime > .5f) {
            Debug.Log("50 Percent There");
        }
    }


}
