using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Food : MonoBehaviour
{
    //public  bool isMoving;

    //private bool addedToSlot;
    //private bool isInserted;

    //private float startPosX;
    //private float startPosY;

    //private Vector3 resetPos;

    public  int slotNum;
    public  int id;
    public  string category;
    public bool isPouring = false;
    public  bool inBlender;


    //public Camera SmoothieCam;


    // BlenderSlot blenderTop;

    //void Start() {
    //    //resetPos = this.transform.localPosition; /* get original pos of object */
    //    //if (GameObject.FindGameObjectWithTag("Blender-Top")) {
    //    //    blenderTop = GameObject.FindGameObjectWithTag("Blender-Top").GetComponent<BlenderSlot>();
    //    //}
    //}

    //void Update() {
    //    //if (GameObject.FindGameObjectWithTag("Smoothie-Camera")) {
    //    //    SmoothieCam = GameObject.FindGameObjectWithTag("Smoothie-Camera").GetComponent<Camera>();
    //    //}

    //    if (isMoving) {
    //        Vector3 mousePos = MousePosition();
    //        /* move object on drag; update position */
    //        this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
    //    }
    //}

    //private void OnMouseDown() {
    //    if (Input.GetMouseButtonDown(0)) {
    //        Vector3 mousePos = MousePosition();
    //        /* get mouse positions and move object */
    //        startPosX = mousePos.x - this.transform.localPosition.x;
    //        startPosY = mousePos.y - this.transform.localPosition.y;
    //    // this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

    //        //this.GetComponent<SpriteRenderer>().sprite.alpha = .6f; /* make transparent while drag */
    //        isMoving = true;
    //        if (isPouring) {
    //            this.transform.eulerAngles = Vector3.forward / 90;
    //            isPouring = false;
    //            this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
    //        }
    //    }
    //}

    //private void OnMouseUp() {
    //    isMoving = false;
    //    /* insert item into available slot if close to blender */
    //    addedToSlot = blenderTop.addedToSlot(this.gameObject);


    //    // this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    //    // if (!addedToSlot) {
    //    //     Destroy(this);
    //    //     /* reset to starting position if not close to blender */
    //    //     // this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
    //    // }
    //}

    //private Vector3 MousePosition() {
    //    Vector3 mousePos;
    //    mousePos = Input.mousePosition;
    //    mousePos = SmoothieCam.ScreenToWorldPoint(mousePos); /* align with camera */
    //    return mousePos;
    //}
}