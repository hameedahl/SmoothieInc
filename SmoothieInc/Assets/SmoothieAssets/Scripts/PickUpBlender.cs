using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class PickUpBlender : MonoBehaviour
{
    private bool isMoving = false;
    public bool isEmpty = true;
    public bool isBlended = false;
    public bool isPouring = false;
    public bool hasIce = false;
    public bool isDetached = false;
    public bool foundCup = false;

    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;

    private Cup cup;
    private GameObject pourSlot;

    private Animator anim;
    public Camera SmoothieCam;

    public Texture2D cursorHand;
    public Texture2D cursorGrab;
    public GameHandler gameHandler;



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
            isDetached = true;
        }
    }

    private void OnMouseUp() {
        Vector2 cursorOffset = new Vector2(cursorHand.width / 2, cursorHand.height / 2);
        Cursor.SetCursor(cursorHand, cursorOffset, CursorMode.ForceSoftware);
        isMoving = false;
        if (isPouring) {
            this.transform.eulerAngles = Vector3.forward / 90;
            anim.Play("Idle-Dirty-Top");
            resetTop();
            this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        }
    }

    private void pour() {
        GameObject cupL = GameObject.FindGameObjectWithTag("LCup");
        if (cupL && !foundCup)
        {
            filledCup(cupL);
        }
        GameObject cupM = GameObject.FindGameObjectWithTag("MCup");
        if (cupM && !foundCup)
        {
            filledCup(cupM);
        }
        GameObject cupS = GameObject.FindGameObjectWithTag("SCup");
        if (cupS && !foundCup)
        {
            filledCup(cupS);
        }

        foundCup = false;
    }

    public void filledCup(GameObject cupGo)
    {
        cup = cupGo.GetComponent<Cup>();

        if (cup && cup.isEmpty && !isEmpty && isBlended &&
            Mathf.Abs(cup.transform.localPosition.x - this.transform.localPosition.x) >= 1000f &&
            Mathf.Abs(cup.transform.localPosition.x - this.transform.localPosition.x) <= 1003f &&
            Mathf.Abs(cup.transform.localPosition.y - this.transform.localPosition.y) >= .5f &&
            Mathf.Abs(cup.transform.localPosition.y - this.transform.localPosition.y) <= 4f)
        {
            this.transform.position = new Vector3(cup.pourSlot.transform.position.x, cup.pourSlot.transform.position.y, cup.pourSlot.transform.position.z);
            this.transform.eulerAngles = Vector3.forward * 90;
            cup.fillCup();
            isPouring = true;
            foundCup = true;
        }
    }

    private Vector3 MousePosition() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = SmoothieCam.ScreenToWorldPoint(mousePos); /* align with camera */
        return mousePos;
    }

    public void resetTop() {
        isEmpty = true;
        isBlended = false;
        isPouring = false;
        hasIce = false;
        isDetached = false;
        foundCup = false;
    }
}
