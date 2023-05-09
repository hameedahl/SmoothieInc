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
    public string bestTime;
    public int minutes = 0;
    public int seconds = 0;
    public int playerMinutes = 0;
    public int playerSeconds = 0;



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
    public float[] gameTimes = new float[6];


    private void Start()
    {
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        WinText.gameObject.SetActive(false);

        gameTimes[0] = -1f;
        gameTimes[1] = 210.0f;
        gameTimes[2] = 180.0f;
        gameTimes[3] = 120.0f;
        gameTimes[4] = 90.0f;
        gameTimes[5] = 75.0f;
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
            if (!badSmoothie(playerScore) && !networkHandler.GetLostStatus())
            {
                /* destory old drink tray */
                if (GameObject.FindGameObjectWithTag("PlayerTray") != null)
                {
                    Destroy(GameObject.FindGameObjectWithTag("PlayerTray"));
                }
                winScreen.gameObject.SetActive(true);
                truckManager.EnableCol();
                accuracyText.text = playerScore + "%";  /* fill ui card */
                calculateTime();
                gameHandler.BestTime();
                tipText.text = "$" + networkHandler.GetPlayerTipStatus().ToString();  
                tipTextSmoothie.text = "$" + gameHandler.totalTip.ToString();

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

        if(!networkHandler.GetHostStatus() && networkHandler.GetFinalWin())
        {
            FinalWin();
        }
    }

    public void calculateTime()
    {
        minutes = Mathf.FloorToInt((int)gameTimes[difficulty] / 60); 
        seconds = Mathf.FloorToInt((int)gameTimes[difficulty] % 60); 


        int time = (int)gameTimes[difficulty] - (int)matchTimer.RemainingTime.Value;
        playerMinutes = Mathf.FloorToInt(time / 60);
        playerSeconds = Mathf.FloorToInt(time % 60);
        timeText.text = $"{playerMinutes:D2}:{playerSeconds:D2}";
    }

    public void NewOrders()
    {
        difficulty++;
        matchTimer.ResetTimer(gameTimes[difficulty]);
        StartGame(gameTimes[difficulty]);
        gameHandler.isFirstRound = false; /* turn off smoothie maker tips */
        sFinish = false;
        dFinish = false;
        if (difficulty != 6)
        {
            networkHandler.ResetOrders();
            gameHandler.newOrder(difficulty);
            truckManager.NewOrder(difficulty + 2);
            winScreen.SetActive(false);
        } else {
            networkHandler.SetFinalWin(true);
            FinalWin();
        }
    }

    public void FinalWin()
    {
        finalWinScreen.SetActive(true);
        bestTime = $"{networkHandler.GetBestTimeMin().ToString():D2}:{networkHandler.GetBestTimeSec().ToString():D2}";
        winTime.text = bestTime;
        winAccuracy.text = networkHandler.GetBestScoreStatus() + "%";
        winMoney.text = "$" + networkHandler.GetTotalMoney();
    }

    public bool badSmoothie(double playerScore)
    {
        if (playerScore < 50)
        {
            networkHandler.SetLostStatus(true);
            LoseScreen.SetActive(true);
            loseText.text = "Your customer was uhappy with their smoothie. They said it tasted much different from what they ordered. They've decided not to order from us again.";
            fullStar.SetActive(true);
            return true;
        }
        return false;

    }


    public void StartGame(float time)
    {
        matchTimer.StartTimer(time);
    }


}
