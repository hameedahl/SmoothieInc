using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public bool arrived = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Destination")
        {
            arrived = true;
        }
    }
}
