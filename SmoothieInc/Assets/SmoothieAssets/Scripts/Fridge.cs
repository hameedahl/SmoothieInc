using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fridge : MonoBehaviour
{
    Animator anim;
    void Start() {
        anim = gameObject.GetComponentInChildren<Animator>();
       
    }

    /* toggle fridge open and close on click */
    private void OnMouseDown() {
        if (anim.GetBool("closed")) {
            anim.SetBool("opened", true);
            anim.SetBool("closed", false);
        } else {
            anim.SetBool("closed", true);
            anim.SetBool("opened", false);
        }
    }
}
