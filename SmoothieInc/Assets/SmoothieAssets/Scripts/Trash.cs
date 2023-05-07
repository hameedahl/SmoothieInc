using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public AudioSource trash;
    public GameObject item;
    public bool hitTrash = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitTrash = true;
    }
}
