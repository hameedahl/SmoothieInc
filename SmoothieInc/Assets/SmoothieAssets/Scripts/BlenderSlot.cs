using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlenderSlot : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource blenderOn;
    public AudioSource buttonClick;
    public AudioSource blenderOff;
    public AudioSource openBlender;
    public AudioSource closeBlender;
    public AudioSource blenderMixing;
    public AudioSource iceDropped;
    public AudioSource liquidPouring;
    public AudioSource foodDropped;
    public AudioSource restartSound;

    public GameObject[] slots;
    public GameObject[] blenderItems;

    public bool[] isFull;
    private Animator animBottom;
    private Animator animTop;

    public GameHandler gameHandler;

    public GameObject bottom;
    private PickUpBlender top;
    Food itemInfo;

    GameObject blender;
    public bool blenderIsFull = false;
    private bool isBlending = false;
    GameObject anchor;
    public GameObject liquid;
    private GameObject newLiquid;
    private int blenderliqs;

    public GameObject timer;


    void Start()
    {
        animTop = this.gameObject.GetComponent<Animator>();
        top = this.gameObject.GetComponent<PickUpBlender>();
        animBottom = bottom.gameObject.GetComponent<Animator>();
        if (GameObject.FindGameObjectWithTag("Blender"))
        {
            blender = GameObject.FindGameObjectWithTag("Blender");
        }
    }

    private void Update()
    {
    }

    public bool addedToSlot(GameObject item)
    {
        itemInfo = item.GetComponent<Food>();
        /* check if item is close to blender */
        //Debug.Log(item.transform.localPosition.x - blender.transform.localPosition.x);
        //Debug.Log(item.transform.localPosition.y - blender.transform.localPosition.y);

        if (item && Mathf.Abs(item.transform.localPosition.x - blender.transform.localPosition.x) >= 0 &&
                        Mathf.Abs(item.transform.localPosition.x - blender.transform.localPosition.x) <= 2f &&
                        Mathf.Abs(item.transform.localPosition.y - blender.transform.localPosition.y) >= 0f &&
                        Mathf.Abs(item.transform.localPosition.y - blender.transform.localPosition.y) <= 3.3f)
        {
            if (top.isDetached || isBlending) /* don't insert if blender is on or detached */
            {
                Destroy(item);
                return false;
            }

            if (itemInfo.category == "Liquids")
            {
                if (!liquidPouring.isPlaying)
                {
                    liquidPouring.Play();
                }
                insertLiq(item, itemInfo);
                return true;
            }
            else if (itemInfo.category == "Ice")
            {
                if (!top.hasIce)
                {
                    insertIce(item, itemInfo);
                    gameHandler.smoothieTut.addFood();
                    return true;
                }
                Destroy(item);
            }
            else
            {
                bool addedFood = insertFood(item, itemInfo);
                if (blenderIsFull)
                {
                    Destroy(item);
                }
                return addedFood;
            }
        }
        return false;
    }

    private void insertLiq(GameObject item, Food itemInfo)
    {
        /* put item in slot above blender */
        item.transform.position = new Vector3(slots[slots.Length - 2].transform.position.x, slots[slots.Length - 2].transform.position.y, slots[slots.Length - 2].transform.position.z);
        itemInfo.isPouring = true;
        top.isEmpty = false;
        item.transform.eulerAngles = Vector3.forward * 90;
        newLiquid = Instantiate(liquid);
        blenderliqs++;
        pour(itemInfo, newLiquid);
        gameHandler.addToOrder(itemInfo);
    }

    private void insertIce(GameObject item, Food itemInfo)
    {
        item.transform.position = new Vector3(slots[slots.Length - 1].transform.position.x, slots[slots.Length - 1].transform.position.y, slots[slots.Length - 1].transform.position.z);
        blenderItems[blenderItems.Length - 1] = item;
        top.hasIce = true;
        iceDropped.Play();
        isFull[slots.Length - 1] = true;
        Destroy(item.GetComponent<DragDrop>());
    }

    private bool insertFood(GameObject item, Food itemInfo)
    {
        for (int i = 0; i < slots.Length; i++)
        { /* find next available slot */
            if (!isFull[i] && i != slots.Length - 2)
            { /* snap object into slot if close enough (don't add to liquid slot)*/
                item.transform.position = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y, slots[i].transform.position.z);
                blenderItems[i] = item;
                isFull[i] = true;
                itemInfo.inBlender = true;
                itemInfo.slotNum = i;
                top.gameObject.GetComponent<PickUpBlender>().isEmpty = false;
                foodDropped.Play();
                gameHandler.addToOrder(itemInfo);
                Destroy(item.GetComponent<DragDrop>()); /* object is no longer draggable */
                return true;
            }
        }
        blenderIsFull = true;
        return false;
    }




    public void stopBlender()
    {
        if (isBlending)
        {
            animTop.Play("Idle-Blended-Top");
            animBottom.Play("Idle-Bottom");
            timer.GetComponent<Timer>().stopTimer();
            top.isBlended = true;
            resetBlender();
            if (blenderMixing.isPlaying) {
                blenderMixing.Stop();
                blenderOff.Play();
            }
            buttonClick.Play();
        }
    }

    public void resetBlender()
    {
        blenderIsFull = false;
        isBlending = false;
    }

    public void startBlender()
    {
        if (!top.isEmpty && top.hasIce && !isBlending)
        {
            isBlending = true;
            animTop.Play("Blending-Top");
            animBottom.Play("Blending-Bottom");
            timer.GetComponent<Timer>().startTimer();
            emptyBlender();
            if (!blenderOn.isPlaying) {
                blenderOn.Play();
                blenderMixing.Play();
            }
            buttonClick.Play();
        }
    }

    public void emptyBlender()
    {
        for (int i = 0; i < blenderItems.Length; i++)
        {
            if (isFull[i])
            { /* remove all items in the blender */
                Destroy(blenderItems[i]);
                isFull[i] = false;
            }
        }

        for (int i = 0; i < blenderliqs; i++)
        {
            /* remove liquids */
            Destroy(GameObject.FindGameObjectsWithTag("BlenderLiq")[i]);
        }
        blenderliqs = 0;
    }


    public void pour(Food item, GameObject liquid)
    {
        SpriteRenderer sprite = liquid.GetComponentInChildren<SpriteRenderer>();
        sprite.color = new Color(item.liqColor.r, item.liqColor.g, item.liqColor.b);
        StartCoroutine(pouring(0, item, liquid));
    }

    public IEnumerator pouring(int yVal, Food item, GameObject anchor)
    {
        anchor = anchor.transform.GetChild(0).gameObject;
        while (anchor.transform.localScale.y != 11 && item.isPouring)
        {
            anchor.transform.localScale = new Vector3(.9f, yVal += 1);
            yield return new WaitForSeconds(.5f);
        }
        liquidPouring.Stop();
    }

    public void restartSmoothie()
    {
        if (!isBlending)
        {
            //restartSound.Play();
            emptyBlender(); /* clear blender */
            /* start player arr over */
            for (int i = 0; i < gameHandler.playerOrder.Length; i++)
            {
                gameHandler.playerOrder[i] = -1;
            }
            resetBlender();
            top.resetTop();
            animTop.Play("Idle-Empty-Top");
            animBottom.Play("Idle-Bottom");
        }
    }
}