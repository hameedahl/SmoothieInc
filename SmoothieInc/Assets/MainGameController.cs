using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainGameController : MonoBehaviour
{
    public BooleanNetworkHandler networkHandler;
    public TMP_Text WinText;
    public GameObject truckUI;
    public GameObject smoothieUI;
    public TruckManager truckManager;
    public GameHandler gameHandler;

    bool sFinish = false;
    bool dFinish = false;

    private void Start()
    {
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        WinText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        double playerScore = System.Math.Round(networkHandler.GetPlayerScoreStatus());
            Debug.Log("BOTH ARE THERE");
            Debug.Log(playerScore);
            WinText.gameObject.SetActive(true);
            if (playerScore >= 80)
            {
                WinText.text = "Arrived! Accuracy: " + playerScore + "% Great work!";
                WinText.color = Color.green;
            } else {
                WinText.text = "Arrived! Accuracy: " + playerScore + "%  Let's go for 80% next time!";
                WinText.color = Color.red;
            }
            networkHandler.ResetOrders();
            gameHandler.newOrder();
            truckManager.NewOrder();
        }
    }
}
