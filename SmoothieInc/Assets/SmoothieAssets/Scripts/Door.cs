using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour
{
    public GameObject open_door;
    public GameObject closed_door;
    public Texture2D cursorHand;
    public Texture2D cursorGrab;
    //public AudioManager audioMan;

    void Start() {
        //open_door.SetActive(false);
        //audioMan = gameObject.Find("Audio Manager").GetComponent<AudioManager>();
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

    /* toggle fridge open and close on click */
    private void OnMouseDown() {
        if (!EventSystem.current.IsPointerOverGameObject()) /* no clicking through canvas */
        {
            Vector2 cursorOffset = new Vector2(cursorGrab.width / 2, cursorGrab.height / 2);
            Cursor.SetCursor(cursorGrab, cursorOffset, CursorMode.ForceSoftware);
            if (open_door.activeSelf)
            {
                open_door.SetActive(false);
                closed_door.SetActive(true);
                //audioMan.Play("Fridge-Close");
            }
            else
            {
                closed_door.SetActive(false);
                open_door.SetActive(true);
                //audioMan.Play("Fridge-Open");
            }
        }
    }
}
