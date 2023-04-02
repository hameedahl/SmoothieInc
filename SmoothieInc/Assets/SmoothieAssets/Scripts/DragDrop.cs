using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour
{
    private bool isMoving;
    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;
    
    // Start is called before the first frame update
    void Start() {
        resetPos = this.transform.localPosition; /* get original pos of object */
    }

    void Update() {
        if (isMoving) {
            Vector3 mousePos = MousePosition();
            /* move object on drag; update position */
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
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
            // blender.pour(this);
        }
    }

    private void OnMouseUp() {
        isMoving = false;

        // if (inBlender) {
        //     /* make spot in blender available */
        //     blender.isFull[this.slotNum] = false;
        // }
        // /* insert item into available slot if close to blender */
        // addedToSlot = blender.addedToSlot(this);
        
        // // canvasGroup.alpha = 1f;
        // if (!addedToSlot) {
        //     /* reset to starting position if not close to blender */
        //     this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        // }
    }

    private Vector3 MousePosition() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); /* align with camera */
        return mousePos;
    }
}
