using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TurnOnClick : MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    public Camera SmoothieCam;

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            //Vector3 mousePos = MousePosition();
            ///* get mouse positions and move object */
            //startPosX = mousePos.x - this.transform.localPosition.x;
            //startPosY = mousePos.y - this.transform.localPosition.y;
            // canvasGroup.alpha = .6f; /* make transparent while drag */
            this.transform.eulerAngles = Vector3.forward * 90;
        }
    }

    private void OnMouseUp() {
        this.transform.eulerAngles = Vector3.forward / 90;
    }

    //private Vector3 MousePosition() {
    //    Vector3 mousePos;
    //    mousePos = Input.mousePosition;
    //    mousePos = SmoothieCam.ScreenToWorldPoint(mousePos); /* align with camera */
    //    mousePos.z = 20f;
    //    return mousePos;
    //}
}
