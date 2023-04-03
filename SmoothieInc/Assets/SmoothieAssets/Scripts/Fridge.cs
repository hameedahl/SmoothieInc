using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fridge : MonoBehaviour
{
    public GameObject open_door;
    public GameObject closed_door;

    void Start() {
        open_door.SetActive(false);
    }

    /* toggle fridge open and close on click */
    private void OnMouseDown() {
        if (open_door.activeSelf) {
            open_door.SetActive(false);
            closed_door.SetActive(true);
        } else {
            closed_door.SetActive(false);
            open_door.SetActive(true);
        }
    }

}
