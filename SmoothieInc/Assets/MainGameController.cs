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
    public GameObject winScreen;
    public TMP_Text accuracyText;
    public TMP_Text timeText;
    public TMP_Text tipText;
    public TMP_Text tipTextSmoothie;


    public GameObject nextLevelButton;
    public MatchTimer matchTimer;


    bool sFinish = false;
    bool dFinish = false;

    bool finished = false;

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

            winScreen.gameObject.SetActive(true);
            accuracyText.text = playerScore + "%";
            timeText.text = matchTimer.RemainingTimeText.text;
            tipText.text = "$" + gameHandler.tripTip.ToString();
            tipTextSmoothie.text = "$" + gameHandler.totalTip.ToString();

            if (!networkHandler.GetHostStatus())
            {
                nextLevelButton.SetActive(false);
            }


            finished = true;
        }

        if(finished && !networkHandler.GetArrivedStatus())
        {
            winScreen.SetActive(false);
        }
    }

    public void NewOrders()
    {
        gameHandler.isFirstRound = false; /* turn off smoothie maker tips */
        sFinish = false;
        dFinish = false;
        int difficulty = Random.Range(1,3);
        networkHandler.ResetOrders();
        gameHandler.newOrder(difficulty);
        truckManager.NewOrder(difficulty);
        winScreen.SetActive(false);
        matchTimer.ResetTimer();

    }

    public void StartGame()
    {
      matchTimer.StartTimer();
    }

  }
