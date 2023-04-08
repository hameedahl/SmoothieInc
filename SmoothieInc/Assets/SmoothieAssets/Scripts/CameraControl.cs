using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]  private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void MoveToNewStation(Transform newStation, Cup cup) {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z),
        ref velocity, speed);
        currentPosX = newStation.position.x;
        cup.transform.position = new Vector3(17.59f, -1.45f, 0);
    }
}

