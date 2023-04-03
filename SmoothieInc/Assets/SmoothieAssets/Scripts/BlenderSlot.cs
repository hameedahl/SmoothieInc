using System.Collections;
using System.Collections.Generic;
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
    Food foodItem;

    private bool isBlending = false;

    void Start() {
        animTop = this.gameObject.GetComponent<Animator>();
        top = this.gameObject.GetComponent<PickUpBlender>();
        animBottom = bottom.gameObject.GetComponent<Animator>();
    }
    // public void OnDrop(PointerEventData eventData) {
    //     if (eventData.pointerDrag != null) {
    //         Debug.Log("HIHJHO:JUIOHJ:");
    //           for (int i = 0; i < slots.Length; i++) { /* find next available slot */
    //                     if (!isFull[i]) { /* snap object into slot if close enough */
    //                     //     eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
    //         Debug.Log("JUasdIOHJ:");

    //                     //     //item.transform.position = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y, slots[i].transform.position.z);
    //                     //     slots[i] = eventData.pointerDrag.gameObject;
    //                     //     isFull[i] = true;
    //                     //     eventData.pointerDrag.GetComponent<Food>().inBlender = true;
    //                     //     eventData.pointerDrag.GetComponent<Food>().slotNum = i;
    //                     //    eventData.pointerDrag.GetComponent<PickUpBlender>().isEmpty = false;
    //                     //     addToOrder(eventData.pointerDrag.GetComponent<Food>());
    //                     //     //return true;
    //                     }
    //                 }
    //     }
    // }
 
    public bool addedToSlot(GameObject item) {
        foodItem = item.GetComponent<Food>();
        /* check if item is close to blender */
        // Debug.Log(Mathf.Abs(item.transform.localPosition.x - this.transform.localPosition.x));
        // Debug.Log(Mathf.Abs(item.transform.localPosition.y - this.transform.localPosition.y));

        if (Mathf.Abs(item.transform.localPosition.x - this.transform.localPosition.x) >= 2.3f && Mathf.Abs(item.transform.localPosition.x - this.transform.localPosition.x) <= 4f ||
            Mathf.Abs(item.transform.localPosition.y - this.transform.localPosition.y) <= 2f && Mathf.Abs(item.transform.localPosition.y - this.transform.localPosition.y) >= .4f) {
                if (foodItem.category == "Liquids") {
                    /* put item in slot above blender */
                    item.transform.position = new Vector3(slots[slots.Length - 1].transform.position.x, slots[slots.Length - 1].transform.position.y, slots[slots.Length - 1].transform.position.z);
                    //anim = item.GetComponent<Animator>();
                    item.transform.eulerAngles = Vector3.forward * 90;
                    //anim.Play("Pouring" + item.id);
                    return true;
                } else {
                    for (int i = 0; i < slots.Length; i++) { /* find next available slot */
                        if (!isFull[i]) { /* snap object into slot if close enough */
                            item.transform.position = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y, slots[i].transform.position.z);
                            slots[i] = item.gameObject;
                            isFull[i] = true;
                            foodItem.inBlender = true;
                            foodItem.slotNum = i;
                           top.gameObject.GetComponent<PickUpBlender>().isEmpty = false;
                            addToOrder(foodItem);
                            return true;
                        }
                   }
                }
        }
        return false;
    }

    private void addToOrder(Food item) {
        KeyValuePair<string, int> newPair = new KeyValuePair<string, int>(item.category, item.id);
        for (int i = 0; i < gameHandler.order.Length; i++) {
            if (newPair.ToString() == gameHandler.order[i].ToString()) {
                return;
            }
        }

        gameHandler.playerScore -= 10;
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
                }
            }
        }
    }
}
