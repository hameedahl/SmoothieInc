using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{
    public BooleanNetworkHandler networkHandler;
    public GameObject WinText;
    public GameObject truckUI;
    public GameObject smoothieUI;
    bool sFinish = false;
    bool dFinish = false;

    // Update is called once per frame
    void Update()
    {
        double playerScore = System.Math.Round(networkHandler.GetPlayerScoreStatus());
        if (!sFinish && networkHandler.GetDrinkFinishedStatus())
        {
            sFinish = true;
            Debug.Log("Smoothie has finished");
        }
        if (!dFinish && networkHandler.GetArrivedStatus())
        {
            dFinish = true;
            Debug.Log("Driver has arrived");
        }
        if (networkHandler.GetArrivedStatus() && networkHandler.GetDrinkFinishedStatus()) {
            Debug.Log("BOTH ARE THERE");
            Debug.Log(playScore);
            Text scoreTextB = WinText.GetComponent<Text>();
            if (playerScore >= 80)
            {
                scoreTextB.text = "Arrived! Accuracy: " + playerScore + "% Great work!";
                scoreTextB.color = Color.green;
            } else {
                scoreTextB.text = "Arrived! Accuracy: " + playerScore + "%  Let's go for 80% next time!";
                scoreTextB.color = Color.red;
            }
        }
        }
    }
