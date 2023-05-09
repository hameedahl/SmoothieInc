using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    int slideNum = -1;

    public GameObject[] dialogue;
    public GameObject driverArt;
    public GameObject smoothieArt;
    public GameObject dialogueBox;
    public GameObject blackScreen;
    public GameObject cont;

    public AudioSource click;

    public GameObject crash;

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
        if(Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire1Alt"))
        {
            ShowSlide(slideNum);
            click.Play();
            slideNum++;
        }
        
    }

    public void Skip()
    {
        menu.StartMenu();
        gameObject.SetActive(false);
    }

    public void ShowSlide(int slideNum)
    {
        if(slideNum == -1)
        {
            // set driver art innactive
            driverArt.SetActive(false);
            // set smoothie art inactive
            smoothieArt.SetActive(false);

            cont.SetActive(true);
            blackScreen.SetActive(true);
        }
        else if(slideNum == 0)
        {
            // set driver art innactive
            driverArt.SetActive(false);
            // set smoothie art active
            smoothieArt.SetActive(true);

            cont.SetActive(false);
            blackScreen.SetActive(false);
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
            // set smoothie art innactive
            smoothieArt.SetActive(false);
            // set driver art innactive
            driverArt.SetActive(false);

            dialogue[3].SetActive(false);
            crash.SetActive(true);
            dialogueBox.SetActive(true);
            blackScreen.SetActive(true);
        }

        else if(slideNum == 5)
        {
             // set driver art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            crash.SetActive(false);
            dialogueBox.SetActive(false);
            blackScreen.SetActive(false);
            dialogue[4].SetActive(true);
            dialogueBox.SetActive(true);
        }


        else if(slideNum == 6)
        {
            driverArt.SetActive(false);
            // set smoothie art active
            smoothieArt.SetActive(true);

            dialogue[4].SetActive(false);
            dialogue[5].SetActive(true);
        }


        else if(slideNum == 7)
        {
            // set smoothie art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            dialogue[5].SetActive(false);
            dialogue[6].SetActive(true);
        }


        else if(slideNum == 8)
        {
            // set driver art innactive
            driverArt.SetActive(false);
            // set smoothie art active
            smoothieArt.SetActive(true);

            dialogue[6].SetActive(false);
            dialogue[7].SetActive(true);
        }


        else if(slideNum == 9)
        {
            // set smoothie art innactive
            smoothieArt.SetActive(false);
            // set driver art active
            driverArt.SetActive(true);

            dialogue[7].SetActive(false);
            dialogue[8].SetActive(true);
        }

        else if(slideNum == 10)
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
