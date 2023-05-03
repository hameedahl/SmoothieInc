using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public AudioSource trash;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Food itemInfo = collision.gameObject.GetComponent<Food>();
        //if (itemInfo && itemInfo.category == "Solids")
        //{

        //}
        Destroy(collision.gameObject);
        trash.Play();
    }
}
