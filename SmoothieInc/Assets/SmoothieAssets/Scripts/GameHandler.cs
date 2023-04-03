// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using System;
// using UnityEngine.UI;
// using System.Random;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    const int solidsRange = 3; // banana, strawberries, blueberries
    const int solidsIndex = 4; /* end index */

    const int liquidsRange = 1; // milk
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
    private GameObject cup;
    public int playerScore = 100;
    public bool orderComplete = false;
    public GameObject WinText;


    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < arraySize; i++) {
            order[i] = new KeyValuePair<string, int>("", emptySlot); 
        }

        generateOrder(1);
        // for (int i = 0; i < order.Length; i++) {
        //     if (order[i].)
        //     Debug.Log(order[i]);
        // }
        foreach( KeyValuePair<string, int> kvp in order )
        {
           // if (kvp.Value != -1) {
                Debug.Log(kvp.Key + kvp.Value);
           // }

        }
    }


    // Update is called once per frame
    void Update() {
        cup = GameObject.FindGameObjectWithTag("Cup");
        if (cup && cup.GetComponent<Cup>().hasStraw && cup.GetComponent<Cup>().isCovered) { /////***
            Text scoreTextB = WinText.GetComponent<Text>();

            if (playerScore == 100) {
                scoreTextB.text = "Correct!";
                scoreTextB.color = Color.green;
            } else {
                scoreTextB.text = "Wrong :(";
                scoreTextB.color = Color.red;
            }
        }
    }

    public void generateOrder(int difficulty) { 
        if (difficulty == 1) {
            for (int i = 0; i < solidsIndex; i++) {
                order[i] = new KeyValuePair<string, int>("Solids", Random.Range(0, solidsRange));
            }

            for (int i = solidsIndex; i < liquidsIndex; i++) {
                order[i] = new KeyValuePair<string, int>("Liquids", Random.Range(0, liquidsRange)); 
            }

            // order[timeIndex] = new KeyValuePair<string, int>("Time", Random.Range(0, timeRange)); 

            // order[cupsIndex] = new KeyValuePair<string, int>("Cups", Random.Range(0, cupsRange)); 

            // order[mixInIndex] = new KeyValuePair<string, int>("MixIn", Random.Range(0, mixInIndex)); 


            // for (int i = mixInIndex + 1; i < toppingsIndex + 1; i++) {
            //     order[i] = new KeyValuePair<string, int>("Toppings", Random.Range(0, toppingsRange)); 
            // }
        // } else if (difficulty == 2) {

        // } else (difficulty == 3) {

        } 
    }
}
