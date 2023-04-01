using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpBlender : MonoBehaviour
{
    private bool isMoving = false;
    public bool isEmpty = true;
    public bool isBlended = false;
    public bool isPouring = false;


    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;

    private Cup cup;
    private Animator anim;

    void Start() {
        cup = GameObject.FindGameObjectsWithTag("Cup")[0].GetComponent<Cup>();
        resetPos = this.transform.localPosition; /* get original pos of object */
        anim = this.GetComponent<Animator>();
    }

    void Update() {
        if (!isEmpty && isBlended && isMoving) {
            Vector3 mousePos = MousePosition();
            /* move object on drag; update position */
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            pour();
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = MousePosition();
            /* get mouse positions and move object */
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
            // canvasGroup.alpha = .6f; /* make transparent while drag */
            isMoving = true;
        }
    }

    private void OnMouseUp() {
        isMoving = false;
        /* reset to starting position if not close to blender */
        if (!isPouring) {
        this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);

        }

        // if (isPouring) {
        //     /* make spot in blender available */
        //     // run pouring animation?

        // }
        // /* insert item into available slot if close to blender */
        // addedToSlot = blender.addedToSlot(this);
        
        // canvasGroup.alpha = 1f;
        // if (!addedToSlot) {

        // }
    }

    private void pour() {
        /* check if item is close to blender */
        if (!isEmpty && isBlended && Mathf.Abs(cup.slot.transform.localPosition.x - this.transform.localPosition.x) <= .9f &&
            Mathf.Abs(cup.slot.transform.localPosition.y - this.transform.localPosition.y) <= 2.3f) {
                /* play pour anim? */
                this.transform.position = new Vector3(cup.slot.transform.position.x, cup.slot.transform.position.y, cup.slot.transform.position.z);
                this.transform.eulerAngles = Vector3.forward * 90;
                /* play cup filling animation */
                /* wait a few to put top back up */
                // item.transform.eulerAngles = Vector3.forward / 90;
                /* make dirty */
                anim.Play("Idle-Dirty-Top");
                isPouring = true;

                //this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        }
    }

    private Vector3 MousePosition() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); /* align with camera */
        return mousePos;
    }
}
