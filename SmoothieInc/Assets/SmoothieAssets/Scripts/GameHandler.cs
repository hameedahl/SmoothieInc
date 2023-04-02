using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Random = System.Random;

public class GameHandler : MonoBehaviour
{
    const int solidsRange = 3; // banana, strawberries, blueberries
    const int solidsIndex = 3; /* end index */

    const int liquidsRange = 1; // milk
    const int liquidsIndex = 6; 

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

    public KeyValuePair<string, int>[] playerOrder = new KeyValuePair<string, int> [arraySize];
    public KeyValuePair<string, int>[] order = new KeyValuePair<string, int> [arraySize];
    public bool orderComplete = false;

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < arraySize; i++) {
            playerOrder[i] = new KeyValuePair<string, int>("", emptySlot);
            order[i] = new KeyValuePair<string, int>("", emptySlot); 
        }

        generateOrder(1);
        for (int i = 0; i < order.Length; i++) {
            Debug.Log(order[i]);
        }
    }


    // Update is called once per frame
    void Update() {
        if (orderComplete) {
            playerOrder.Sort((x, y) => (y.Value.CompareTo(x.Value)));
            
            bool result = order.Length == playerOrder.Length && playerOrder.SequenceEqual(order);
            if (result) {
                Debug.Log("Correct");
            } else {
                Debug.Log("Wrong");
            }
        }
    }

    public void generateOrder(int difficulty) { 
        Random rnd = new Random();
        if (difficulty == 1) {
            for (int i = 0; i < solidsIndex; i++) {
                order[i] = new KeyValuePair<string, int>("Solids", rnd.Next(solidsRange));
            }

            for (int i = solidsIndex; i < liquidsIndex; i++) {
                order[i] = new KeyValuePair<string, int>("Liquids", rnd.Next(liquidsRange)); 
            }

            // order[timeIndex] = new KeyValuePair<string, int>("Time", rnd.Next(timeRange)); 

            // order[cupsIndex] = new KeyValuePair<string, int>("Cups", rnd.Next(cupsRange)); 

            // order[mixInIndex] = new KeyValuePair<string, int>("MixIn", rnd.Next(mixInRange)); 


            // for (int i = mixInIndex + 1; i < toppingsIndex + 1; i++) {
            //     order[i] = new KeyValuePair<string, int>("Toppings", rnd.Next(toppingsRange)); 
            // }

            // order[1] = rnd.Next(solidsRange);
            // order[2] = rnd.Next(solidsRange);
            // //order[3] = rnd.Next(solidsRange);

            // order[4] = rnd.Next(liquidsSize);
            // order[5] = rnd.Next(liquidsSize);
            // //order[6] = rnd.Next(liquidsSize);

            // order[7] = rnd.Next(timeSize);

            // order[8] = rnd.Next(cupsRange);

            // //order[9] = rnd.Next(mixInRange);

            // order[10] = rnd.Next(toppingsRange);
            //order[11] = rnd.Next(toppingsRange);
        // } else if (difficulty == 2) {

        // } else (difficulty == 3) {

        } 
        order.Sort((x, y) => (y.Value.CompareTo(x.Value)));

    }

}
