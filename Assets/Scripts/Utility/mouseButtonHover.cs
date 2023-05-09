using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseButtonHover : MonoBehaviour
{
    void OnMouseEnter() {
        AudioManager.Instance.Play("menuHover");
    }
}
