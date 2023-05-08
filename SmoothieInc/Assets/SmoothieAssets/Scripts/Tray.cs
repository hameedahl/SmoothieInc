using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.EventSystems.EventTrigger;

public class Tray : MonoBehaviour
{
    private Cup cup;
    private GameObject cupGo;
    public bool gettingAcc = false;

    public GameHandler gameHandler;
    public GameObject[] slots;
    public bool[] isFull;
    public int drinksInTray = 0;
    public bool accCalculated = false;
    public AudioSource intray;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        putInTray();
        if (drinksInTray == gameHandler.drinkCount && !accCalculated) /* all drinks are in tray */
        {
            gameHandler.smoothieTut.writeToScreen("Smoothie completed!");

            accCalculated = true;
            gameHandler.completeOrder();
        }
    }

    private void putInTray()
    {
        GameObject cupL = GameObject.FindGameObjectWithTag("LCup");
        if (cupL)
        {
            checkCup(cupL);
        }
        GameObject cupM = GameObject.FindGameObjectWithTag("MCup");
        if (cupM)
        {
            checkCup(cupM);
        }
        GameObject cupS = GameObject.FindGameObjectWithTag("SCup");
        if (cupS)
        {
            checkCup(cupS);
        }

    }


    public void checkCup(GameObject cupGo) {  
            cup = cupGo.GetComponent<Cup>();
            if (cup && cup.isFinished &&
                Mathf.Abs(cupGo.transform.localPosition.x - transform.localPosition.x) >= .5f &&
                Mathf.Abs(cupGo.transform.localPosition.x - transform.localPosition.x) <= 2f &&
                Mathf.Abs(cupGo.transform.localPosition.y - transform.localPosition.y) >= .4f &&
                Mathf.Abs(cupGo.transform.localPosition.y - transform.localPosition.y) <= 1.5f)
            {
                for (int j = 0; j < gameHandler.drinkCount; j++)
                { /* find next available slot */
                    if (!isFull[j])
                    { /* snap object into slot if close enough (don't add to liquid slot)*/
                        cupGo.transform.position = new Vector3(slots[j].transform.position.x, slots[j].transform.position.y, slots[j].transform.position.z);
                        cupGo.transform.parent = this.transform;
                        intray.Play();
                        isFull[j] = true;
                        Destroy(cupGo.GetComponent<DragDrop>());
                        gameObject.tag = "PlayerTray";
                        drinksInTray++;
                        break;
                    }
                }
            }
    }
}
