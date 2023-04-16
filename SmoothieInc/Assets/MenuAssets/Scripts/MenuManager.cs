using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void startGame(bool isDriver)
    {
        if(isDriver)
        {
            StaticClass.CrossSceneInformation = "Driver";
            SceneManager.LoadScene("Christopher_Minimap_Driver");
        }
        else
        {
            StaticClass.CrossSceneInformation = "Smoothie";
            SceneManager.LoadScene("Christopher_Minimap_Driver");
        }
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
