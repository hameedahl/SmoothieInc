using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public AudioSource trash;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
       // trash.Play();
    }
}
