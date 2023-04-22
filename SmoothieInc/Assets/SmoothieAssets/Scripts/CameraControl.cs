using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]  private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
   
    public void MoveToNewStation(Transform newStation, GameObject tray) {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z),
        ref velocity, speed);
        currentPosX = newStation.position.x;
        tray.transform.position = new Vector3(1015.6f, -3f, 0);
        //this.fieldOfView = 30;
    }
}

