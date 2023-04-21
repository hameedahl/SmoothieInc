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
    private Food foodInfo;

    /* FoodStack variables */
    public GameObject objToGrab;
    private GameObject singleObj;

    public Texture2D cursorHand;
    public Texture2D cursorGrab;

    // Start is called before the first frame update
    void Start() {
        resetPos = this.transform.localPosition; /* get original pos of object */
        if (GameObject.FindGameObjectWithTag("Blender-Top"))  {
            blenderTop = GameObject.FindGameObjectWithTag("Blender-Top").GetComponent<BlenderSlot>();
        }
        //Vector2 cursorOffset = new Vector2(cursorHand.width / 2, cursorHand.height / 2);
        //Cursor.SetCursor(cursorHand, cursorOffset, CursorMode.ForceSoftware);
    }

    void Update() {
        if (isMoving) {
            Vector3 mousePos = MousePosition();
            Vector2 cursorOffset = new Vector2(cursorGrab.width / 2, cursorGrab.height / 2);
            Cursor.SetCursor(cursorGrab, cursorOffset, CursorMode.ForceSoftware);
            /* move object on drag; update position */
            if (this.tag == "FoodStack") {
                /* move object on drag; update position */
                singleObj.gameObject.transform.position = MousePosition();
            }
            else {
                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            }
        }
    }

    private void OnMouseEnter()
    {
        //Vector2 cursorOffset = new Vector2(cursorHand.width / 2, cursorHand.height / 2);
        //Cursor.SetCursor(cursorHand, cursorOffset, CursorMode.ForceSoftware);
    }
       

    private void OnMouseExit()
    {
        //Vector2 cursorOffset = new Vector2(cursorHand.width / 2, cursorHand.height / 2);
        //Cursor.SetCursor(null, cursorOffset, CursorMode.ForceSoftware);
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
         
            Vector3 mousePos = MousePosition();
            /* get mouse positions and move object */
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
            /* make transparent while drag */
            // this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);

            if (this.tag == "Food") {
                foodInfo = this.GetComponent<Food>();
                isFood = true;
                isFoodStack = false;
                pourLiquid(); /* check if liquid is pouring */
            } else if (this.tag == "FoodStack") {
                isFoodStack = true;
                isFood = false;
                singleObj = Instantiate(objToGrab, MousePosition(), Quaternion.identity); /* create new object from stack */
                singleObj.GetComponent<DragDrop>().SmoothieCam = SmoothieCam;
                foodInfo = singleObj.GetComponent<Food>();
            }
            isMoving = true;

        }
    }

    private void OnMouseUp() {
            //Vector2 cursorOffset = new Vector2(cursorHand.width / 2, cursorHand.height / 2);
            //Cursor.SetCursor(cursorHand, cursorOffset, CursorMode.ForceSoftware);
        
        isMoving = false;
        if (isFood) {
            if (!blenderTop.addedToSlot(this.gameObject)) {
               if (blenderTop.blenderIsFull) { /* returns false if not close enough or if filled up */
                    Destroy(gameObject);
                }


                /* reset to starting position if not inserted */
                //Destroy(gameObject);
                //this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
            }
        } else if (isFoodStack) {
            /* insert item into available slot if close to blender */
            if (singleObj && singleObj.tag != "Cover" && singleObj.tag != "Cup" && singleObj.tag != "Straw") {
                if (!blenderTop.addedToSlot(singleObj))
                {
                    if (blenderTop.blenderIsFull)
                    { /* returns false if not close enough or if filled up */
                        Destroy(singleObj);
                    }
                    /* reset to starting position if not inserted */
                    //Destroy(singleObj);
                    //this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
                }
            }
        }
        // this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    private Vector3 MousePosition() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = SmoothieCam.ScreenToWorldPoint(mousePos); /* align with camera */
        mousePos.z = 20f;
        return mousePos;
    }

    private void pourLiquid() {
        /* rotate sprite to OG position */
        if (foodInfo.isPouring) {
            this.transform.eulerAngles = Vector3.forward / 90;
            foodInfo.isPouring = false;
            this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        }
    }
}
