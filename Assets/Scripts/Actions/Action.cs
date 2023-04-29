using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[System.Serializable]
public class Action : MonoBehaviour
{
    [SerializeField] public float Speed;
    [SerializeField] public bool StandingOpenFire;
    [SerializeField] public bool GungHo;
    [SerializeField] public SplineContainer splineContainer;
    [SerializeField] public int duration;
}
