using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHouses : MonoBehaviour
{
    public GameObject[] houseModels;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform currChild = transform.GetChild(i);
            int house = Random.Range(0,2);
            GameObject curr = Instantiate(houseModels[house], currChild);
            curr.transform.parent = currChild;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
