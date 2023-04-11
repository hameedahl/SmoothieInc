using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*  [SOLID_ID, SOLID_ID, SOLID_ID, SOLID_ID,
     LIQUID_ID, LIQUID_ID, LIQUID_ID, BLEND_TIME (1-4),
     CUP_SIZE (1-3), MIX_IN_ID, TOPPING_ID, TOPPING_ID] */

public class GameHandler : MonoBehaviour
{
    const int solidsRange = 10;
    const int solidsIndex = 4; /* end index */

    const int liquidsRange = 5;
    const int liquidsIndex = 7;

    const int timeRange = 4;
    const int timeIndex = 7;

    const int cupsRange = 3; // S M L
    const int cupsIndex = 8;

    const int mixInRange = 4;
    const int mixInIndex = 9;

    const int toppingsRange = 5;
    const int toppingsIndex = 11;

    const int arraySize = 12;
    const int emptySlot = -1;

    public KeyValuePair<string, int>[] order = new KeyValuePair<string, int> [arraySize];
    public KeyValuePair<string, int>[] playerOrder = new KeyValuePair<string, int>[arraySize];
    public int[] valuesArray = new int[arraySize];
    private GameObject cup;
    public double playerScore = 0;
    public double itemWeight;
    public bool orderComplete = false;
    public bool drinkFinished = false;

    private int orderCount = 0;
    public GameObject WinText;
    public FillCard fillCard;

    [SerializeField] private Transform toppingStation;
    [SerializeField] private Transform blendingStation;

    //[SerializeField] private CameraControl cam;
   // private bool inStation0 = true;


    // Start is called before the first frame update
    void Start() {
        blendingStation = GameObject.FindGameObjectWithTag("Station0").transform;
        toppingStation = GameObject.FindGameObjectWithTag("Station1").transform;

        for (int i = 0; i < arraySize; i++) {
            order[i] = new KeyValuePair<string, int>("", emptySlot);
            playerOrder[i] = new KeyValuePair<string, int>("", emptySlot);
        }

        generateOrder(1);
        foreach (KeyValuePair<string, int> item in order)
        {
            Debug.Log(item);
        }

        for (int i = 0; i < arraySize; i++)
        {
            valuesArray[i] = order[i].Value;
        }

        // Print the valuesArray for debugging purposes
        // foreach (int value in valuesArray)
        // {
        //     Debug.Log(value);
        // }



        fillCard.Initialize(valuesArray);


    }


    // Update is called once per frame
    void Update() {
        //if (GameObject.FindGameObjectWithTag("Smoothie-Camera")) {
        //    cam = GameObject.FindGameObjectWithTag("Smoothie-Camera").GetComponent<CameraControl>();

        //}

        cup = GameObject.FindGameObjectWithTag("Cup");
        if (orderComplete) { /////***
            //if (inStation0)
            //{
            //    pauseGame();
            //}
            pauseGame();
            Text scoreTextB = WinText.GetComponent<Text>();

            //// if (playerScore == 100) {
            //     // scoreTextB.text = "Correct!";
            //     // scoreTextB.color = Color.green;
            // //} else {
            if (playerScore < 0)
            {
                scoreTextB.text = "Accuracy: 0%";
            }
            else
            {
                scoreTextB.text = "Accuracy: " + System.Math.Round(playerScore) + "%";
            }
            scoreTextB.color = Color.red;
            // drinkFinished = true;
            // delete old cup


            // }
        }

        //cup = GameObject.FindGameObjectWithTag("Cup");
        //if (cup && !cup.GetComponent<Cup>().isEmpty) { /////***
        //    //if (inStation0)
        //    //{
        //    //    pauseGame();
        //    //}
        //    Text scoreTextB = WinText.GetComponent<Text>();

        //    //// if (playerScore == 100) {
        //    //     // scoreTextB.text = "Correct!";
        //    //     // scoreTextB.color = Color.green;
        //    // //} else {
        //    if (playerScore < 0)
        //    {
        //        scoreTextB.text = "Accuracy: 0%";
        //    }
        //    else
        //    {
        //        scoreTextB.text = "Accuracy: " + System.Math.Round(playerScore) + "%";
        //    }
        //    scoreTextB.color = Color.red;
        //    // drinkFinished = true;
        //    // delete old cup


        //    // }
        //}
    }

    public void pauseGame()
    {
        StartCoroutine(GamePauser());
    }

    public IEnumerator GamePauser()
    {
        yield return new WaitForSeconds(1);
        //cam.MoveToNewStation(toppingStation, cup.GetComponent<Cup>());
       // inStation0 = false;
    }

    public void generateOrder(int difficulty) {
        System.Random rand = new System.Random();
        if (difficulty == 1) {
            for (int i = 0; i < 3; i++) {
                order[i] = new KeyValuePair<string, int>("Solids", rand.Next(0, solidsRange));
                orderCount++;
            }

            for (int i = solidsIndex; i < solidsIndex + 2; i++) {
                order[i] = new KeyValuePair<string, int>("Liquids", rand.Next(0, liquidsRange));
                orderCount++;

            }

            //order[timeIndex] = new KeyValuePair<string, int>("Time", rand.Next(0, timeRange));
            //    orderCount++;

            itemWeight = System.Math.Round(100.0 / orderCount, 2);

            // order[cupsIndex] = new KeyValuePair<string, int>("Cups", rand.Next(0, cupsRange));

            // order[mixInIndex] = new KeyValuePair<string, int>("MixIn", rand.Next(0, mixInIndex));

            // for (int i = mixInIndex + 1; i < toppingsIndex + 1; i++) {
            //     order[i] = new KeyValuePair<string, int>("Toppings", rand.Next(0, toppingsRange));
            // }
        }

        // else if (difficulty == 2) {

        // } else (difficulty == 3) {
    }

    public bool GetDrinkFinished()
    {
      return drinkFinished;
    }

    public double GetPlayerScore()
    {
      return playerScore;
    }
    
}
