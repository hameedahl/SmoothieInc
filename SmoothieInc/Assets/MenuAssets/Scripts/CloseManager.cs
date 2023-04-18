using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseManager : MonoBehaviour
{
    public GameObject menu;

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}
