using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Mixers")]
    public AudioMixer audioMixer;

    [Header("Main Menu UI")]
    public GameObject hostButton;
    public GameObject clientButton;
    public GameObject creditsButton;
    public GameObject title;
    public GameObject mainMenuBackground;

    [Header("Join Menu UI")]
    public GameObject joinCodeInputBox;
    public TMP_InputField joinCodeInput;
    public GameObject joinButton;
    public GameObject joinCodeDisplay;
    public GameObject joinMenuBackground;
    public GameObject closeButton;
    
    
    [Header("Object References")]
    public Camera truckCamera;
    public Camera smoothieCamera;
    public Camera menuCamera;

    public GameObject truckUI;
    public GameObject smoothieUI;

    public TestRelay testRelay;

    void Awake()
    {
        audioMixer.SetFloat("SFXVolume", -80.00f);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Hide Join Menu
        toggleJoinMenu(false, false);
        
        // Show Main Menu
        toggleMainMenu(true);
    }

    // enable or disable the join menu
    void toggleJoinMenu(bool enable, bool driver)
    {
        joinMenuBackground.SetActive(enable);
        if(enable)
        {
            if(!driver)
            {
                joinCodeInputBox.SetActive(enable);
                joinButton.SetActive(enable);
            }
            else
            {
                joinCodeDisplay.SetActive(enable);
                closeButton.SetActive(enable);
            }
        }
        else
        {
            joinCodeInputBox.SetActive(enable);
            joinButton.SetActive(enable);
            joinCodeDisplay.SetActive(enable);
            closeButton.SetActive(enable);
        }
    }

    // enable or disable the main menu
    void toggleMainMenu(bool enable)
    {
        hostButton.SetActive(enable);
        clientButton.SetActive(enable);
        creditsButton.SetActive(enable);
        title.SetActive(enable);
        mainMenuBackground.SetActive(enable);
    }

    public void StartHostMenu()
    {
        NetworkManager.Singleton.StartHost();
        testRelay.CreateRelay();
        toggleJoinMenu(true, true);
        toggleMainMenu(false);
    }

    public void StartHost()
    {
        menuCamera.gameObject.SetActive(false);
        truckCamera.gameObject.SetActive(true);
        truckUI.gameObject.SetActive(true);
        toggleJoinMenu(false, false);
        audioMixer.SetFloat("SFXVolume", 1.0f);
    }

    public void StartClientMenu()
    {
        toggleJoinMenu(true, false);
        toggleMainMenu(false);
    }

    public void StartClient()
    {
        menuCamera.gameObject.SetActive(false);
        toggleJoinMenu(false, false);
        NetworkManager.Singleton.StartClient();
        smoothieCamera.gameObject.SetActive(true);
        smoothieUI.gameObject.SetActive(true);
        testRelay.JoinRelay(joinCodeInput.text);
    }

    private void FixedUpdate()
    {
        if(joinCodeInput.text.Length >= 6)
        {
            joinCodeInput.text = joinCodeInput.text.Substring(0,6);
            joinButton.GetComponent<Button>().interactable = true;
        }
        else
            joinButton.GetComponent<Button>().interactable = false;
    }

    public void toggleCredits(bool enable)
    {
        if(enable)
        {
            Debug.Log("Show Credits");
        }
        if(!enable)
        {
            Debug.Log("Hide Credits");
        }

    }
}