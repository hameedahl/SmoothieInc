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
    public bool hasIce = false;

    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;

    private Cup cup;
    private GameObject pourSlot;

    private Animator anim;
    public Camera SmoothieCam;

    public Texture2D cursorHand;
    public Texture2D cursorGrab;

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
            Vector2 cursorOffset = new Vector2(cursorGrab.width / 2, cursorGrab.height / 2);
            Cursor.SetCursor(cursorGrab, cursorOffset, CursorMode.ForceSoftware);
            /* move object on drag; update position */
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            pour();
        }
    }

    private void OnMouseEnter()
    {
        Vector2 cursorOffset = new Vector2(cursorHand.width / 2, cursorHand.height / 2);
        Cursor.SetCursor(cursorHand, cursorOffset, CursorMode.ForceSoftware);
    }


    private void OnMouseExit()
    {
        Vector2 cursorOffset = new Vector2(cursorHand.width / 2, cursorHand.height / 2);
        Cursor.SetCursor(null, cursorOffset, CursorMode.ForceSoftware);
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Vector3 mousePos = MousePosition();
            /* get mouse positions and move object */
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
            // canvasGroup.alpha = .6f; /* make transparent while drag */
            isMoving = true;
        }
    }

    private void OnMouseUp() {
        Vector2 cursorOffset = new Vector2(cursorHand.width / 2, cursorHand.height / 2);
        Cursor.SetCursor(cursorHand, cursorOffset, CursorMode.ForceSoftware);
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
        //Debug.Log(cup.transform.localPosition.x - this.transform.localPosition.x);
        //Debug.Log(cup.transform.localPosition.y - this.transform.localPosition.y);
        if (cup && cup.isEmpty && !isEmpty && isBlended &&
            Mathf.Abs(cup.transform.localPosition.x - this.transform.localPosition.x) >= 1000f &&
            Mathf.Abs(cup.transform.localPosition.x - this.transform.localPosition.x) <= 1003f &&
            Mathf.Abs(cup.transform.localPosition.y - this.transform.localPosition.y) >= .5f &&
            Mathf.Abs(cup.transform.localPosition.y - this.transform.localPosition.y) <= 4f) {
                 //pourSlot = cup.transform.GetChild(0).gameObject;
                this.transform.position = new Vector3(cup.pourSlot.transform.position.x, cup.pourSlot.transform.position.y, cup.pourSlot.transform.position.z);
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
