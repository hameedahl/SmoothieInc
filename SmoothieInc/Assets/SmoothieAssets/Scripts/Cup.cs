using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Burst.Intrinsics.X86.Avx;


public class Cup : MonoBehaviour
{
    public int itemId;
    public GameObject slot;
    public bool isEmpty = true;
    private Animator anim;
    public bool isCovered = false;
    public bool hasStraw = false;
    public AudioSource pourSmoothie;
    public AudioSource coverSound;
    public AudioSource strawSound;

    private GameObject cover;
    private GameObject coverSlot;
    public GameObject pourSlot;
    public Sprite coverArt;

    private GameObject straw;
    private GameObject strawSlot;
    public Sprite strawArt;
    public GameHandler gameHandler;
    public bool isFinished = false;
    private PickUpBlender blender;
    public Tray tray;
    public GameObject trayGo;

    void Start() {
        anim = this.GetComponent<Animator>();
        pourSlot = this.transform.GetChild(0).gameObject;
        coverSlot = this.transform.GetChild(1).gameObject;
        strawSlot = this.transform.GetChild(2).gameObject;
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        blender = GameObject.FindGameObjectWithTag("Blender-Top").GetComponent<PickUpBlender>();
        //blender.
    }

    void Update() {
        finishCup();
    }

    public void fillCup() {
        isEmpty = false;
        anim.Play("Fill-Cup");
        pourSmoothie.Play();
    }

    private void finishCup() {
        cover = GameObject.FindGameObjectWithTag("Cover");
        straw = GameObject.FindGameObjectWithTag("Straw");
        /* if straw and cover are close to cup, finish this smoothie */
        if (cover && !isEmpty && !isCovered && Mathf.Abs(cover.transform.localPosition.x - this.transform.localPosition.x) <= 1f &&
            Mathf.Abs(cover.transform.localPosition.y - this.transform.localPosition.y) <= 1f) {
                coverSlot.GetComponent<SpriteRenderer>().sprite = coverArt;
                cover.SetActive(false);
                isCovered = true;
                //coverSound.Play();
        }

        if (straw && isCovered && !hasStraw && !isEmpty && Mathf.Abs(straw.transform.localPosition.x - this.transform.localPosition.x) <= .8f &&
            Mathf.Abs(straw.transform.localPosition.y - this.transform.localPosition.y) <= .8f) {
                KeyValuePair<string, int> newPair = new KeyValuePair<string, int>("Size", itemId);
                strawSlot.GetComponent<SpriteRenderer>().sprite = strawArt;
                straw.SetActive(false);
                hasStraw = true;
                gameHandler.playerOrder[8] = newPair;
                isFinished = true;
                //strawSound.Play();

        }
    }

}
