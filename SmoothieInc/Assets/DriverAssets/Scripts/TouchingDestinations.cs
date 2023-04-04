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

    public List<GameObject> GetDestinations()
    {
        int shift = Random.Range(0,destinations.Count - 1);
        for(int i = 0; i < shift; i++)
        {
            GameObject temp = destinations[0];
            for(int j = 1; j < destinations.Count; j++)
            {
                destinations[j - 1] = destinations[j];
            }
            destinations[destinations.Count - 1] = temp;
        }
        return destinations;
    }
}
