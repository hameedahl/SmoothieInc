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
    }

    void Update() {
        finishCup();
      //  pour();
        putInTray();

        //if (isCovered && hasStraw) { /* change to all drinks finished */
        //    gameHandler.finishDrink();
        //}
    }

    public void fillCup() {
        isEmpty = false;
        anim.Play("Fill-Cup");
        /* make button blink */
        //gameHandler.complete();
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
                isFinished = true;
        }
    }

    //public void pour()
    //{
    //    if (isEmpty && !blender.isEmpty && blender.isBlended &&
    //        Mathf.Abs(transform.localPosition.x - blender.transform.localPosition.x) >= 1000f &&
    //        Mathf.Abs(transform.localPosition.x - blender.transform.localPosition.x) <= 1003f &&
    //        Mathf.Abs(transform.localPosition.y - blender.transform.localPosition.y) >= .5f &&
    //        Mathf.Abs(transform.localPosition.y - blender.transform.localPosition.y) <= 4f)
    //    {
    //        //pourSlot = cup.transform.GetChild(0).gameObject;
    //        blender.transform.position = new Vector3(pourSlot.transform.position.x, pourSlot.transform.position.y, pourSlot.transform.position.z);
    //        blender.transform.eulerAngles = Vector3.forward * 90;
    //        fillCup();
    //        blender.isPouring = true;
    //    }
    //}

    private void putInTray()
    {
        if (GameObject.FindGameObjectWithTag("Tray"))
        {
            tray = GameObject.FindGameObjectWithTag("Tray").GetComponent<Tray>();
            trayGo = GameObject.FindGameObjectWithTag("Tray");
        }

        if (tray && isFinished &&
            Mathf.Abs(transform.localPosition.x - trayGo.transform.localPosition.x) >= .5f &&
            Mathf.Abs(transform.localPosition.x - trayGo.transform.localPosition.x) <= 2f &&
            Mathf.Abs(transform.localPosition.y - trayGo.transform.localPosition.y) >= .4f &&
            Mathf.Abs(transform.localPosition.y - trayGo.transform.localPosition.y) <= 1.5f)
        {
            for (int i = 0; i < gameHandler.drinkCount; i++)
            { /* find next available slot */
                if (!tray.isFull[i])
                { /* snap object into slot if close enough (don't add to liquid slot)*/
                    transform.position = new Vector3(tray.slots[i].transform.position.x, tray.slots[i].transform.position.y, tray.slots[i].transform.position.z);
                    transform.parent = this.transform;
                    tray.isFull[i] = true;
                    Destroy(GetComponent<DragDrop>());
                    (tray.drinks)++;
                }
            }
        }
    }
}
