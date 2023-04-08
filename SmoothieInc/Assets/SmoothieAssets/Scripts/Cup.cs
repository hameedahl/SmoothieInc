using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Burst.Intrinsics.X86.Avx;


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
    public GameHandler gameHandler;
    //private bool moveStation = false;

    //[SerializeField] private Transform toppingStation;
    //[SerializeField] private Transform blendingStation;

    //[SerializeField] private CameraControl cam;
    //private GameObject newCup;


    void Start() {
        anim = this.GetComponent<Animator>();
        coverSlot = this.transform.GetChild(1).gameObject;
        strawSlot = this.transform.GetChild(2).gameObject;
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        //toppingStation = GameObject.FindGameObjectWithTag("Station0").transform;
        //blendingStation = GameObject.FindGameObjectWithTag("Station1").transform;

        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();

    }

    void Update() {
        finishCup();
        //if (moveStation) {
        //    cam.MoveToNewStation(toppingStation, this);

        //}

    }

    public void fillCup() {
        isEmpty = false;
        anim.Play("Fill-Cup");
        //gameHandler.inStation0 = false;
        //newCup = Instantiate(this.gameObject);
        //newCup.transform.position = new Vector3(17.6f, -2.82f, 0);
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

    //private void OnMouseDown()
    //{
    //    if (gameHandler.drinkFinished)
    //    {
    //        moveStation = true;
    //    }
    //}
}
