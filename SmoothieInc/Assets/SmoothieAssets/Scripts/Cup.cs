using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Cup : MonoBehaviour
{
    public GameObject slot;
    public bool isEmpty = true;
    private Animator anim;
    private bool isCovered = false;
    // private bool hasStraw = false;
    private GameObject cover;
    private GameObject coverSlot;
    // private GameObject strawSlot;


    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        coverSlot = this.transform.GetChild(1).gameObject;
        // strawSlot = gameObject.Find("StrawSlot");
    }

    // Update is called once per frame
    void Update()
    {
   

        // if (GameObject.FindGameObjectWithTag("Straw")) {
        //     straw = GameObject.FindGameObjectWithTag("Straw");
        // }

        finishCup();
    }

    public void fillCup() {
        isEmpty = false;
        anim.Play("Fill-Cup");
    }

    private void finishCup() {
        cover = GameObject.FindGameObjectWithTag("Cover");

        if (cover && Mathf.Abs(cover.transform.localPosition.x - this.transform.localPosition.x) <= .4f &&
            Mathf.Abs(cover.transform.localPosition.y - this.transform.localPosition.y) <= 2.3f) {
                cover.transform.position = new Vector3(coverSlot.transform.position.x, coverSlot.transform.position.y, coverSlot.transform.position.z);
        
                isCovered = true;
        }
    }

}
