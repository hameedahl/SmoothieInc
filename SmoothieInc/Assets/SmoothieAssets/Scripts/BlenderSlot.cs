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
    private bool isBlending = false;

    void Start() {
        animTop = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        animBottom = gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>();
    }
 
    public bool addedToSlot(Food item) {
        /* check if item is close to blender */
        if (Mathf.Abs(item.transform.localPosition.x - this.transform.localPosition.x) <= 2.3f &&
            Mathf.Abs(item.transform.localPosition.y - this.transform.localPosition.y) <= 5f) {
                if (item.category == "Liquids") {
                    /* put item in slot above blender */
                    item.transform.position = new Vector3(slots[slots.Length - 1].transform.position.x, slots[slots.Length - 1].transform.position.y, slots[slots.Length - 1].transform.position.z);
                    anim = item.GetComponent<Animator>();
                    item.transform.eulerAngles = Vector3.forward * 90;
                    anim.Play("Pouring" + item.id);
                    return true;
                } else {
                for (int i = 0; i < slots.Length; i++) { /* find next available slot */
                    if (!isFull[i]) { /* snap object into slot if close enough */
                        item.transform.position = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y, slots[i].transform.position.z);
                        slots[i] = item.gameObject;
                        isFull[i] = true;
                        item.inBlender = true;
                        item.slotNum = i;
                        gameObject.transform.GetChild(0).gameObject.GetComponent<PickUpBlender>().isEmpty = false;
                        gameHandler.playerOrder.Add(new KeyValuePair<string, int>(item.category, item.id));
                        return true;
                    }
                }
                }
        }
        return false;
    }

    public void stopBlender() {
        if (isBlending) {
            animTop.Play("Idle-Blended-Top");
            animBottom.Play("Idle-Bottom");
            gameObject.transform.GetChild(0).gameObject.GetComponent<PickUpBlender>().isBlended = true;
        }
    }

    public void startBlender() {
        if (!gameObject.transform.GetChild(0).gameObject.GetComponent<PickUpBlender>().isEmpty) {
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
