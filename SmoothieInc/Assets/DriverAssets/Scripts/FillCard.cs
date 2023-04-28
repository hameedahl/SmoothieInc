using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillCard : MonoBehaviour
{
    [Header("Images")]
    public Sprite empty;
    public Sprite[] solids;
    public Sprite[] liquids;
    public Sprite[] blendTimes;
    public Sprite[] cupSizes;
    //public Sprite[] mixIns;
    //public Sprite[] toppings;

    bool colorSet = false;

    [Header("Slots")]
    public Image solid1;
    public Image solid2;
    public Image solid3;
    public Image solid4;

    public Image liquid1;
    public Image liquid2;
    public Image liquid3;

    public Image blendTime;

    public Image cupSize;

    public Image mixIn;

    public Image topping1;
    public Image topping2;

    public TextMeshProUGUI nameText;

    public void Initialize(int[] arr)
   {
       Fill(arr);
   }

    void Fill(int[] arr)
    {
        if(!colorSet)
        {
            Debug.Log(arr[5]);

            // randomly set color of card
            int ran = Random.Range(0, 3);
            if(ran == 0)
            {
                GetComponent<Image>().color = new Color(Random.Range(0.75f, 1f), Random.Range(0.5f, 1f), 1f, 1f);
            }
            else if(ran == 2)
            {
                GetComponent<Image>().color = new Color(Random.Range(0.75f, 1f), 1f, Random.Range(0.5f, 1f), 1f);
            }
            else
            {
                GetComponent<Image>().color = new Color(1f, Random.Range(0.75f, 1f), Random.Range(0.5f, 1f), 1f);
            }
            colorSet = true;
        }

        // this is very innefficient and i probably could have done it with
        // a for loop but i cannot be bothered (i will fix later maybe)

        // Solid 1
        if(arr[0] == -1)
            solid1.sprite = empty;
        else
            solid1.sprite = solids[arr[0]];

        // Solid 2
        if(arr[1] == -1)
            solid2.sprite = empty;
        else
            solid2.sprite = solids[arr[1]];

        // Solid 3
        if(arr[2] == -1)
            solid3.sprite = empty;
        else
            solid3.sprite = solids[arr[2]];

        // Solid 4
        if(arr[3] == -1)
            solid4.sprite = empty;
        else
            solid4.sprite = solids[arr[3]];

        // Liquid 1
        if(arr[4] == -1)
            liquid1.sprite = empty;
        else
            liquid1.sprite = liquids[arr[4]];

        // Liquid 2
        if(arr[5] == -1)
            liquid2.sprite = empty;
        else
            liquid2.sprite = liquids[arr[5]];

        // Liquid 3
        if(arr[6] == -1)
            liquid3.sprite = empty;
        else
            liquid3.sprite = liquids[arr[6]];

        // Blend Time
        if (arr[7] == -1)
            blendTime.sprite = empty;
        else
            blendTime.sprite = blendTimes[arr[7]];

        // Cup Size
        if (arr[8] == -1)
            cupSize.sprite = empty;
        else
            cupSize.sprite = cupSizes[arr[8]];

        //// Mix-In
        //if(arr[9] == -1)
        //    mixIn.sprite = empty;
        //else
        //    mixIn.sprite = mixIns[arr[9]];

        //// Topping 1
        //if(arr[10] == -1)
        //    topping1.sprite = empty;
        //else
        //    topping1.sprite = toppings[arr[10]];

        //// Topping 2
        //if(arr[11] == -1)
        //    topping2.sprite = empty;
        //else
        //    topping2.sprite = toppings[arr[11]];
    }
}
