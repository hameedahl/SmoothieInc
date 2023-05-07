using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public SpriteRenderer sr;
    public bool currentOrder = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f,1f,1f,0.5f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            sr.color = new Color(1f,1f,1f,0.3f);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            sr.color = new Color(1f,1f,1f,0.5f);
        }
    }

    public void SetCurrent(bool current)
    {
        if(current)
        {
            // currentOrder = true;
            sr.enabled = true;
        }
        else
        {
            // currentOrder = false;
            sr.enabled = false;
        }
    }
}
