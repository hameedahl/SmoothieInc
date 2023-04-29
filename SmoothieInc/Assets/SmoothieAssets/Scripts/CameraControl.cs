using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    private bool inStation0 = true;
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
        if (EventSystem.current.currentSelectedGameObject == rBtn && currPos < upperBound) {
            currPos += 6;
            if (currPos > upperBound) {
                currPos = upperBound;
            }
            lBtn.SetActive(true);
        } else if (EventSystem.current.currentSelectedGameObject == lBtn && currPos > lowerBound) {
            currPos -= 5;
            if (currPos < lowerBound){
                currPos = lowerBound;
            }
        }

        cam.transform.position = new Vector3(currPos, cam.transform.position.y, cam.transform.position.z);


        //if (inStation0) {
        //    //tray.transform.position = new Vector3(1015.6f, -2.49f, 0);
        //    inStation0 = false;
        //    lBtn.SetActive(true);
        //    rBtn.SetActive(false);
        //} else {
        //    //tray.transform.position = new Vector3(997.97f, -.08f, -10);
        //    cam.transform.position = new Vector3(997.97f, cam.transform.position.y, cam.transform.position.z);
        //    inStation0 = true;
        //    lBtn.SetActive(false);
        //    rBtn.SetActive(true);
        //}
    }
}

