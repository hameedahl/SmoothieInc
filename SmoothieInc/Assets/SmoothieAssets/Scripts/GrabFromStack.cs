using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrabFromStack : MonoBehaviour
{
    //    private bool isMoving;
    //    private float startPosX;
    //    private float startPosY;
    //    public GameObject objToGrab;
    //    private GameObject singleObj;

    //    private Vector3 resetPos;
    //    GameObject blender;
    //    BlenderSlot blenderTop;
    //    bool addedToSlot = false;
    //    public Camera SmoothieCam;

    //    void Start() {
    //        blender = GameObject.FindGameObjectWithTag("Blender");
    //        blenderTop = GameObject.FindGameObjectWithTag("Blender-Top").GetComponent<BlenderSlot>();
    //    }

    //void Update() {
    //    //if (GameObject.FindGameObjectWithTag("Smoothie-Camera")) {
    //    //    SmoothieCam = GameObject.FindGameObjectWithTag("Smoothie-Camera").GetComponent<Camera>();
    //    //}

    //    if (isMoving) {
    //        Vector3 mousePos = MousePosition();
    //        /* move object on drag; update position */
    //        singleObj.gameObject.transform.position = MousePosition();
    //        // new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, singleObj.gameObject.transform.localPosition.z);
    //    }
    //}

    //private void OnMouseDown() {
    //    if (Input.GetMouseButtonDown(0)) {

    //       // singleObj = Instantiate(objToGrab, transform);

    //        //singleObj.GetComponent<Food>().SmoothieCam = SmoothieCam;

    //        Vector3 mousePos = MousePosition();
    //        /* get mouse positions and move object */
    //        startPosX = mousePos.x - this.transform.localPosition.x;
    //        startPosY = mousePos.y - this.transform.localPosition.y;
    //        singleObj = Instantiate(objToGrab, MousePosition(), Quaternion.identity); /* create new object from stack */
    //        singleObj.GetComponent<DragDrop>().SmoothieCam = SmoothieCam;
    //        // this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);

    //        /* make transparent while drag */
    //        isMoving = true;
    //    }
    //}

    //private void OnMouseUp() {
    //    isMoving = false;
    //    /* insert item into available slot if close to blender */
    //    if (singleObj && singleObj.tag != "Cover" && singleObj.tag != "Cup" && singleObj.tag != "Straw") {
    //        addedToSlot = blenderTop.addedToSlot(singleObj);
    //    }
    //    // this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);


    //    // if (!addedToSlot) {
    //    //     /* reset to starting position */
    //    //     Destroy(singleObj);
    //    // }
    //}

    //private Vector3 MousePosition() {
    //    Vector3 mousePos;
    //    mousePos = Input.mousePosition;
    //    mousePos = SmoothieCam.ScreenToWorldPoint(mousePos); /* align with camera */
    //    mousePos.z = 20f;
    //    return mousePos;
    //}
}
