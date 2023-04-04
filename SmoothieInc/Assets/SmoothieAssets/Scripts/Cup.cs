using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Cup : MonoBehaviour
{
    public GameObject slot;
    public bool isEmpty = true;
    private Animator anim;
    public bool isCovered = false;
    public bool hasStraw = false;

    private GameObject cover;
    private GameObject coverSlot;
    public Sprite coverArt;

    private GameObject straw;
    private GameObject strawSlot;
    public Sprite strawArt;

    void Start() {
        anim = this.GetComponent<Animator>();
        coverSlot = this.transform.GetChild(1).gameObject;
        strawSlot = this.transform.GetChild(2).gameObject;
    }
    
    void Update() {
        finishCup();
    }

    public void fillCup() {
        isEmpty = false;
        anim.Play("Fill-Cup");
    }

    private void finishCup() {
        cover = GameObject.FindGameObjectWithTag("Cover");
        straw = GameObject.FindGameObjectWithTag("Straw");
        /* if straw and cover are close to cup, finish this smoothie */
        if (cover && !isEmpty && Mathf.Abs(cover.transform.localPosition.x - this.transform.localPosition.x) <= 1f &&
            Mathf.Abs(cover.transform.localPosition.y - this.transform.localPosition.y) <= 1f) {
                coverSlot.GetComponent<SpriteRenderer>().sprite = coverArt;
                cover.SetActive(false);
                isCovered = true;
        }

        if (straw && isCovered && !isEmpty && Mathf.Abs(straw.transform.localPosition.x - this.transform.localPosition.x) <= .4f &&
            Mathf.Abs(straw.transform.localPosition.y - this.transform.localPosition.y) <= .4f) {
                strawSlot.GetComponent<SpriteRenderer>().sprite = strawArt;
                straw.SetActive(false);
                hasStraw = true;
        }
    }
}
