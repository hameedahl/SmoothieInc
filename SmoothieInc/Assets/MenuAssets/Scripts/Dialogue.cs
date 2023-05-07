using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    int slideNum = 0;

    public GameObject[] dialogue;
    public GameObject driverArt;
    public GameObject smoothieArt;

    // Start is called before the first frame update
    void Start()
    {
        ShowSlide(slideNum);
        slideNum++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ShowSlide(slideNum);
            slideNum++;
        }
        
    }

    public void ShowSlide(int slideNum)
    {
        if(slideNum == 0)
        {
            // set driver art innactive
            driverArt.SetActive(false);
            // set smoothie art active
            smoothieArt.SetActive(true);

            dialogue[0].SetActive(true);
        }
        if(slideNum == 1)
        {
            // set smoothie art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            dialogue[0].SetActive(false);
            dialogue[1].SetActive(true);
        }

    }

}
