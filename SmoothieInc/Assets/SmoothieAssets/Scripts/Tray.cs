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
    public int drinksInTray = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drinksInTray == gameHandler.drinkCount) /* all drinks are in tray */
        {
            gameHandler.completeOrder();
        }
        putInTray();
    }

    private void putInTray()
    {
        GameObject[] cups = GameObject.FindGameObjectsWithTag("Cup");
        int cupsSize = cups.Length;

        for (int i = 0; i < cupsSize; i++)
        {
            if (cups[i])
            {
                cup = cups[i].GetComponent<Cup>();
            }

            if (cup && cup.isFinished &&
                Mathf.Abs(cups[i].transform.localPosition.x - transform.localPosition.x) >= .5f &&
                Mathf.Abs(cups[i].transform.localPosition.x - transform.localPosition.x) <= 2f &&
                Mathf.Abs(cups[i].transform.localPosition.y - transform.localPosition.y) >= .4f &&
                Mathf.Abs(cups[i].transform.localPosition.y - transform.localPosition.y) <= 1.5f)
            {
                for (int j = 0; j < gameHandler.drinkCount; j++)
                { /* find next available slot */
                    if (!isFull[j])
                    { /* snap object into slot if close enough (don't add to liquid slot)*/
                        cups[i].transform.position = new Vector3(slots[j].transform.position.x, slots[j].transform.position.y, slots[j].transform.position.z);
                        cups[i].transform.parent = this.transform;
                        isFull[j] = true;
                        Destroy(cups[i].GetComponent<DragDrop>());
                        drinksInTray++;
                        break;
                    }
                }
            }
        }
    }

}
