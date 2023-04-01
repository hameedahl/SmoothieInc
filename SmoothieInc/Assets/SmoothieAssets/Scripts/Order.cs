using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Order : MonoBehaviour
{
    const int tableSize = 5;
    const int solidsSize = 3; // banana, strawberries, blueberries
    const int liquidsSize = 4; // milk
    const int toppingsSize = 5;
    const int cupsSize = 3; // S M L
    const int specialSize = 5;
    public GameHandler gameHandler;

    

    // Start is called before the first frame update
    // void Start() {
    //     generateOrder(1);
    //     for (int i = 0; i < order.Count; i++) {
    //         Debug.Log(order[i]);
    //     }
    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    // public void generateOrder(int difficulty) { // 1 - 3
    //     Random rnd = new Random();
    //     if (difficulty == 1) {
    //         gameHandler.order.Add(rnd.Next(solidsSize));
    //         gameHandler.order.Add(rnd.Next(liquidsSize));
    //         gameHandler.order.Add(rnd.Next(toppingsSize));
    //         gameHandler.order.Add(rnd.Next(cupsSize));
    //        // order.Add(rnd.Next(specialSize));
    //     // } else if (difficulty == 2) {

    //     // } else (difficulty == 3) {

    //     } 
    // }
}