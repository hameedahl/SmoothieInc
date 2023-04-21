using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRange : MonoBehaviour
{
    public Collider2D range;
    public MeshRenderer current;

    // Start is called before the first frame update
    void Start()
    {
        range = GameObject.Find("Truck").transform.Find("Spawn Range").gameObject.GetComponent<Collider2D>();
        current.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<Collider2D>() == range)
        {
            current.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.GetComponent<Collider2D>() == range)
        {
            current.enabled = false;
        }
    }
}
