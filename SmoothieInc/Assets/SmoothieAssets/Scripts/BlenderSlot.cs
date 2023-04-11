using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlenderSlot : MonoBehaviour
{
    public GameObject[] slots;
    public bool[] isFull;
    private Animator anim;
    private Animator animBottom;
    private Animator animTop;

    public GameHandler gameHandler;
    public GameObject bottom;
    private PickUpBlender top;
    Food itemInfo;

    GameObject blender;
    public bool blenderIsFull = false;

    private bool isBlending = false;

    void Start() {
        animTop = this.gameObject.GetComponent<Animator>();
        top = this.gameObject.GetComponent<PickUpBlender>();
        animBottom = bottom.gameObject.GetComponent<Animator>();
        if (GameObject.FindGameObjectWithTag("Blender")) {
            blender = GameObject.FindGameObjectWithTag("Blender");
        }
    }

    public bool addedToSlot(GameObject item) {
        itemInfo = item.GetComponent<Food>();
        /* check if item is close to blender */
        //Debug.Log(item.transform.localPosition.x - blender.transform.localPosition.x);
        //Debug.Log(item.transform.localPosition.y - blender.transform.localPosition.y);

        if (item && Mathf.Abs(item.transform.localPosition.x - blender.transform.localPosition.x) >= 0 &&  
                        Mathf.Abs(item.transform.localPosition.x - blender.transform.localPosition.x) <= 2f && 
                        Mathf.Abs(item.transform.localPosition.y - blender.transform.localPosition.y) >= 0f &&
                        Mathf.Abs(item.transform.localPosition.y - blender.transform.localPosition.y) <= 3.3f) {
                if (itemInfo.category == "Liquids") {
                    /* put item in slot above blender */
                    item.transform.position = new Vector3(slots[slots.Length - 1].transform.position.x, slots[slots.Length - 1].transform.position.y, slots[slots.Length - 1].transform.position.z);
                    itemInfo.isPouring = true;
                    // anim = item.GetComponent<Animator>();
                    item.transform.eulerAngles = Vector3.forward * 90;
                    // anim.Play("Pouring" + itemInfo.id);
                    addToOrder(itemInfo);
                    return true;
                } else {
                    for (int i = 0; i < slots.Length; i++) { /* find next available slot */
                        if (!isFull[i] && i != slots.Length - 1) { /* snap object into slot if close enough (don't add to liquid slot)*/
                            item.transform.position = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y, slots[i].transform.position.z);
                            slots[i] = item;
                            isFull[i] = true;
                            itemInfo.inBlender = true;
                            itemInfo.slotNum = i;
                            top.gameObject.GetComponent<PickUpBlender>().isEmpty = false;
                            addToOrder(itemInfo);
                            Destroy(item.GetComponent<DragDrop>()); /* object is no longer draggable */
                            return true;
                        }
                    }
                    //blenderIsFull = true;
            }
        }
        return false;
    }

    private void addToOrder(Food item) {
        KeyValuePair<string, int> newPair = new KeyValuePair<string, int>(item.category, item.id);
            for (int i = 0; i < gameHandler.order.Length; i++) {
                if (newPair.ToString() == gameHandler.order[i].ToString() &&
                    !Array.Exists(gameHandler.playerOrder, elem => elem.ToString() == newPair.ToString())) {
                    gameHandler.playerOrder[i] = newPair;
                    gameHandler.playerScore += gameHandler.itemWeight;
                    return;
                }
            }
            if (gameHandler.playerScore > 0)
            {
                gameHandler.playerScore -= gameHandler.itemWeight;
            }
    }

    public void stopBlender() {
        if (isBlending) {
            animTop.Play("Idle-Blended-Top");
            animBottom.Play("Idle-Bottom");
            top.isBlended = true;
        }
    }

    public void startBlender() {
        if (!top.isEmpty) {
            isBlending = true;
            animTop.Play("Blending-Top");
            animBottom.Play("Blending-Bottom");
            for (int i = 0; i < slots.Length; i++) { 
                if (isFull[i]) { /* remove all items in the blender */
                    Destroy(slots[i]);
                    isFull[i] = false;
                }
            }
        }
    }
}
