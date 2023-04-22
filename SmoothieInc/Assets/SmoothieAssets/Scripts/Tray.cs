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
    private int drinks = 0;

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
        putInTray();
    }

    private void putInTray()
    {
        if (GameObject.FindGameObjectWithTag("Cup"))
        {
            cup = GameObject.FindGameObjectWithTag("Cup").GetComponent<Cup>();
            cupGo = GameObject.FindGameObjectWithTag("Cup");
        }

        if (cup && cup.isFinished &&
            Mathf.Abs(cup.transform.localPosition.x - this.transform.localPosition.x) >= .5f &&
            Mathf.Abs(cup.transform.localPosition.x - this.transform.localPosition.x) <= 2f &&
            Mathf.Abs(cup.transform.localPosition.y - this.transform.localPosition.y) >= .4f &&
            Mathf.Abs(cup.transform.localPosition.y - this.transform.localPosition.y) <= 1.5f)
        {
            for (int i = 0; i < gameHandler.drinkCount; i++)
            { /* find next available slot */
                if (!isFull[i])
                { /* snap object into slot if close enough (don't add to liquid slot)*/
                    cupGo.transform.position = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y, slots[i].transform.position.z);
                    cupGo.transform.parent = this.transform;
                    isFull[i] = true;
                    Destroy(cupGo.GetComponent<DragDrop>());
                    drinks++;
                }
            }
        }
    }
}
