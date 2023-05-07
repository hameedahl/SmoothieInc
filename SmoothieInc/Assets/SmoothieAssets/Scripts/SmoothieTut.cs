using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SmoothieTut : MonoBehaviour
{
    public GameHandler gameHandler;
    public TMP_Text tutText;

    public void writeToScreen(string message)
    {
        if (gameHandler.isFirstRound)
        {
            tutText.text = message;
        } else {
            tutText.gameObject.SetActive(false);
        }
    }

    //public void startTut()
    //{
    //     "Open the cooler and add ice to the blender.";
    //}

    //public void addFood()
    //{
    //    tutText.text = "Add the ingredients from the driver into the blender.";
    //}
}
