using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFinder : MonoBehaviour
{
    public Transform destinationList;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        destinationList = GameObject.Find("DestinationList").transform;
    }

    void Update()
    {
        transform.position = player.position;
    }

    public GameObject Find(int difficulty)
    {
        List<GameObject> maxDif = transform.GetChild(difficulty).gameObject.GetComponent<TouchingDestinations>().GetDestinations();
        List<GameObject> minDif = transform.GetChild(difficulty - 1).gameObject.GetComponent<TouchingDestinations>().GetDestinations();

        GameObject worstCaseObj = destinationList.GetChild(0).gameObject;
        int worstCaseDif = -1;

        for(int i = 0; i < maxDif.Count; i++)
        {
            if(!minDif.Contains(maxDif[i]))
            {
                Debug.Log("Found " + maxDif[i].name + " with difficulty " + difficulty);
                return maxDif[i];
            }
            else
            {
                for(int j = difficulty; j >= 0; j--)
                {
                    if(!transform.GetChild(j).gameObject.GetComponent<TouchingDestinations>().GetDestinations().Contains(maxDif[i]))
                    {
                        if(j > worstCaseDif)
                        {
                            worstCaseDif = j;
                            worstCaseObj = maxDif[i];
                            break;
                        }
                    }
                }
            }
        }

        Debug.Log("Worst Case " + worstCaseObj.name + " with original difficulty " + difficulty + " and worse case difficulty " + worstCaseDif);
        return worstCaseObj;
    }

    public void EnableCol()
    {
        for(int i = 0; i < 11; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    public void DisableCol()
    {
        for(int i = 0; i < 11; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
