using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFinder : MonoBehaviour
{
    public Transform destinationList;

    // Start is called before the first frame update
    void Start()
    {
        destinationList = GameObject.Find("DestinationList").transform;
    }

    public GameObject Find(int difficulty)
    {
        List<GameObject> maxDif = transform.GetChild(difficulty).gameObject.GetComponent<TouchingDestinations>().destinations;
        List<GameObject> minDif = transform.GetChild(difficulty - 1).gameObject.GetComponent<TouchingDestinations>().destinations;

        GameObject worstCaseObj = destinationList.GetChild(0).gameObject;
        int worstCaseDif = -1;

        for(int i = 0; i < maxDif.Count; i++)
        {
            Debug.Log(maxDif[i]);
            if(!minDif.Contains(maxDif[i]))
            {
                return maxDif[i];
            }
            else
            {
                for(int j = difficulty - 1; j >= 0; j--)
                {
                    if(!transform.GetChild(j).gameObject.GetComponent<TouchingDestinations>().destinations.Contains(maxDif[i]))
                    {
                        worstCaseDif = j;
                        worstCaseObj = maxDif[i];
                        break;
                    }
                }
            }
        }
        return worstCaseObj;
    }
}
