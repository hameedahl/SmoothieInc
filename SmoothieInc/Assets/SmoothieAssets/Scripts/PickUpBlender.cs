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
    private GameObject pourSlot;

    private Animator anim;
    public Camera SmoothieCam;

    void Start() {
        resetPos = this.transform.localPosition; /* get original pos of object */
        anim = this.GetComponent<Animator>();
    }

    void Update() {
        if (GameObject.FindGameObjectWithTag("Smoothie-Camera")) {
            SmoothieCam = GameObject.FindGameObjectWithTag("Smoothie-Camera").GetComponent<Camera>();
        }

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
        if (isPouring) {
            this.transform.eulerAngles = Vector3.forward / 90;
            anim.Play("Idle-Dirty-Top");
            isEmpty = true;
            isBlended = false;
            isPouring = false;
            this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        }
    }

    private void pour() {
        if (GameObject.FindGameObjectWithTag("Cup"))
        {
            cup = GameObject.FindGameObjectWithTag("Cup").GetComponent<Cup>();

        }

        /* check if item is close to cup */
        Debug.Log(cup.transform.localPosition.x - this.transform.localPosition.x);
        Debug.Log(cup.transform.localPosition.y - this.transform.localPosition.y);
        if (cup && cup.isEmpty && !isEmpty && isBlended &&
            Mathf.Abs(cup.transform.localPosition.x - this.transform.localPosition.x) <= 10.5f &&
            Mathf.Abs(cup.transform.localPosition.x - this.transform.localPosition.x) >= 8.2f &&
            Mathf.Abs(cup.transform.localPosition.y - this.transform.localPosition.y) <= 2f &&
            Mathf.Abs(cup.transform.localPosition.y - this.transform.localPosition.y) >= .4f) {
                 //pourSlot = cup.transform.GetChild(0).gameObject;
               // this.transform.position = new Vector3(pourSlot.transform.position.x, pourSlot.transform.position.y, pourSlot.transform.position.z);
                this.transform.eulerAngles = Vector3.forward * 90;
                cup.fillCup();
                isPouring = true;
        }
    }

    private Vector3 MousePosition() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = SmoothieCam.ScreenToWorldPoint(mousePos); /* align with camera */
        return mousePos;
    }
}
