using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlenderSlot : MonoBehaviour
{
    public GameObject[] slots;
    public bool[] isFull;
    private Animation anim;

    // void Start() {
    //     anim = this.GetComponent<Animation>();
    // }
 
    public bool addedToSlot(DragDrop item) {
        /* check if item is close to blender */
        if (Mathf.Abs(item.transform.localPosition.x - this.transform.localPosition.x) <= 2.3f &&
            Mathf.Abs(item.transform.localPosition.y - this.transform.localPosition.y) <= 2.3f) {
                // Debug.Log("");
                for (int i = 0; i < slots.Length; i++) { /* find next available slot */
                    if (!isFull[i]) { /* snap object into slot if close enough */
                        item.transform.position = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y, slots[i].transform.position.z);
                        isFull[i] = true;
                        item.inBlender = true;
                        item.slotNum = i;
                        return true;
                    }
                }
        }
        return false;
    }

    public void stop() {
        anim = this.GetComponent<Animation>();
        Debug.Log("Here");
        anim.Play("Idle-Blended");
    }

    public void start() {
        anim = this.GetComponent<Animation>();
        Debug.Log("asdlfkz");

        anim.Play("Blending");
    }
}
