using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;



/*  [SOLID_ID, SOLID_ID, SOLID_ID, SOLID_ID,
     LIQUID_ID, LIQUID_ID, LIQUID_ID, BLEND_TIME (1-4),
     CUP_SIZE (1-3), MIX_IN_ID, TOPPING_ID, TOPPING_ID] */

public class GameHandler : MonoBehaviour
{
    const int solidsRange = 13;
    const int solidsIndex = 4; /* end index */

    const int liquidsRange = 8;
    const int liquidsIndex = 7;

    const int timeRange = 3;
    const int timeIndex = 8;

    const int cupsRange = 3; // S M L
    const int cupsIndex = 9;

    const int arraySize = 12;
    const int emptySlot = -1;

    public double blenderLevel = 0;

    public int[] order = new int[arraySize];
    public int[] playerOrder = new int[arraySize];
    public int[] valuesArray = new int[arraySize];
    private GameObject tray;
    public double playerScore = 0;
    public double bestPlayerScore = 0;
    
    public double itemWeight = 0;
    public bool orderComplete = false;
    bool inOrder = false;

    private int orderCount = 0;
    public int drinkCount = 0;
    public bool levelCompleted = false;

    public FillCard fillCard;
    public BooleanNetworkHandler bnh;

    public TMP_Text tipText;
    public double maxTip = 0;
    public double totalTip = 0;
    public double tripTip = 0;


    public bool isFirstRound = true;
    public SmoothieTut smoothieTut;




    // Start is called before the first frame update
    void Start() {
        newOrder(1);
    }

    public void newOrder(int difficulty)
    {
        /* destory old drink tray */
        if (GameObject.FindGameObjectWithTag("PlayerTray") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("PlayerTray"));
        }

        smoothieTut.writeToScreen("Open the cooler and add ice to the blender.");
        /* update tip */
        tipText.text = "$" + totalTip;

        Debug.Log("Generating New Order");
        for (int i = 0; i < arraySize; i++)
        {
            order[i] = emptySlot;
            playerOrder[i] = emptySlot;
        }

        generateOrder(difficulty);

        for (int i = 0; i < arraySize; i++)
        {
            valuesArray[i] = order[i];
        }
    }

    public void generateOrder(int difficulty) {
        orderComplete = false;
        System.Random rand = new System.Random();
        if (difficulty == 1) {
            drinkCount = 1;
            maxTip = 5;
            generateSolids(rand, 2);
            generateLiquids(rand, 1);
        } else if (difficulty == 2) {
            drinkCount = 1;
            maxTip = 6;
            generateSolids(rand, 3);
            generateLiquids(rand, 2);
        } else if (difficulty == 3) {
            drinkCount = 1;
            maxTip = 10;
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

        order[timeIndex - 1] = rand.Next(1, timeRange+1);
        orderCount++;

        order[cupsIndex - 1] = rand.Next(0, cupsRange);
        orderCount++;

        itemWeight = System.Math.Round(100.0 / orderCount, 2);
        difficulty++;
    }

    public void generateSolids(System.Random rand, int count)
    {
        for (int i = 0; i < count; i++)
        {
            order[i] = rand.Next(0, solidsRange+1);
            orderCount++;
        }
    }

    public void generateLiquids(System.Random rand, int count)
    {
        for (int i = solidsIndex; i < solidsIndex + count; i++)
        {
            order[i] = rand.Next(0, liquidsRange+1);
            orderCount++;
        }
    }

    public int getAccuracy()
    {
        checkOrder(0, solidsIndex);
        checkOrder(solidsIndex, liquidsIndex);
        checkBlendTime();
        checkOrder(timeIndex, cupsIndex);

        if (playerScore > 100) { playerScore = 100; }
        getTip();
        if (isFirstRound)
        {
            bestPlayerScore = playerScore;
        } else {
            BestScore();
        }
        return (int) playerScore;
    }

    public void getTip()
    {
        double percent = playerScore / 100;
        tripTip = System.Math.Round(maxTip * percent, 2);
        totalTip += System.Math.Round(tripTip, 2);
    }

    private void checkOrder(int start, int end)
    {
        for (int item = start; item < end; item++)
        {
            for (int orderItem = start; orderItem < end; orderItem++)
            {
                if (playerOrder[item] != -1 && playerOrder[item] == valuesArray[orderItem] && playerScore < 100)
                {
                    playerScore += itemWeight;
                    inOrder = true;
                    break;
                }
            }
        }
        lowerScore();
    }

    private void checkBlendTime()
    {
        if (blenderLevel != 0 && valuesArray[7] != 0)
        {
            playerScore += (blenderLevel / (valuesArray[7])) * itemWeight;
        }
    }

    private void lowerScore()
    {
        if (!inOrder && (playerScore - itemWeight) > 0)
        {
            playerScore -= itemWeight;
        }
    }

    public void addToOrder(Food item)
    {
        if (item.category == "Solids") {
            for (int i = 0; i < solidsIndex; i++)
            {
                if (playerOrder[i] == -1)
                {
                    playerOrder[i] = item.id;
                    return;
                }
            }
        } else if (item.category == "Liquids") {
            for (int i = solidsIndex; i < liquidsIndex; i++)
            {
                if (playerOrder[i] == -1)
                {
                    playerOrder[i] = item.id;
                    return;
                }
            }
        }
    }

    public void BestScore()
    {
        if (bestPlayerScore < playerScore)
        {
            bestPlayerScore = playerScore;
        }
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
        valuesArray = bnh.GetValuesArrayFromNetwork();
        double points = getAccuracy();
        for (int i = 0; i < playerOrder.Length; i++)
        {
            playerOrder[i] = -1;
        }
        Debug.Log(points);
        bnh.SetSmoothieServerRPC(true, points);
    }

}
