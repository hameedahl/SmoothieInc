using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using static UnityEditor.Progress;
using UnityEngine.XR;


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
    const int timeIndex = 8;

    const int cupsRange = 3; // S M L
    const int cupsIndex = 9;

    const int mixInRange = 4;
    const int mixInIndex = 10;

    const int toppingsRange = 5;
    const int toppingsIndex = 11;

    const int arraySize = 12;
    const int emptySlot = -1;

    public int difficulty = 1;



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
    public bool levelCompleted = false;

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

        newOrder();
    }

    private void Update()
    {
       //if (bnh.GetArrivedStatus() && bnh.GetDrinkFinishedStatus())
       //{

       //}
    }

    public void newOrder()
    {
        Debug.Log("Generating New Order");
        for (int i = 0; i < arraySize; i++)
        {
            order[i] = new KeyValuePair<string, int>("", emptySlot);
            playerOrder[i] = new KeyValuePair<string, int>("", emptySlot);
        }

        generateOrder();

        for (int i = 0; i < arraySize; i++)
        {
            valuesArray[i] = order[i].Value;
        }
    }

    public void generateOrder() {
        orderComplete = false;
        System.Random rand = new System.Random();
        if (difficulty == 1) {
            drinkCount = 1;
            generateSolids(rand, 2);
            generateLiquids(rand, 1);
        } else if (difficulty == 2) {
            drinkCount = 1;
            generateSolids(rand, 3);
            generateLiquids(rand, 2);
        } else if (difficulty == 3) {
            drinkCount = 1;
            generateSolids(rand, 4);
            generateLiquids(rand, 3);
        } else if (difficulty == 4) {
            //drinkCount = 2;
            //generateSolids(rand, 4);
            //generateLiquids(rand, 3);
            //order[timeIndex - 1] = new KeyValuePair<string, int>("Time", rand.Next(0, timeRange));
            //orderCount++;
            //order[cupsIndex - 1] = new KeyValuePair<string, int>("Cups", rand.Next(0, cupsRange));
            //orderCount++;
            //generateSolids(rand, 4);
            //generateLiquids(rand, 3);
        } else {
            //drinkCount = 4;
            //generateSolids(rand, 4);
            //generateLiquids(rand, 3);
            //generateSolids(rand, 4);
            //generateLiquids(rand, 3);
        }

        order[timeIndex - 1] = new KeyValuePair<string, int>("Time", rand.Next(0, timeRange));
        orderCount++;

        order[cupsIndex - 1] = new KeyValuePair<string, int>("Cups", rand.Next(0, cupsRange));
        orderCount++;

        itemWeight = System.Math.Round(100.0 / orderCount, 2);
        difficulty++;
    }

    public void generateSolids(System.Random rand, int count)
    {
        for (int i = 0; i < count; i++)
        {
            order[i] = new KeyValuePair<string, int>("Solids", rand.Next(0, solidsRange));
            orderCount++;
        }
    }

    public void generateLiquids(System.Random rand, int count)
    {
        for (int i = solidsIndex; i < count; i++)
        {
            order[i] = new KeyValuePair<string, int>("Liquids", rand.Next(0, liquidsRange));
            orderCount++;
        }
    }

    public int getAccuracy()
    {
        Debug.Log("player");
        for (int w = 0; w < 12; w++)
        {
            Debug.Log(playerOrder[w].Value);
        }


        //if (bnh.GetArrivedStatus() && bnh.GetDrinkFinishedStatus()) {
        //valuesArray = bnh.GetValuesArrayFromNetwork();
        int[] playerOrderArr = playerOrdertoArr();
            checkOrder(0, solidsIndex, playerOrderArr);
            checkOrder(solidsIndex, liquidsIndex, playerOrderArr);
            checkOrder(liquidsIndex, timeIndex, playerOrderArr);
            checkOrder(timeIndex, cupsIndex, playerOrderArr);
            //checkOrder(cupsIndex, mixInIndex, playerOrderArr);
            //checkOrder(mixInIndex, toppingsIndex, playerOrderArr);
      //  }
        return (int) playerScore;
    }

    private void checkOrder(int start, int end, int[] playerOrderArr)
    {
        for (int item = start; item < end; item++)
        {
            for (int orderItem = start; orderItem < end; orderItem++)
            {
                //Debug.Log("Comparing");
                //Debug.Log(playerOrderArr[item] + " with " + valuesArray[orderItem]);

                if (playerOrderArr[item] == valuesArray[orderItem] && playerScore < 100)
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
        //Debug.Log("Complete");
  

        valuesArray = bnh.GetValuesArrayFromNetwork();
        Debug.Log("Vals from bnh");

        for (int w = 0; w < 12; w++)
        {
            Debug.Log(valuesArray[w]);
        }

        double points = getAccuracy();
        Debug.Log(points);
        bnh.SetSmoothieServerRPC(true, points);
    }

}
