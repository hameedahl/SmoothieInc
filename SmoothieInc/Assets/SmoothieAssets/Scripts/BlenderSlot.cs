using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlenderSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;

    public void OnDrop(PointerEventData eventData) {
        /* get dragged item and place in slot */
        if (eventData.pointerDrag != null) {
            rectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
