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

    // Update is called once per frame
    void Update()
    {
        double playerScore = System.Math.Round(networkHandler.GetPlayerScoreStatus());
        if (networkHandler.GetArrivedStatus() && networkHandler.GetDrinkFinishedStatus()) {
            Text scoreTextB = WinText.GetComponent<Text>();
            if (playerScore >= 80)
            {
                scoreTextB.text = "Arrived! Accuracy: " + playerScore + "% Great work!";
                scoreTextB.color = Color.green;
            } else {
                scoreTextB.text = "Arrived! Accuracy: " + playerScore + "%  Let's go for at least 80% next time!";
                scoreTextB.color = Color.red;
            }
        }
        }
    }

