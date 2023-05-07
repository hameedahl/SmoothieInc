using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    int slideNum = 0;

    public GameObject[] dialogue;
    public GameObject driverArt;
    public GameObject smoothieArt;

    public MainMenu menu;

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
        else if(slideNum == 1)
        {
            // set smoothie art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            dialogue[0].SetActive(false);
            dialogue[1].SetActive(true);
        }

        else if(slideNum == 2)
        {
             // set driver art innactive
            driverArt.SetActive(false);
            // set smoothie art active
            smoothieArt.SetActive(true);

            dialogue[1].SetActive(false);
            dialogue[2].SetActive(true);
        }

        else if(slideNum == 3)
        {
            // set smoothie art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            dialogue[2].SetActive(false);
            dialogue[3].SetActive(true);
        }

        else if(slideNum == 4)
        {
             // set driver art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            dialogue[3].SetActive(false);
            dialogue[4].SetActive(true);
        }


        else if(slideNum == 5)
        {
            driverArt.SetActive(false);
            // set smoothie art active
            smoothieArt.SetActive(true);

            dialogue[4].SetActive(false);
            dialogue[5].SetActive(true);
        }


        else if(slideNum == 6)
        {
            // set smoothie art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            dialogue[5].SetActive(false);
            dialogue[6].SetActive(true);
        }


        else if(slideNum == 7)
        {
            // set driver art innactive
            driverArt.SetActive(false);
            // set smoothie art active
            smoothieArt.SetActive(true);

            dialogue[6].SetActive(false);
            dialogue[7].SetActive(true);
        }


        else if(slideNum == 8)
        {
            // set smoothie art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            dialogue[7].SetActive(false);
            dialogue[8].SetActive(true);
        }

        else if(slideNum == 9)
        {

            // set driver art innactive
            driverArt.SetActive(false);
            // set smoothie art active
            smoothieArt.SetActive(true);

            dialogue[8].SetActive(false);
            dialogue[9].SetActive(true);
        }



        else
        {
            menu.StartMenu();
            gameObject.SetActive(false);
        }

    }

}
