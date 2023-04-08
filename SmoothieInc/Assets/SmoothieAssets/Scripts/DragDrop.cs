using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour
{
    private bool isMoving;
    private float startPosX;
    private float startPosY;
    public Camera SmoothieCam;
    private Vector3 resetPos;

    /* Food variables */
    private bool isFood = false;
    private bool isFoodStack = false;
    private BlenderSlot blenderTop;
    private Food food;

    /* FoodStack variables */
    public GameObject objToGrab;
    private GameObject singleObj;

    // Start is called before the first frame update
    void Start() {
        resetPos = this.transform.localPosition; /* get original pos of object */
        if (GameObject.FindGameObjectWithTag("Blender-Top"))  {
            blenderTop = GameObject.FindGameObjectWithTag("Blender-Top").GetComponent<BlenderSlot>();
        }
    }

    void Update() {
        if (isMoving && this.tag != "FoodStack") {
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
            /* make transparent while drag */
            // this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            isMoving = true;

            if (this.tag == "Food") {
                food = this.GetComponent<Food>();
                isFood = true;
                pourLiquid(); /* check if liquid is pouring */
            } else if (this.tag == "FoodStack") {
                singleObj = Instantiate(objToGrab, MousePosition(), Quaternion.identity); /* create new object from stack */
                singleObj.GetComponent<DragDrop>().SmoothieCam = SmoothieCam;
                food = singleObj.GetComponent<Food>();
            }
        }
    }

    private void OnMouseUp() {
        isMoving = false;

        if (isFood) {
            
            if (!blenderTop.addedToSlot(this.gameObject)) {
                /* reset to starting position if not inserted */
                Destroy(food);
                //this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
            }
        } else if (isFoodStack) {
            /* insert item into available slot if close to blender */
            if (singleObj && singleObj.tag != "Cover" && singleObj.tag != "Cup" && singleObj.tag != "Straw") {
                if (!blenderTop.addedToSlot(this.singleObj))
                {
                    /* reset to starting position if not inserted */
                    Destroy(food);
                    //this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
                }
            }
        }
        // this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    private Vector3 MousePosition() {
        //Vector3 mousePos;
        //mousePos = Input.mousePosition;
        //mousePos = SmoothieCam.ScreenToWorldPoint(mousePos); /* align with camera */
        //return mousePos;
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = SmoothieCam.ScreenToWorldPoint(mousePos); /* align with camera */
        mousePos.z = 20f;
        return mousePos;
    }

    private void pourLiquid() {
        /* rotate sprite to OG position */
        if (food.isPouring) {
            this.transform.eulerAngles = Vector3.forward / 90;
            food.isPouring = false;
            this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        }
    }
}
