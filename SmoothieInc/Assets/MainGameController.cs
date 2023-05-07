using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UIElements;

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
    public string finishTime;
    public string bestTime = "01:00";

    bool sFinish = false;
    bool dFinish = false;

    bool finished = false;


    public GameObject finalWinScreen;
    public TMP_Text winTime;
    public TMP_Text winAccuracy;
    public TMP_Text winMoney;
    public GameObject LoseScreen;
    public Text loseText;
    public GameObject fullStar;
    public int difficulty = 1;



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
        }
        if (!dFinish && networkHandler.GetArrivedStatus())
        {
            dFinish = true;
        }
        if (networkHandler.GetArrivedStatus() && networkHandler.GetDrinkFinishedStatus()) {
            double playerScore = System.Math.Round(networkHandler.GetPlayerScoreStatus());
            matchTimer.isTimerStarted = false; /* pause timer */
            if (!badSmoothie(playerScore))
            {
                winScreen.gameObject.SetActive(true);
                accuracyText.text = playerScore + "%";
                calculateTime();
                //finishTime = matchTimer.RemainingTimeText.text;
                timeText.text = finishTime;
                if (gameHandler.isFirstRound)
                {
                    bestTime = finishTime;
                }
                else
                {
                    BestTime();
                }

                tipText.text = "$" + networkHandler.GetPlayerTipStatus().ToString();  
                tipTextSmoothie.text = "$" + gameHandler.tripTip.ToString();

                if (!networkHandler.GetHostStatus())
                {
                    nextLevelButton.SetActive(false);
                }

                finished = true;
            }
        }

        if(finished && !networkHandler.GetArrivedStatus())
        {
            winScreen.SetActive(false);
        }
    }

    public void NewOrders()
    {
        matchTimer.ResetTimer();
        StartGame();
        gameHandler.isFirstRound = false; /* turn off smoothie maker tips */
        sFinish = false;
        dFinish = false;
        //int difficulty = Random.Range(1,3);
        difficulty++;
        if (difficulty != 4)
        {
            networkHandler.ResetOrders();
            gameHandler.newOrder(difficulty);
            truckManager.NewOrder(difficulty + 2);
            winScreen.SetActive(false);
        } else {
            FinalWin();
        }
    }

    public void FinalWin()
    {
        finalWinScreen.SetActive(true);
        winTime.text = bestTime;
        winAccuracy.text = networkHandler.GetBestScoreStatus() + "%";
        winMoney.text = "$" + networkHandler.GetTotalMoney();
    }

    public bool badSmoothie(double playerScore)
    {
        if (playerScore < 50)
        {
            LoseScreen.SetActive(true);
            loseText.text = "Your customer was uhappy with their smoothie. They said it tasted much different from what they ordered. They've decided not to order from us again.";
            fullStar.SetActive(true);
            return true;
        }
        return false;

    }

    public void calculateTime()
    { 
        Debug.Log(matchTimer.RemainingTime.Value / 60);
        int minutes = 0;
        int seconds = 60 - Mathf.FloorToInt(matchTimer.RemainingTime.Value % 60);
        finishTime = $"{minutes:0}:{seconds:D2}";

    }

    public void StartGame()
    {
        matchTimer.StartTimer();
    }

    public void BestTime()
    {
        bestTime.Remove(2, 1);
        finishTime.Remove(2, 1);
        Debug.Log(System.Int32.Parse(bestTime));
        Debug.Log(System.Int32.Parse(finishTime));

        if (System.Int32.Parse(bestTime) < System.Int32.Parse(finishTime))
        {
            bestTime = finishTime;
        }
    }

}
