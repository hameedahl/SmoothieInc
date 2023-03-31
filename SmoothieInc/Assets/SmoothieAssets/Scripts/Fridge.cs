using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fridge : MonoBehaviour
{
    Animator anim;
    private SpriteRenderer sprite;

    void Start() {
        anim = gameObject.GetComponentInChildren<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    /* toggle fridge open and close on click */
    private void OnMouseDown() {
        if (anim.GetBool("closed")) {
            anim.SetBool("opened", true);
            // sprite.sortingOrder -= 2;
            anim.SetBool("closed", false);
        } else {
            anim.SetBool("closed", true);
            // sprite.sortingOrder += 2;
            anim.SetBool("opened", false);
        }
    }
}
