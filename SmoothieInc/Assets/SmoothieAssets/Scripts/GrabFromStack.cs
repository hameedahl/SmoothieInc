using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrabFromStack : MonoBehaviour
{
    private bool isMoving;
    private float startPosX;
    private float startPosY;
    public GameObject objToGrab;
    private GameObject singleObj;

    private Vector3 resetPos;
    
    // Start is called before the first frame update
    void Start() {
        // resetPos = singleObj.transform.localPosition; /* get original pos of object */
    }

    void Update() {
        if (isMoving) {
            Vector3 mousePos = MousePosition();
            /* move object on drag; update position */
            singleObj.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, singleObj.gameObject.transform.localPosition.z);
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("in");
            singleObj = Instantiate(objToGrab); /* create new object from stack */
            Vector3 mousePos = MousePosition();
            /* objects spawn where mouse is */
         

            /* get mouse positions and move object */
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
            // canvasGroup.alpha = .6f; /* make transparent while drag */
            isMoving = true;
            // blender.pour(singleObj);
        }
    }

    private void OnMouseUp() {
        isMoving = false;
    }

    private Vector3 MousePosition() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); /* align with camera */
        return mousePos;
    }
}
