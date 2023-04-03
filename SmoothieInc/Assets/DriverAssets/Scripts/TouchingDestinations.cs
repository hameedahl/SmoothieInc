using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDestinations : MonoBehaviour
{
    public List<GameObject> destinations;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Destination")
        {
            destinations.Add(col.transform.parent.gameObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Destination")
        {
            destinations.Remove(col.transform.parent.gameObject);
        }
    }
}
