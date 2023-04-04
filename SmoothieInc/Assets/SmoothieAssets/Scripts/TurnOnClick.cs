using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TurnOnClick : MonoBehaviour
{

    private float startPosX;
    private float startPosY;

    // private Vector3 resetPos;
    // Start is called before the first frame update
    void Start()
    {
        // this.transform.eulerAngles = Vector3.forward / 90;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = MousePosition();
            /* get mouse positions and move object */
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
            // canvasGroup.alpha = .6f; /* make transparent while drag */
            this.transform.eulerAngles = Vector3.forward / 90;
        }
    }

    private void OnMouseUp() {
        this.transform.eulerAngles = Vector3.forward * 90;
    }

    private Vector3 MousePosition() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); /* align with camera */
        return mousePos;
    }
}
