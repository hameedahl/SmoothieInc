using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SmoothieTut : MonoBehaviour
{
    public GameHandler gameHandler;
    public TMP_Text tutText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTut()
    {
        tutText.text = "Open the cooler and add ice to the blender.";
    }

    public void addFood()
    {
        tutText.text = "Add the ingredients from the driver into the blender.";
    }
}
