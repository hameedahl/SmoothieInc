using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        TooltipHover._instance.SetAndShowTip(message);
    }

    private void OnMouseExit()
    {
        TooltipHover._instance.HideTip();
    }

}
