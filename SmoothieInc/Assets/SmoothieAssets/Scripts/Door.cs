using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour
{
    public GameObject open_door;
    public GameObject closed_door;
    //public AudioManager audioMan;

    void Start() {
        //open_door.SetActive(false);
        //audioMan = gameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    /* toggle fridge open and close on click */
    private void OnMouseDown() {
        if (open_door.activeSelf) {
            open_door.SetActive(false);
            closed_door.SetActive(true);
            //audioMan.Play("Fridge-Close");
        } else {
            closed_door.SetActive(false);
            open_door.SetActive(true);
            //audioMan.Play("Fridge-Open");
        }
    }
}
