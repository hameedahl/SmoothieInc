using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRange : MonoBehaviour
{
    public Collider2D range;
    public Collider2D current;

    // Start is called before the first frame update
    void Start()
    {
        range = GameObject.Find("Truck").transform.Find("Spawn Range").gameObject.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider == range)
        {
            current.enabled = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.collider == range)
        {
            current.enabled = false;
        }
    }
}
