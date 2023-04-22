using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipHover : MonoBehaviour
{
    public static TooltipHover _instance;
    public TextMeshProUGUI textComponent;
    private void Awake()
    {
        /* only one tooltip allowed at the same time */
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false); /* have tool tip hidden */
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Input.mousePosition; /* follow mouse pos. */
    }

    public void SetAndShowTip(string message)
    {
        gameObject.SetActive(true);
        textComponent.text = message;
    }

    public void HideTip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
