using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


/*  [SOLID_ID, SOLID_ID, SOLID_ID, SOLID_ID,
     LIQUID_ID, LIQUID_ID, LIQUID_ID, BLEND_TIME (1-4),
     CUP_SIZE (1-3), MIX_IN_ID, TOPPING_ID, TOPPING_ID] */

public class GameHandler : MonoBehaviour
{
    const int solidsRange = 13;
    const int solidsIndex = 4; /* end index */

    const int liquidsRange = 8;
    const int liquidsIndex = 7;

    const int timeRange = 2;
    const int timeIndex = 7;

    const int cupsRange = 3; // S M L
    const int cupsIndex = 9;

    const int mixInRange = 4;
    const int mixInIndex = 10;

    const int toppingsRange = 5;
    const int toppingsIndex = 11;

    const int arraySize = 12;
    const int emptySlot = -1;

    public KeyValuePair<string, int>[] order = new KeyValuePair<string, int>[arraySize];
    public KeyValuePair<string, int>[] playerOrder = new KeyValuePair<string, int>[arraySize];
    public int[] valuesArray = new int[arraySize];
    private GameObject tray;
    public double playerScore = 0;
    public double itemWeight = 0;
    public bool orderComplete = false;
    bool inOrder = false;

    private int orderCount = 0;
    public int drinkCount = 0;

    public FillCard fillCard;
    public BooleanNetworkHandler bnh;

    [SerializeField] private Transform toppingStation;
    [SerializeField] private Transform blendingStation;

    public Camera camGo;
    private CameraControl cam;
    public GameObject startBtn;
    public GameObject stopBtn;


    // Start is called before the first frame update
    void Start() {
        blendingStation = GameObject.FindGameObjectWithTag("Station0").transform;
        toppingStation = GameObject.FindGameObjectWithTag("Station1").transform;
        cam = camGo.GetComponent<CameraControl>();

        newOrder(1);


    }

    private void Update()
    {
       
    }

    public void newOrder(int difficulty)
    {
        for (int i = 0; i < arraySize; i++)
        {
            order[i] = new KeyValuePair<string, int>("", emptySlot);
            playerOrder[i] = new KeyValuePair<string, int>("", emptySlot);
        }

        generateOrder(difficulty);

        for (int i = 0; i < arraySize; i++)
        {
            valuesArray[i] = order[i].Value;
        }
    }

    public void generateOrder(int difficulty) {
        System.Random rand = new System.Random();
        if (difficulty == 1) {
            drinkCount = 1;
            for (int i = 0; i < 3; i++) {
                order[i] = new KeyValuePair<string, int>("Solids", rand.Next(0, solidsRange));
                orderCount++;
            }

            for (int i = solidsIndex; i < solidsIndex + 2; i++) {
                order[i] = new KeyValuePair<string, int>("Liquids", rand.Next(0, liquidsRange));
                orderCount++;
            }

            order[timeIndex] = new KeyValuePair<string, int>("Time", rand.Next(0, timeRange));
            orderCount++;

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

    public int getAccuracy()
    {
        if (bnh.GetArrivedStatus() && bnh.GetDrinkFinishedStatus()) {
            valuesArray = bnh.GetValuesArrayFromNetwork();
            int[] playerOrderArr = playerOrdertoArr();
            checkOrder(0, solidsIndex, playerOrderArr);
            checkOrder(solidsIndex, liquidsIndex, playerOrderArr);
            checkOrder(liquidsIndex, timeIndex, playerOrderArr);
            //checkOrder(timeIndex, cupsIndex, playerOrderArr);
            //checkOrder(cupsIndex, mixInIndex, playerOrderArr);
            //checkOrder(mixInIndex, toppingsIndex, playerOrderArr);
        }
        return (int) playerScore;
    }

    private void checkOrder(int start, int end, int[] playerOrderArr)
    {
        for (int item = start; item < end; item++)
        {
            for (int orderItem = start; orderItem < end; orderItem++)
            {
                if (playerOrderArr[item] == valuesArray[orderItem])
                {
                    playerScore += itemWeight;
                    inOrder = true;
                }
            }
        }
        lowerScore();
    }

    private void lowerScore()
    {
        if (!inOrder && playerScore > 0)
        {
            playerScore -= itemWeight;
        }
    }
 
    private int[] playerOrdertoArr()
    {
         int[] playerOrderArr = new int[arraySize];
         for (int i = 0; i < playerOrder.Length; i++)
         {
                playerOrderArr[i] = playerOrder[i].Value;
         }
         return playerOrderArr;
    }


    public bool GetDrinkFinished()
    {
      return orderComplete;
    }

    public double GetPlayerScore()
    {
      return playerScore;
    }

    public int[] GetValuesArray()
    {
      return valuesArray;
    }

    public void SetValuesArray(int[] order)
    {
      valuesArray = order;
    }

    public void completeOrder()
    {
        orderComplete = true;
        bnh.SetSmoothieServerRPC(true, getAccuracy());
    }
}
