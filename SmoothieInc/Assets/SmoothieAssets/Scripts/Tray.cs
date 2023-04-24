using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEditor.Progress;

public class Tray : MonoBehaviour
{
    private Cup cup;
    private GameObject cupGo;

    public GameHandler gameHandler;
    public GameObject[] slots;
    public bool[] isFull;
    public int drinks = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        if (drinks == gameHandler.drinkCount) /* all drinks are in tray */
        {
            gameHandler.completeOrder();
        }
    }

    
}
