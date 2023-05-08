using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    public Camera cam;
    public GameObject lBtn;
    public GameObject rBtn;
    private float upperBound = 1008.66f;
    private float lowerBound = 997.97f;
    private float currPos = 997.97f;


    private void Start()
    {
        lBtn.SetActive(false);
    }

    public void MoveToNewStation() {
        if (EventSystem.current.currentSelectedGameObject == rBtn && currPos < upperBound) { /* move right */
            currPos += 6;
            if (currPos > upperBound) {
                currPos = upperBound;
            }
            lBtn.SetActive(true);
        } else if (EventSystem.current.currentSelectedGameObject == lBtn && currPos > lowerBound) { /* move left */
            currPos -= 5;
            if (currPos < lowerBound){
                currPos = lowerBound;
            }
            rBtn.SetActive(true);
        }

        cam.transform.position = new Vector3(currPos, cam.transform.position.y, cam.transform.position.z);

        /* disable arrows when you reach the end */
        if (currPos == upperBound)
        {
            rBtn.SetActive(false);
            lBtn.SetActive(true);
        }
        else if (currPos == lowerBound)
        {
            lBtn.SetActive(false);
            rBtn.SetActive(true);

        }
    }
}

