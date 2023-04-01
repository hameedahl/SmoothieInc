using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class GameHandler : MonoBehaviour
{
    const int solidsSize = 3; // banana, strawberries, blueberries
    const int liquidsSize = 1; // milk
    const int toppingsSize = 5;
    const int cupsSize = 3; // S M L
    const int specialSize = 5;

   // public List<int> playerOrder = new List<int>();
    public List<KeyValuePair<string, int>> playerOrder = new List<KeyValuePair<string, int>>();
    public List<KeyValuePair<string, int>> order = new List<KeyValuePair<string, int>>();
    public bool orderComplete = false;

    // Start is called before the first frame update
    void Start() {
        generateOrder(1);
        for (int i = 0; i < order.Count; i++) {
            Debug.Log(order[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (orderComplete) {
            bool res = order.Count == playerOrder.Count && playerOrder.All(order.Contains);
            if (res) {
                Debug.Log("Correct");
                KeyValuePair<string, int>[] playerArray = playerOrder.ToArray();
            } else {
                Debug.Log("Wrong");
            }
        }
    }

    public void generateOrder(int difficulty) { // 1 - 3
        Random rnd = new Random();
        if (difficulty == 1) {
            order.Add(new KeyValuePair<string, int>("Solids", rnd.Next(solidsSize)));
            order.Add(new KeyValuePair<string, int>("Solids", rnd.Next(solidsSize)));
            order.Add(new KeyValuePair<string, int>("Liquids", rnd.Next(liquidsSize)));
            order.Add(new KeyValuePair<string, int>("Liquids", rnd.Next(liquidsSize)));

            order.Add(new KeyValuePair<string, int>("Toppings", rnd.Next(toppingsSize)));
            order.Add(new KeyValuePair<string, int>("Toppings", rnd.Next(toppingsSize)));


        //     order.Add(rnd.Next(toppingsSize));
        //    order.Add(rnd.Next(cupsSize));
        //    order.Add(rnd.Next(specialSize));
        // } else if (difficulty == 2) {

        // } else (difficulty == 3) {

        } 
    }
}
