using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonEffects : MonoBehaviour
{
    public Vector3 normalSize;
    public Vector3 hoverSize;
    public Vector3 clickSize;
    public TMP_Text text;
    
    bool hover = false;

    void Start()
    {
        transform.localScale = normalSize;
        text.color = new Color(255, 255, 255);
    }

    public void enterHover()
    {
        transform.localScale = hoverSize;
        hover = true;
    }

    public void exitHover()
    {
        transform.localScale = normalSize;
        hover = false;
    }

    public void mouseDown()
    {
        if(hover)
            transform.localScale = clickSize;
    }

    public void mouseUp()
    {
        if(hover)
            transform.localScale = hoverSize;
        else
            transform.localScale = normalSize;
    }
}
