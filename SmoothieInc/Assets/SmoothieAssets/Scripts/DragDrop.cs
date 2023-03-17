using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,
                        IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;

    private void Awake() {
        /* get object to be dragged */
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /* track mouse movement */
    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .6f; /* make transparent while drag */
        canvasGroup.blocksRaycasts = false; /* object lands in slot */
    }

    public void OnDrag(PointerEventData eventData) {
        /* get the amount the mouse has moved; make object drag */
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; 
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

}
