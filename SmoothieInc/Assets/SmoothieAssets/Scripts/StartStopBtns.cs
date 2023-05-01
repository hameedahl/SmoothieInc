using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartStopBtns : MonoBehaviour
{
    public bool isStartBtn;
    private Color32 StartBtnIdle = new Color32(0, 171, 2, 255);
    private Color32 StartBtnHover = new Color32(3, 118, 0, 255);  

    private Color32 StopBtnIdle = new Color32(171, 21, 0, 255);  
    private Color32 StopBtnHover = new Color32(118, 10, 0, 255);

    private BlenderSlot blender;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Blender-Top"))
        {
            blender = GameObject.FindGameObjectWithTag("Blender-Top").GetComponent<BlenderSlot>();
        }
    }

    private void OnMouseOver()
    {
        darkenColor();
    }

    private void OnMouseDown()
    {
        darkenColor();
        if (isStartBtn) {
            blender.startBlender();
        } else {
            blender.stopBlender();
        }
    }

    private void OnMouseExit()
    {
        if (isStartBtn) {
            this.GetComponentInChildren<SpriteRenderer>().color = StartBtnIdle;
        } else {
            this.GetComponentInChildren<SpriteRenderer>().color = StopBtnIdle;
        }
    }

    private void darkenColor()
    {
        if (isStartBtn) {
            this.GetComponentInChildren<SpriteRenderer>().color = StartBtnHover;
        }
        else {
            this.GetComponentInChildren<SpriteRenderer>().color = StopBtnHover;
        }
    }
}
