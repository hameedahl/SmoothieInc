using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlenderSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    private Slots inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Blender").GetComponent<Slots>();
    }

    public void OnDrop(PointerEventData eventData) {
        /* get dragged item and place in slot */
        if (eventData.pointerDrag != null) {
            for (int i = 0; i < inventory.slots.Length; i++){
                if (inventory.isFull[i] == false) {
                    /* item can be added to inventory */
                    rectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    inventory.isFull[i] = true;
            Debug.Log("IN");

                    break;
                }
            }
        }
    }
}
